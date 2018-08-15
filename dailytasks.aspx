<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dailytasks.aspx.cs" Inherits="PassMobile.dailytasks" %>

<!DOCTYPE html>
<html>
<head>
    <title></title>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta name="format-detection" content="telephone=no">
    <meta name="msapplication-tap-highlight" content="no">
    <meta name="viewport" content="user-scalable=no, initial-scale=1, maximum-scale=1, minimum-scale=1, width=device-width">
    <link rel='stylesheet' href='fullcalendar/fullcalendar.css' />
    <link rel="stylesheet" type="text/css" href="bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="css/global.css" />
    <link rel="stylesheet" type="text/css" href="css/dailytasks.css" />
    <script src="scripts/global.js"></script>
    <script src='fullcalendar/lib/jquery.min.js'></script>
    <script src='fullcalendar/lib/moment.min.js'></script>
    <script src='fullcalendar/fullcalendar.js'></script>
    <script src="scripts/dailytasks.js"></script>
    <script>
        $(document).ready(function () {
            $.get("sidenav.html", function (data) {
                $("#sidenav").html(data);
            });
        });
    </script>
</head>
<body>
    <div id="sidenav"></div>
    <div id="main">
        <span id="nav-gliph" onclick="openNav()" class="glyphicon glyphicon-th-list"></span>
        <div class="panel">
            <h3>Daily Tasks and Repairs</h3>
            <div id="calendar"></div>
        </div>
    </div>
    
</body>
</html>
