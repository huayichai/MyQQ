var ws = null;

InitSocket();
SendFriendListRequest();

function InitSocket() {
    if ("WebSocket" in window) {
        // 打开一个 web socket
        ws = new WebSocket("ws://127.0.0.1:30000");

        ws.onopen = function() {
            // Web Socket 已连接上，使用 send() 方法发送数据
            alert("连接Socket成功");
        };

        ws.onmessage = function(evt) {
            var received_msg = evt.data;
            alert("数据已接收..." + "\r\n" + received_msg);
        };

        ws.onclose = function() {
            // 关闭 websocket
            alert("连接已关闭...");
        };

        ws.onerror = function() {
            alert("连接失败")
        }
    } else {
        // 浏览器不支持 WebSocket
        alert("您的浏览器不支持 WebSocket!");
    }
}

function SendFriendListRequest() {
    var request = {
        type: "friend",
        account: "161830207"
    };
    ws.addEventListener('open', function() {
        ws.send(JSON.stringify(request));
    });
}