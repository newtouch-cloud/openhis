//var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5212/hubs/msgcenter").build();
"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub", { accessTokenFactory: () => "d2762dbd" }).build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user) {
    console.debug(user);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

//$.connection.hub.url = "http://localhost:5212/hubs/msgcenter?accesstoken=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBcHBJZCI6IkFQSS5NYW5hZ2UiLCJPcmdhbml6ZUlkIjoiNmQ1NzUyYTctMjM0YS00MDNlLWFhMWMtZGY4YjQ1ZDM0NjlmIiwiVG9wT3JnYW5pemVJZCI6ImQ4ZWU4NWY4LWQ4ZDEtNGI1Yy1iZDllLTE3NjFmNDQyNDJkOCIsIkFjY291bnQiOiIwMDAwMDAiLCJVc2VyQ29kZSI6IjAwMDAwMCIsIlVzZXJOYW1lIjoiMDAwMDAwIiwiVXNlcklkIjoiYjI0NWE2MzMtMTk3Ni00ZGFlLTkxNTMtNThhZmVkOWQyYjcwIiwiVG9rZW5UeXBlIjoiIiwiVG9rZW4iOiIiLCJBcHBBdXRoS2V5IjoiODk1QTBERkY5NjU4MEZGQzRGRUU3ODdBMEQwRDRERTgwRkMwNERFNTY1Mjk1NjM0RUZCNDM0OURCQjY0RTNCMTE3RjI0QkFDQUQ3MDlGMzM5QTBDQjNENDIyQzRDNzc0OERDNDM5NUNEODY3NDlDNDdEQkVCRTM3MUE5RDQyQzg5Qjk4ODA0QUM0ODE5OTk2OEQ2OTkwMjc2RkM3NTczQ0QyNTczMjQxMjREOTNCMDI1MzBEQTBGMDM2NDFFNEUwNkQ2MTg4MEVBNTlENTM5MTRBRkEwMEZFMzkyMDk0OTM1MEU0RDlFMkM5QjM1OTdDQTk3MkJBRUM5RDgyQjY3MzRDOTNFQkUxM0Q1MTQ5MEY1NzE5RjJFMzUwMTdGOTk3ODFCOTJEMUUyMkZCQzA1NTRCMjIzRjIyMEM3QkJCMUU1MDA2NDgwQzVBMUQyNTFFMzcyNDIzOUREMTBFQzFDQjg4QkRBNzQ2REYwREUyNDFGMzM5MEYxQzQwNUJDMzRGNDU2QTdGODI3RUUyQzM0NUZEMkExRjA2MUREQzE2QzJCMkVEQkVGNDM2MDE5QUMzIiwiZXhwIjoxNzAwOTE5Nzg0LCJpc3MiOiJOZXd0b3VjaC5PcGVuSElTIiwiYXVkIjoiQVBJLk1hbmFnZSJ9.6Ljqg7vqcCgUej5PgYUQ-nSVoCXBigkIgOMIXSmpbxo";

//var messageHubProxy = $.connection.signalRMessageHub;
//messageHubProxy.client.SendAsync = function (name, message) {
//    console.log(name + ' ' + mesage);
//}

//$.connection.hub.start().done(function () {
//    // 连接发送按钮来调用服务器上的NewContosoChatMessage。
//    $("#sendButton").click(function () {
//        contosoChatHubProxy.server.newContosoChatMessage($("#userInput").val(), $("#messageInput").val());
//        $("#messageInput").val('').focus();
//    });
//}).catch(function (err) {
//    return console.error(err.toString());
//});

////$.connection.on("ReceiveMessage", function (user, message) {
////    var li = document.createElement("li");
////    document.getElementById("messagesList").appendChild(li);

////    li.textContent = `${user} ：${message}`;
////});
 

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    contosoChatHubProxy.server.newContosoChatMessage(user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});

