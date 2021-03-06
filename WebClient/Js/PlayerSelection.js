﻿
var clientSocket, ws;
var bExitLobby, bRefresh, bInvite, bAuthorization, bRegistration, bCreate, bEnter, bRef, buttons, bExit, userName, turn;
var inGame = false;
var online = false;
var texNewPass, bNewPass, bChangePass;
var textLoginReg, texPassReg, bRegistrationReg, bBackReg;
var bForgotPass;
var str = "";
var fblogin = "";
var fbid = 0;

window.onload = function () {

    bExitLobby = document.getElementById("bLogIn");
    bRefresh = document.getElementById("bLogOut");
    bInvite = document.getElementById("bInvite");
    bAuthorization = document.getElementById("bAuthorization");
    bRegistration = document.getElementById("bRegistration");
	bExit = document.getElementById("bExit");
    turn = document.getElementById("lTurn");    
	
	buttons = Array(document.getElementById("b1"), document.getElementById("b2"), b3 = document.getElementById("b3"),b4 = document.getElementById("b4"), b5 = document.getElementById("b5"), b6 = document.getElementById("b6"), b7 = document.getElementById("b7"), b8 = document.getElementById("b8"), b9 = document.getElementById("b9")); 
	userName = document.getElementById("textLogin");
	bexit = document.getElementById("bExit");
	
	texNewPass = document.getElementById("texNewPass");
	bNewPass = document.getElementById("bNewPass");
	bChangePass = document.getElementById("bChangePass");
	
	bForgotPass = document.getElementById("bForgotPass");
	
	textLoginReg = document.getElementById("textLoginReg");
	texPassReg = document.getElementById("texPassReg");
	bRegistrationReg = document.getElementById("bRegistrationReg");
	bBackReg = document.getElementById("bBackReg");
	
    bExitLobby.onclick = OnExitLobby;
	bExit.onclick = OnExitGame;
    bRefresh.onclick = OnRefresh;
    bInvite.onclick = OnInvite;
    bRegistration.onclick = OnRegistration;
    bAuthorization.onclick = OnAuthorization;
	bNewPass.onclick = OnNewPass;
	bChangePass.onclick = OnChangePass;
	bForgotPass.onclick = OnForgotPass;
	bBackReg.onclick = OnBackReg;
	bRegistrationReg.onclick = OnRegistrationReg;
}

function statusChangeCallback(response) {
    console.log('statusChangeCallback');
    console.log(response);
    if (response.status === 'connected') {
        success(response);
        //if (authorization) {
        //    authfb();
        //    return;
        //}
        regfb();
    } else if (response.status === 'not_authorized') {
        document.getElementById('status').innerHTML = 'Please log ' +
          'into this app.';
    } else {
        document.getElementById('status').innerHTML = 'Please log ' +
          'into Facebook.';
    }
}

function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

window.fbAsyncInit = function () {
    FB.init({
        appId: '{1821350971448020}',
        cookie: true,  // enable cookies to allow the server to access 
        // the session
        xfbml: true,  // parse social plugins on this page
        version: 'v2.8' // use graph api version 2.8
    });

    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
};

function success(response) {
    console.log(response.authResponse.accessToken);
    console.log(response.authResponse.name);
    console.log(response.authResponse.first_name);
    console.log(response.authResponse.last_name);
    console.log(response.authResponse.email);
    console.log(response.authResponse.userID.first_name);
    console.log(response.authResponse.userID.email);
    console.log(response.authResponse.userID.last_name);
    var userid1 = response.authResponse.accessToken.id;
    var useremail1 = response.authResponse.accessToken.email;
    var userid = response.id;
    var useremail = response.email;
}

function regfb() {
    console.log('Welcome!  Fetching your information.... ');
    FB.api('/me', { fields: 'name, email, first_name, last_name' }, function (response) {
        document.getElementById('status').innerHTML = 'name = ' + response.name + ' email = ' + response.email + '. Fn: ' + response.first_name + ', Ln: ' + response.last_name + '. Username: ' + response.username + '. ID: ' + response.id;
        fblogin = response.first_name;
        fbid = response.id;
        connect("regfb");
        setTimeout(function () {
        }, 5000);
    });

function connect(command) {
			try
			{
				ws = new WebSocket("ws://127.0.0.1:8888");
            }
			finally
			{
				str = "Server is not available";
			}
            ws.onopen = function () {
		
				if(command == "auth"){
					var name = document.getElementById("textLogin").value;
					var pass = document.getElementById("texPass").value;
					ws.send(command +","+ name + "," + pass + ",0");
				}
				else if(command == "reg"){
					var name = document.getElementById("textLoginReg").value;
					var pass = document.getElementById("texPassReg").value;
					var email = document.getElementById("texEmailReg").value;
					ws.send(command +","+ name + "," + pass + "," + email +  ",0");
				}
				else if(command == "sendpassword")
				{
					var name = document.getElementById("textLogin").value;
					if(name != "")
					{
						ws.send(command + "," + name);
					}
				}
            };

            ws.onmessage = function (evt) {
                var received_msg = evt.data;     
                
                listener(received_msg);
            };
            ws.onclose = function () {
				var c = "Connection is closed...";
				if(str != ""){                
					c = str;
					str = "";
				}
				//alert(c);
            };
        };
		
window.onbeforeunload = function(){
	if(online)
	{
		if(inGame)
			OnExitGame();
		OnExitLobby();
	}
}
		
function listener(message){
			message = message.replace("\r\n", "");
			var msg = message.split(",");
            switch(msg[0])
            {
				case "forgotpasssuccess":
					alert("Your password was sent to your email!");
					ws.close();
					break;
				case "forgotpassrefuse":
					alert("Invalid login!");
					ws.close();
					break;
                case "loginsuccess":
                    ShowMainPage();
                    document.getElementById("statusLabel2").value = "Your name: " + msg[1];
					online = true;
                    break;
                case "loginrefuse":
                    alert("Invalid login or password!")
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
					inGame = true;
                    break;
                default:
                break;
            }
        }

function ShowAuthorizationPage() {
    document.getElementById("authPage").style.display = 'block';
    document.getElementById("statusMenu").style.display = 'none';
    document.getElementById("popupMenu").style.display = 'none';
    document.getElementById("playerList").style.display = 'none';
	document.getElementById("changePassPage").style.display = 'none';
	document.getElementById("regPage").style.display = 'none';
}

function ShowRegistrationPage() {
	document.getElementById("regPage").style.display = 'block';
    document.getElementById("authPage").style.display = 'none';
    document.getElementById("statusMenu").style.display = 'none';
    document.getElementById("popupMenu").style.display = 'none';
    document.getElementById("playerList").style.display = 'none';
	document.getElementById("changePassPage").style.display = 'none';
}

function ShowChangePassPage() {
	document.getElementById("changePassPage").style.display = 'block';
    document.getElementById("authPage").style.display = 'none';
    document.getElementById("statusMenu").style.display = 'none';
    document.getElementById("popupMenu").style.display = 'none';
    document.getElementById("playerList").style.display = 'none';
	document.getElementById("regPage").style.display = 'none';
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
	document.getElementById("gameMenu").style.display = 'none';
	document.getElementById("changePassPage").style.display = 'none';
	document.getElementById("regPage").style.display = 'none';
}

function Game(message)
{
	var msg = message.split(",");
	if(msg[1] == "fail")
	{
		alert("You lost this game");
		ShowMainPage();
		inGame = false;
	}
	else if(msg[1] == "victory")
	{
		alert("You won!");
		ShowMainPage();
		inGame = false;
	}
	else if(msg[1] == "standoff")
	{
		alert("It's a draw!");
		ShowMainPage();
		inGame = false;
	}
	else if(msg[1] == "stopgame")
	{
		alert("The game was interruped");
		ShowMainPage();
		inGame = false;
	}
	else if(msg[1] == "yourturn")
		turn.textContent = "Your turn!";
	else if(msg[1] == "notyourturn")
		turn.textContent = "Not your turn!";
	else
	{
		buttons[msg[1]].value = msg[2];
		buttons[msg[1]].disabled = true;
	}
}

function ShowGamePage() {
    document.getElementById("authPage").style.display = 'none';
    document.getElementById("statusMenu").style.display = 'none';
    document.getElementById("popupMenu").style.display = 'none';
    document.getElementById("playerList").style.display = 'none';
    document.getElementById("gameMenu").style.display = 'block';
	document.getElementById("changePassPage").style.display = 'none';
	document.getElementById("regPage").style.display = 'none';
	for(var i=0;i<buttons.length;i++)
	{
		buttons[i].value = " ";
		buttons[i].disabled = false;
	}
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
	throw SyntaxError("Please choose player firstly");
	
}
function OnExitLobby(){
	var message = "lobby,exit,"+userName.value;
	ws.send(message);
	document.getElementById("statusLabel2").value = "Your name:";
	ShowAuthorizationPage();
	inGame = false;
	setTimeout(function() {
    ws.close();
	}, 2000);
	online = false;
}

function OnExitGame()
{
	var message = "games,gamexo," + userName.value + ",stopgame";
	ws.send(message);
	ShowMainPage();
	inGame = false;
}

function OnRefresh() {
	var message = "list";
	ws.send(message);
}

function OnInvite() {
	try
	{
		var message = "lobby,invite" + "," + userName.value + "," + GetSelectedPlayer() + "," + "XO";
		ws.send(message);
	}
	catch(ex)
	{
		alert(ex.message);
	}
}

function OnRegistration() {
	ShowRegistrationPage();
}

function OnAuthorization() {
	if(CheckRA())
		connect('auth');
}

function OnNewPass()
{
	ws.send("lobby,changepass," + userName.value + "," + texNewPass.value);
	ShowMainPage();
}

function CheckRA()
{
	var name = document.getElementById("textLogin").value;
    var pass = document.getElementById("texPass").value;
	if(name == "" || pass == "")
	{
		alert("Empty username or password");
		return false;
	}
	else if(/^[a-zA-Z1-9]+$/.test(name) === false && /^[a-zA-Z1-9]+$/.test(pass) === false)
	{
		alert('Invalid login or pass'); 
		return false;
	}
	if(name.length > 15)
    { 
		alert('Very long username! Enter username till 15 symbols.'); 
		return false;
	}

	return true;
}

function CheckRAReg()
{
	var name = document.getElementById("textLoginReg").value;
    var pass = document.getElementById("texPassReg").value;
	var email = document.getElementById("texEmailReg").value;
	if(name == "" || pass == "" || email == "")
	{
		alert("Empty username, password or email");
		return false;
	}
	else if(/^[a-zA-Z1-9]+$/.test(name) === false && /^[a-zA-Z1-9]+$/.test(pass) === false && /^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$/.test(email == false))
	{
		alert('Invalid login, pass or email'); 
		return false;
	}
	if(name.length > 15)
    { 
		alert('Very long username! Enter username till 15 symbols.'); 
		return false;
	}

	return true;
}

function OnChangePass()
{
	ShowChangePassPage();
}

function OnForgotPass()
{
	connect("sendpassword");
}

function OnBackReg()
{
	ShowAuthorizationPage();
}

function OnRegistrationReg()
{
	if(CheckRAReg())
		connect('reg');
}