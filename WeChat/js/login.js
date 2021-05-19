var ws = null;
var user = null;

Initws();

function Initws() {
    if ("WebSocket" in window) {
        // 打开一个 web ws
        ws = new WebSocket("ws://127.0.0.1:30000");

        ws.onopen = function() {

        };

        ws.onmessage = function(evt) {
            var response = JSON.parse(evt.data);
            if (response.type == "login") {
                LoginHandler(response);
            }
        };

        ws.onclose = function() {
            SendLogoutRequest();
            alert("连接已关闭...");
        };

        ws.onerror = function() {
            alert("连接失败")
        }
    } else {
        // 浏览器不支持 Webws
        alert("您的浏览器不支持 Webwsocket!");
    }
}

function SendLoginRequest() {
    var request = {
        type: "login",
        account: "161830207",
        password: "123"
    };
    ws.send(JSON.stringify(request));
}

function LoginHandler(response) {
    if (response.status == 200) {
        user = response.user;
        console.log(user);
        sessionStorage.setItem("ws", ws);
        sessionStorage.getItem("user", user);
        window.location.href = "../index.html";
    } else {
        alert("登陆失败：" + response.message);
    }
}

$(".loginButton").click(() => {
    var account = $("#account").val();
    var password = $("#password").val();
    if (account == "") {
        alert("账号不能为空");
    } else if (password == "") {
        alert("密码不能为空");
    } else {
        SendLoginRequest(account, password);
    }
})