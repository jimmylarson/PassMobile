<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="PassMobile.login" %>



<!DOCTYPE html>
<html>
<head>
    <!--
        Customize the content security policy in the meta tag below as needed. Add 'unsafe-inline' to default-src to enable inline JavaScript.
        For details, see http://go.microsoft.com/fwlink/?LinkID=617521
    -->

    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta name="format-detection" content="telephone=no">
    <meta name="msapplication-tap-highlight" content="no">
    <meta name="viewport" content="user-scalable=no, initial-scale=1, maximum-scale=1, minimum-scale=1, width=device-width">
    <link rel="stylesheet" type="text/css" href="css/global.css" />
    <link rel="stylesheet" type="text/css" href="css/index.css">
    <script src="fullcalendar/lib/jquery.min.js"></script>
    <script src="scripts/index.js"></script>
    <title>PassMS</title>
</head>
    <body>
        <div class="panel">
            <div class="state"><img id="logo" src="images/passms.png"/></div>
            <div class="form">
            <form id="form1" runat="server" onsubmit="">
            <div>
                <asp:TextBox id="TextUsername" runat="server" placeholder="Username"/>
                <asp:TextBox id="TextPassword" runat="server" TextMode="Password" placeholder="Password"/>
                <asp:Button id="ButtonLogin" class="login" runat="server" OnClick="ButtonLogin_Click" Text="Login" />
            </div>
            </form>
            </div>
            <div class="link"><a href="dailytasks.html"><i class="fa fa-question-circle"></i>Forgot password?</a></div>
        </div>
    </body>
</html>