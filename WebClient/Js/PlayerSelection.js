
var clientSocket, ws;
var bLogin, bLogout, bInvite, bAuthorization, bRegistration, bCreate, bEnter, bRef, buttons, lturn;


window.onload = function () {

    bLogin = document.getElementById("bLogIn");
    bLogout = document.getElementById("bLogOut");
    bInvite = document.getElementById("bInvite");
    bAuthorization = document.getElementById("bAuthorization");
    bRegistration = document.getElementById("bRegistration");
     
	buttons = Array(document.getElementById("b1"), document.getElementById("b2"), b3 = document.getElementById("b3"),b4 = document.getElementById("b4"), b5 = document.getElementById("b5"), b6 = document.getElementById("b6"), b7 = document.getElementById("b7"), b8 = document.getElementById("b8"), b9 = document.getElementById("b9"), userName = document.getElementById("textLogin"));
	lturn = document.getElementById("lGameTurn");
	
    bLogin.onclick = OnLogIn;
    bLogout.onclick = OnLogOut;
    bInvite.onclick = OnInvite;
    bRegistration.onclick = OnRegistration;
    bAuthorization.onclick = OnAuthorization;
    b1.onclick = OnBtnClick;
}

function connect(command) {
            ws = new WebSocket("ws://127.0.0.1:8888");
            
            ws.onopen = function () {
                var name = document.getElementById("textLogin").value;
                var password = document.getElementById("texPass").value;
                ws.send(command +","+ name + "," + password + ",0");
            };

            ws.onmessage = function (evt) {
                var received_msg = evt.data;     
                
                listener(received_msg);
            };
            ws.onclose = function () {
                alert("Connection is closed...");
            };
        };
		
function listener(message){
			message = message.replace("\r\n", "");
			var msg = message.split(",");
            switch(msg[0])
            {
                case "loginsuccess":
                    ShowMainPage();
                    document.getElementById("statusLabel2").value += msg[1];
                    break;
                case "loginrefuse":
                    alert("Введи логин и пароль нормальный БЛЕАТЬ!")
					ws.close();
                    break;
				case "list":
					AddToPlayerList(msg);
					break;
                case "invite":
                    var res = MessageBox("Player " + msg[1] + " invites you to play a game! Accept the invitation?");

					var ask = "games,ask" + ",";
					if (res === true)
					{
						ask += "Yes,";
					}
					else
					{
						ask += "No,";
					}
					ask += userName.value + "," + msg[1]+",XO";
					ws.send(ask);
                    break;
                case "gamexo":
					Game(message);
					break;
                case "ask":
                    ShowGamePage();
                    break;
                default:
                break;
            }
        }


    function Vasyan(zhopa){
    var rooms = document.getElementById("rooms");
    rooms.options[rooms.options.length] = new Option(zhopa, zhopa);
    rooms.size += rooms.options.length;
}

function ShowAuthorizationPage() {
    document.getElementById("authPage").style.display = 'block';
    document.getElementById("statusMenu").style.display = 'none';
    document.getElementById("popupMenu").style.display = 'none';
    document.getElementById("playerList").style.display = 'none';
}


function MessageBox(message) {
    var res = confirm(message);
    return res;
}


function ShowMainPage() {
    document.getElementById("authPage").style.display = 'none';
    document.getElementById("statusMenu").style.display = 'flex';
    document.getElementById("popupMenu").style.display = 'flex';
    document.getElementById("playerList").style.display = 'block';
}

function Game(message)
{
	var msg = message.split(",");
	if(msg[1] == "fail")
	{
		alert("You lost this game");
		ShowMainPage();
        document.getElementById("statusLabel2").value += msg[1];
	}
	else if(msg[1] == "victory")
	{
		alert("You won!");
		ShowMainPage();
		document.getElementById("statusLabel2").value += msg[1];
	}
	else if(msg[1] == "stopgame")
	{
		alert("The game was interruped");
		ShowMainPage();
        document.getElementById("statusLabel2").value += msg[1];
	}
	else if(msg[1] == "yourturn")
		lturn.text = "Your turn";
	else if(msg[1] == "notyourturn")
		lturn.text = "Not your turn";
	else
	{
		//var btn = document.getElementById("b"+msg[1]+1);
		buttons[msg[1]].value = msg[2];
		buttons[msg[1]].disabled = true;
	}
}

function ShowGamePage() {
    document.getElementById("authPage").style.display = 'none';
    document.getElementById("statusMenu").style.display = 'none';
    document.getElementById("popupMenu").style.display = 'none';
    document.getElementById("playerList").style.display = 'none';
    document.getElementById("gameMenu").style.display = "block";
}

function OnBtnClick(btn)
{
	var message = "games,gamexo,"+userName.value+","+btn;
	ws.send(message);
}

function AddToPlayerList(names) {
    playerList.innerHTML = "";

    for (var i = 1; i < names.length; i++) {
        if (names[i] === userName.value){
            continue;
        }
		var args = names[i].split("#");
		if(args[1] == "0")
		{
        playerList.innerHTML += "<input type='radio' name='players' id='" + args[0] + "' />" + args[0] + "<br />";
		}
    }
}
function GetSelectedPlayer() {
    for (var i = 0; i < playerList.childNodes.length; i++) {
        if (playerList.childNodes[i].checked) {
            return playerList.childNodes[i + 1].nodeValue;
        }
    }
}
function OnLogIn(){
	ws = new WebSocket("ws://localhost:8888");

    ws.onopen = function () {
    };
    ws.onclose = function () {
        alert("Connection is closed...");
    };
    ws.onmessage = OnMessageReceive;
    ShowAuthorizationPage();
}

function OnLogOut() {

}

function OnInvite() {
	var message = "lobby,invite" + "," + userName.value + "," + GetSelectedPlayer() + "," + "XO";
	ws.send(message);
    
}

function OnRegistration() {
    connect('reg');

}

function OnAuthorization() {
    connect('auth');
}


function RefreshRoom(){
    var message = new Object();
    message.modul = "lobby";
    message.command = "refresh";
    var tmp = JSON.stringify(message);
    ws.send(tmp); 
}

