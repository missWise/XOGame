using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameServer
{
    public class UniversalStream
    {
        public UniversalStream(TcpClient client)
        {
            this.client = client;
            this.stream = client.GetStream();
        }

        public ClientType Type { get; set; }
        private TcpClient client;
        private NetworkStream stream;
        private byte[] message;
        
        public string Read()
        {
            string res = null;

            byte[] bytes = new byte[client.Available];
            stream.Read(bytes, 0, bytes.Length);
            message = (byte[])bytes.Clone();
            res = Encoding.UTF8.GetString(bytes);
            
            return res;
        }
        
        public string Decode()
        {
            return (Type == ClientType.Web) ? DecodeMessage(message) : null;
        }
        public void Write(string message)
        {
            if (Type == ClientType.Web)
            {
                byte[] response;
                message += Environment.NewLine;
                response = Encoding.UTF8.GetBytes("  " + message);
                response[0] = 0x81; // denotes this is the final message and it is in text
                response[1] = Convert.ToByte(response.Length - 2); // payload size = message - header size
                stream.Write(response, 0, response.Length);
            }
            else if (Type == ClientType.Desktop)
            {
                var sw = new StreamWriter(stream);
                sw.WriteLine(message);
                sw.Flush();
            }
        }
        public void WriteHandshake(string data)
        {
            string message = "HTTP/1.1 101 Switching Protocols" + Environment.NewLine
                + "Connection: Upgrade" + Environment.NewLine
                + "Upgrade: websocket" + Environment.NewLine
                + "Sec-WebSocket-Accept: "
                + Convert.ToBase64String(
                    SHA1.Create().ComputeHash(
                        Encoding.UTF8.GetBytes(
                            new Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1].Value.Trim()
                                + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                        )
                    )
                ) + Environment.NewLine
                + Environment.NewLine;

            byte[] response = Encoding.UTF8.GetBytes(message);
            stream.Write(response, 0, response.Length);
        }

        private string DecodeMessage(byte[] bytes)
        {
            string incomingData = string.Empty;
            byte secondbyte = bytes[1];
            int dataLength = secondbyte & 127;
            int indexFirstMask = 2;
            if (dataLength == 126)
                indexFirstMask = 4;
            else if (dataLength == 127)
                indexFirstMask = 10;

            IEnumerable<byte> keys = bytes.Skip(indexFirstMask).Take(4);
            int indexFirstDatabyte = indexFirstMask + 4;

            byte[] decoded = new byte[bytes.Length - indexFirstDatabyte];
            for (int i = indexFirstDatabyte, j = 0; i < bytes.Length; i++, j++)
            {
                decoded[j] = (byte)(bytes[i] ^ keys.ElementAt(j % 4));
            }

            return incomingData = Encoding.UTF8.GetString(decoded, 0, decoded.Length);
        }

        private byte[] EncodeMessageToSend(string message)
        {
            byte[] response;
            byte[] bytesRaw = Encoding.UTF8.GetBytes(message);
            byte[] frame = new byte[10];

            int indexStartRawData = -1;
            int length = bytesRaw.Length;

            frame[0] = (byte)129;
            if (length <= 125)
            {
                frame[1] = (byte)length;
                indexStartRawData = 2;
            }
            else if (length >= 126 && length <= 65535)
            {
                frame[1] = (byte)126;
                frame[2] = (byte)((length >> 8) & 255);
                frame[3] = (byte)(length & 255);
                indexStartRawData = 4;
            }
            else
            {
                frame[1] = (byte)127;
                frame[2] = (byte)((length >> 56) & 255);
                frame[3] = (byte)((length >> 48) & 255);
                frame[4] = (byte)((length >> 40) & 255);
                frame[5] = (byte)((length >> 32) & 255);
                frame[6] = (byte)((length >> 24) & 255);
                frame[7] = (byte)((length >> 16) & 255);
                frame[8] = (byte)((length >> 8) & 255);
                frame[9] = (byte)(length & 255);

                indexStartRawData = 10;
            }

            response = new byte[indexStartRawData + length];

            int i, reponseIdx = 0;

            //Add the frame bytes to the reponse
            for (i = 0; i < indexStartRawData; i++)
            {
                response[reponseIdx] = frame[i];
                reponseIdx++;
            }

            //Add the data bytes to the response
            for (i = 0; i < length; i++)
            {
                response[reponseIdx] = bytesRaw[i];
                reponseIdx++;
            }

            return response;
        }

        public enum ClientType
        {
            Desktop,
            Web,
            Android
        }
    }
}
