﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer 
{
    public class XO : IGame
    {
        int[] matrix;
        Client player1;
        Client player2;
        bool player1turn = true;

        public XO()
        {
            matrix = new int[9];
        }
        public XO(Client player1, Client player2)
        {
            matrix = new int[9];
            this.player1 = player1;
            this.player2 = player2;
            Thread.Sleep(100);
            player1.stream.Write("gamexo,yourturn");
            player2.stream.Write("gamexo,notyourturn");
            Thread.Sleep(100);
        }

        public bool ContainsPlayer(Client client)
        {
            try
            {
                if (player1.Equals(client) || player2.Equals(client))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: ContainsPlayer" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }

        public bool Action(Client player, string input)
        {
            try
            {
                if (input == "stopgame")
                {
                    if (player.Equals(player2))
                    {
                        player2.stream.Write("gamexo" + "," + "fail");
                        player1.status = "0";

                        player2.status = "0";
                        player1.stream.Write("gamexo" + "," + "victory");
                    }
                    else
                    {
                        player2.stream.Write("gamexo" + "," + "victory");
                        player1.status = "0";

                        player2.status = "0";
                        player1.stream.Write("gamexo" + "," + "fail");
                    }
                    return true;
                }
                if (player1turn && player1.Equals(player)) // player1 turn
                {
                    string turn1 = input;
                    string res1 = Turn1(turn1);

                    player2.stream.Write("gamexo" + "," + turn1 + "," + "X");
                    player1.stream.Write("gamexo" + "," + turn1 + "," + "X");

                    Thread.Sleep(100);

                    if (res1 == "victory")
                    {
                        player1.stream.Write("gamexo" + "," + "victory");
                        player1.status = "0";

                        player2.status = "0";
                        player2.stream.Write("gamexo" + "," + "fail");

                        return true;
                    }

                    if (res1 == "standoff")
                    {
                        player1.stream.Write("gamexo" + "," + "standoff");
                        player1.status = "0";
                        player2.stream.Write("gamexo" + "," + "standoff");
                        player2.status = "0";

                        return true;
                    }

                    player1turn = !player1turn;

                    Thread.Sleep(100);
                    player2.stream.Write("gamexo,yourturn");
                    Thread.Sleep(100);
                    player1.stream.Write("gamexo,notyourturn");
                }

                else if (!player1turn && player2.Equals(player))
                {
                    string turn2 = input;
                    string res2 = Turn2(turn2);

                    player1.stream.Write("gamexo" + "," + turn2 + "," + "O");
                    player2.stream.Write("gamexo" + "," + turn2 + "," + "O");

                    Thread.Sleep(100);

                    if (res2 == "victory")
                    {
                        player1.stream.Write("gamexo" + "," + "fail");
                        player1.status = "0";
                        player2.stream.Write("gamexo" + "," + "victory");
                        player2.status = "0";

                        return true;
                    }

                    if (res2 == "standoff")
                    {
                        player1.stream.Write("gamexo" + "," + "standoff");
                        player1.status = "0";
                        player2.stream.Write("gamexo" + "," + "standoff");
                        player2.status = "0";

                        return true;
                    }

                    player1turn = !player1turn;

                    Thread.Sleep(100);
                    player1.stream.Write("gamexo,yourturn");
                    Thread.Sleep(100);
                    player2.stream.Write("gamexo,notyourturn");
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: Action" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }

        public string Turn1(string message)
        {
            try
            {
                if (matrix[Convert.ToInt32(message)] == 0)
                {
                    matrix[Convert.ToInt32(message)] = 1;
                }
                if (Checking() == 1)
                    return "victory";
                if (Checking() == -1)
                    return "standoff";

                else return "";
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: Turn1" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
        public string Turn2(string message)
        {
            try
            {
                if (matrix[Convert.ToInt32(message)] == 0)
                {
                    matrix[Convert.ToInt32(message)] = 2;
                }
                if (Checking() == 1)
                    return "victory";
                if (Checking() == -1)
                    return "standoff";
                else return "";
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: Turn2" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }

        public int Checking()
        {
            try
            {
                int res = 0;

                if (((matrix[0] == 1 && matrix[3] == 1 && matrix[6] == 1) || (matrix[0] == 2 && matrix[3] == 2 && matrix[6] == 2))
                    || ((matrix[1] == 1 && matrix[4] == 1 && matrix[7] == 1) || (matrix[1] == 2 && matrix[4] == 2 && matrix[7] == 2))
                    || ((matrix[2] == 1 && matrix[5] == 1 && matrix[8] == 1) || (matrix[2] == 2 && matrix[5] == 2 && matrix[8] == 2))
                    || ((matrix[0] == 1 && matrix[4] == 1 && matrix[8] == 1) || (matrix[0] == 2 && matrix[4] == 2 && matrix[8] == 2))
                    || ((matrix[2] == 1 && matrix[4] == 1 && matrix[6] == 1) || (matrix[2] == 2 && matrix[4] == 2 && matrix[6] == 2))
                    || ((matrix[0] == 1 && matrix[1] == 1 && matrix[2] == 1) || (matrix[0] == 2 && matrix[1] == 2 && matrix[2] == 2))
                    || ((matrix[3] == 1 && matrix[4] == 1 && matrix[5] == 1) || (matrix[3] == 2 && matrix[4] == 2 && matrix[5] == 2))
                    || ((matrix[6] == 1 && matrix[7] == 1 && matrix[8] == 1) || (matrix[6] == 2 && matrix[7] == 2 && matrix[8] == 2)))
                    res = 1;
                else
                {
                    for (int i = 0; i < matrix.Length; i++)
                    {
                        if (matrix[i] == 0)
                        {
                            break;
                        }
                        if (i == matrix.Length - 1 && matrix[i] != 0)
                        {
                            res = -1;
                        }
                    }
                }

                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: Checking" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
    }
}
