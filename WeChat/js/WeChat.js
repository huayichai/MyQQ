//==========================================================================
// 全局变量
//==========================================================================
var ws = null; // ws
var FriendList = new Array(); //好友列表
var user = null;

document.title = "登录";

Initws();
// 初始化ws
function Initws() {
    if ("WebSocket" in window) {
        // 打开一个 web ws
        ws = new WebSocket("ws://127.0.0.1:30000");

        ws.onopen = function() {

        };

        ws.onmessage = function(evt) {
            var response = JSON.parse(evt.data);
            CenterController(response);
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

function CenterController(response) {
    console.log(response);
    if (response.type == "friend") {
        FriendHandler(response);
    } else if (response.type == "message") {
        MessageHandler(response);
    } else if (response.type == "login") {
        LoginHandler(response);
    } else {
        alert("未注册事件");
    }
}

function FriendHandler(response) {
    FriendList = response.friends;
    ShowFriendList();
}

function MessageHandler(response) {
    var index;
    for (var i = 0; i < FriendList.length; i++) {
        if (FriendList[i].account == response.from) {
            index = i;
            break;
        }
    }
    console.log("success");
    var msgBox = $("#leftlist_list2_" + index + "_main [class=right_xianshi]");
    console.log(msgBox);
    msgBox.append("<div class='time'>" + getTime() + "</div>"); //时间
    msgBox.append("<div class='lleft'>" + img + "</div>"); //头像
    msgBox.append("<div class='lright'><div class='arrow1'></div><div class='neirong'>" + response.content + "</div></div>");
    msgBox.append("<div class='cle'></div>");
    //msgBox[0].scrollTop = msgBox[0].scrollHeight;
}

function LoginHandler(response) {
    if (response.status == 200) {
        user = response.user;
        $(".myBox").css("display", "none");
        $(".Box").css("display", "");
        SendFriendListRequest();
        ShowUserInfo();
        document.title = user.name;
    } else {
        alert("登陆失败：" + response.message);
    }
}

function SendLogoutRequest() {
    console.log(user);
    if (user != null) {
        var request = {
            type: "logout",
            account: user.account
        };
        ws.send(JSON.stringify(request));
    }
}


// 请求好友列表
function SendFriendListRequest() {
    var request = {
        type: "friend",
        account: user.account
    };
    ws.send(JSON.stringify(request));
}


function SendLoginRequest(account, password) {
    var request = {
        type: "login",
        account: account,
        password: password
    };
    ws.send(JSON.stringify(request));
}


// 展示好友列表
function ShowFriendList() {
    for (var ss = 0; ss < FriendList.length; ss++) {
        $("#leftlist_list2_ul3").append(
            '<div id="leftlist_list2_ul3_' +
            ss + '" class="leftlist_list2_ul3_li">' + '<img src="img/webwxgetheadimg (1).jpg">' + FriendList[ss].name + '</div>'
        );
    }
    //以下为联系人列表
    $("#leftlist_list2_ul3 .leftlist_list2_ul3_li").each(function() {
        var num = 0;
        $(this).click(function() {
            // 找到是第几个用户
            for (var t = 0; t < $(".leftlist_list2_ul3_li").length; t++) {
                if ($(this).attr("id") == "leftlist_list2_ul3_" + t) {
                    num = t;
                }
            }
            $(".leftlist_list2_ul3_li").css("background-color", "#2E3238");
            $(this).css("background-color", "#3A3F45");
            $id = $(this).attr("id");
            $("#right_title").html($(this).text());
            var $main = $("#right_main_3");
            $main.children().hide();

            $this = $("#" + $id + "_main");
            if ($this.length > 0) {
                $this.show();
            } else {
                $main.append(
                    '<div id="' + $id + '_main"><div class="right_main_3_touxiang">' +
                    '<img src="img/webwxgetheadimg (1).jpg">' +
                    '</div><div class="right_main_3_name">' +
                    FriendList[num].name +
                    '<img src="img/头像图标.png" /></div><div class="right_main_3_nicheng">' +
                    FriendList[num].name + '</div><div class="right_main_3_beizhu">备注：' +
                    FriendList[num].name + '</div><div class="right_main_3_diqu">地区：' +
                    FriendList[num].introduction +
                    '</div><input class="right_main_3_faxiaoxi" type="button" value="发消息"/></div> '
                );
                $("#" + $id + "_main .right_main_3_faxiaoxi").click(function() {
                    $main.hide();
                    $("#leftlist_list1_liaotian").click();

                    if ($("#leftlist_list2_" + num).length > 0) {
                        $("#leftlist_list2_ul1").prepend($("#leftlist_list2_" + num))
                        $("#leftlist_list2_" + num).click();
                    } else {
                        $("#leftlist_list2_ul1").prepend(
                            "<li id='leftlist_list2_" + num + "'><span>" +
                            '<img src="img/webwxgetheadimg (1).jpg">' + "</span><p class='name'><span>" +
                            FriendList[num].name +
                            "</span><span class='span_time'></span></p><p class='liaotian'></p></li>");
                        //添加点击事件
                        $("#leftlist_list2_" + num).click(li1tianjia);
                        $("#leftlist_list2_" + num).click();
                    }
                })
            }
        })
    })
}


function ShowUserInfo() {
    $("#leftlist_title_name").text(user.name);
}








//==========================================================================================
// 页面后台部分
//==========================================================================================

$("#leftlist_list1 p").each(function() {
    $(this).click(function() {
        $("#gundongttiao").css("display", "none");
        $("#gundongttiao1").css("display", "none");
        for (var i = 0; i < $("#leftlist_list1 p").length; i++) {
            $("#leftlist_list1 p").eq(i).children().eq(0).css("display", "none");
            $("#leftlist_list2 ul").eq(i).css("display", "none");
            $("#leftlist_list1 p").eq(i).children().eq(1).css("display", "");
        }
        var index = parseInt($(this).attr("class"));
        $(this).children().eq(0).css("display", "");
        $(this).children().eq(1).css("display", "none");
        $("#leftlist_list2 ul").eq(index).css("display", "");
    })
})
$("#leftlist_list1_liaotian").click(function() {
    $("#right_main_1").show();
    $("#right_main_2").hide();
    $("#right_main_3").hide();
})
$("#leftlist_list1_zhong").click(function() {
    $("#right_main_2").show();
    $("#right_main_1").hide();
    $("#right_main_3").hide();
})
$("#leftlist_list1_you").click(function() {
    $("#right_main_3").show();
    $("#right_main_2").hide();
    $("#right_main_1").hide();
})

function li1tianjia() {
    $("#right_title").html($(this).children().eq(1).children().eq(0).html());
    var $main = $("#right_main_1");
    $("#leftlist_list2_ul1 li").css("background-color", "#2E3238");
    $(this).css("background-color", "#3A3F45");
    $main.children().css("display", "none");
    $id = $(this).attr("id");
    toUser = FriendList[parseInt($id.substr($id.lastIndexOf("_") + 1))];
    if ($("#" + $id + "_main").length > 0) {
        $("#" + $id + "_main").show();
    } else {
        $main.append('<div id="' + $id + '_main"><div class="right_xianshi"></div><div class="right_zhushou"><img src="img/图标/表情.png" class="biaoqing"/><img src="img/图标/剪切.png" /><img src="img/图标/文件.png" /></div><div class="right_text" contenteditable="true" onkeyup="backhome(event)"></div><p><input type="button" value="发送" class="right_fasong" /><span class="right_zhushi">按下Ctrl+Enter发送</span> </p></div>')
    }
    $(this).children().eq(2).css("color", "white");
    img = $(this).children().eq(0).html();
    txt = $("#" + $id + "_main [class=right_text]")[0];
    info = $("#" + $id + "_main [class=right_xianshi]");
    $time = $(this).children().eq(1).eq(0).children().eq(1);
    $liao = $(this).children().eq(2);
}

$("#right_main_1").click(function(event) {
    var $this = event.target ? event.target : event.srcElement;
    if ($this.type == "button") {
        cli();
    }
});
var xianshi = "";

// 发送消息
function cli() {
    var num = parseInt(Math.random() * 4) + 1;
    xianshi += txt.innerHTML;
    //右
    info.append("<div class='time'>" + getTime() + "</div>"); //时间
    info.append("<div class='rright'>" + '<img id="leftlist_title_touxiang" src="img/webwxgeticon (2).jpg" />' + "</div>") //头像
    info.append("<div class='rleft'><div class='arrow2'></div><div class='neirong'>" + xianshi + "</div></div>")
    info.append("<div class='cle'></div>")

    var messageRequest = {
        type: "message",
        from: user.account,
        to: toUser.account,
        content: txt.innerHTML,
        time: Date.now
    }

    ws.send(JSON.stringify(messageRequest));

    //左
    // $.post("http://www.tuling123.com/openapi/api", {
    //     key: "26c463640125432b9f275ffe6d999c05",
    //     info: xianshi,
    //     userid: "12345678"
    // }, function(data) {
    //     //机器人返回的信息
    //     info.append("<div class='time'>" + getTime() + "</div>"); //时间
    //     info.append("<div class='lleft'>" + img + "</div>"); //头像
    //     if (data.url) {
    //         info.append("<div class='lright'><div class='arrow1'></div><div class='neirong'>" + data.text + "<a target='_blank' href='" + data.url + "'>打开页面</a>" + "</div></div>");
    //     } else if (data.list) {
    //         info.append('<div class="lright">' + '<div class="xinwen"><div class="xinwen_1"><a target="_blank" href="' + data.list[0].detailurl + '"><p>' + data.list[0].article + '</p></a><a target="_blank" href="' + data.list[0].detailurl + '"><img src="' + data.list[0].icon + '" /></a></div><div class="xinwen_2"><a target="_blank" href="' + data.list[1].detailurl + '"><div class="mark">' + data.list[1].article + '</div></a><a target="_blank" href="' + data.list[1].detailurl + '"><img src="' + data.list[1].icon + '" /></a></div><div class="xinwen_2"><a target="_blank" href="' + data.list[2].detailurl + '"><div class="mark">' + data.list[2].article + '</div></a><a target="_blank" href="' + data.list[2].detailurl + '"><img src="' + data.list[2].icon + '" /></a></div><div class="xinwen_2"><a target="_blank" href="' + data.list[3].detailurl + '"><div class="mark">' + data.list[3].article + '</div></a><a target="_blank" href="' + data.list[3].detailurl + '"><img src="' + data.list[3].icon + '" /></a></div></div>' + '</div>');
    //     } else {
    //         info.append("<div class='lright'><div class='arrow1'></div><div class='neirong'>" + data.text + "</div></div>");
    //     }
    //     info.append("<div class='cle'></div>");
    //     info[0].scrollTop = info[0].scrollHeight;
    //     $lasthtml = $("#" + $id + "_main .lright>.neirong").last().html();
    //     $liao.html(imgpd.test($lasthtml) ? "图片" : $lasthtml);

    // });
    $time.html(getTime());
    // var imgpd = /^<img src/;
    // $lasthtml = $("#" + $id + "_main .lright>.neirong").last().html();

    // $liao.html(imgpd.test($lasthtml) ? "图片" : $lasthtml);
    txt.innerHTML = "";
    xianshi = "";
    info[0].scrollTop = info[0].scrollHeight;
}

function dd(num) {
    return num < 10 ? "0" + num : num;
}

function getTime() {
    var time = new Date();
    var hh = dd(time.getHours());
    var mm = dd(time.getMinutes());
    return hh + ":" + mm;
}

function backhome(event) {
    if (event.ctrlKey && !event.keyCode == 13) {
        txt.innerHTML = txt.innerHTML + '\n';
    }
}
window.onkeydown = function(event) {
    if (event.keyCode == 13 && event.ctrlKey) {
        cli();
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

$("#leftlist_title_touxiang").click(() => {
    SendLogoutRequest();
    window.close();
})

// window.onbeforeunload = () => {
//     alert("cao");
//     SendLogoutRequest();
// }