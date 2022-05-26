# MyQQ

## 前言

本项目使用C#的WPF框架+Socket通信，仿制了QQ的客户端。

使用技术：

- C# WPF框架、**多线程技术**
- **Socket编程**
- HTML、CSS、JS、WebSocket库
- MySQL
- VS2019



## 项目简介

- Server/

	该目录下存放服务端程序

- QQ/

	该目录下存放QQ客户端

- WeChat/

	该目录下存放WeChat网页端

- Report/

	该目录下存放项目报告



## 架构简介

- QQ客户端之间、WeChat网页端之间、客户端和网页端之间的聊天信息均通过Server中转
- Server多线程处理多个用户的数据请求
![image](https://github.com/huayichai/MyQQ/blob/master/Report/img/UseCase.png)


## 使用方式

1. 分别编译QQ、Server两个项目后，得到两个可执行文件QQ.exe、Server.exe
2. 按照Report/详细设计报告第四章数据库设计创建表。修改Server/DAO/BaseDAO.cs文件中constring变量连接字符串的值以连接到你本地的数据库。
3. 双击Server.exe开启服务端
4. 双击QQ.exe开启客户端（可开启多个，进行聊天）
5. 双击Wechat/index.html开启WeChat网页端（可开启多个，进行聊天）
6. 可以跨QQ客户端以及WeChat网页端间进行聊天





## 展示

![image](https://github.com/huayichai/MyQQ/blob/master/Report/img/login.png)


![image](https://github.com/huayichai/MyQQ/blob/master/Report/img/FriendWindow.png)


![image](https://github.com/huayichai/MyQQ/blob/master/Report/img/ChatWindow.png)


![image](https://github.com/huayichai/MyQQ/blob/master/Report/img/AddFriendWindow.png)


![image](https://github.com/huayichai/MyQQ/blob/master/Report/img/HistoryWindow.png)


![image](https://github.com/huayichai/MyQQ/blob/master/Report/img/WeChatLogin.png)


![image](https://github.com/huayichai/MyQQ/blob/master/Report/img/WeChatFriend.png)


![image](https://github.com/huayichai/MyQQ/blob/master/Report/img/WeChatChat.png)


![image](https://github.com/huayichai/MyQQ/blob/master/Report/img/ServerMainWindow.png)


![image](https://github.com/huayichai/MyQQ/blob/master/Report/img/ChatManagement.png)



> **友情提示：南航的同学使用该项目请慎重！！！**（特别是：**计算机网络实验+现代软件开发方法 **这两门）



![image](https://github.com/huayichai/MyQQ/blob/master/Report/img/do%20not%20copy.png)
