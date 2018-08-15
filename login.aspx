<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="PassMobile.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" onsubmit="">
        <div>
            <asp:Label runat="server">Username:</asp:Label>
            <asp:TextBox id="TextUsername" runat="server" />
            <asp:Label runat="server">Password:</asp:Label>
            <asp:TextBox id="TextPassword" runat="server" TextMode="Password" />
            <asp:Button id="ButtonLogin" runat="server" OnClick="ButtonLogin_Click" Text="Login" />
        </div>
    </form>
</body>
</html>
