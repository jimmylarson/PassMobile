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
    <div id="data-container">
        <div id="panel-data">
                <form class="modal-data" runat="server" onclick="">
                    <asp:HiddenField ID="ModalEventDetailsId" runat="server" ClientIDMode="Static" />

                    <label class="label-data">Event Name:</label>
                    <asp:Label ID="ModalEventDetailsName" runat="server" ClientIDMode="Static" CssClass="caldata" />

                    <label class="label-data">Event Type:</label>
                    <asp:Label ID="ModalEventDetailsTypeText" runat="server" ClientIDMode="Static" CssClass="caldata" />

                    <label class="label-data">Event Frequency:</label>
                    <asp:Label ID="ModalEventDetailsFrequency" runat="server" ClientIDMode="Static" CssClass="caldata" />

                    <label class="label-data" for="task-complete-action-by">Action By:</label>
                    <asp:Label ID="ModalEventDetailsActionBy" runat="server" ClientIDMode="Static" CssClass="caldata" />

                    <label class="label-data">Date Actioned:</label>
                    <asp:TextBox ID="ModalEventDetailsActionDate" runat="server" ClientIDMode="Static" CssClass="caldata"/>

                    <label class="label-data">Time Actioned:</label>
                    <asp:TextBox ID="ModalEventDetailsActionTime" runat="server" CssClass="caldata"/>

                    <label class="label-data">Completed By:</label>
                    <asp:DropDownList ID="ModalEventDetailsCompletedBy" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="caldata"></asp:DropDownList>
                    
                    <label class="label-data">Completion Notes:</label>
                    <asp:TextBox ID="ModalEventDetailsNotes" runat="server" AutoPostBack="false" ClientIDMode="Static" TextMode="MultiLine" Columns="50" Rows="3" CssClass="form-control"></asp:TextBox>
                    <asp:HiddenField ID="ModalEventDetailsType" runat="server" ClientIDMode="Static" Value="" />

                    <asp:Button ID="ModalEventDetailsComplete"
                        runat="server" ClientIDMode="Static"
                        Text="Complete" OnClick="ModalEventDetailsComplete_Click"
                        OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled=true; this.value='working...';"
                        UseSubmitBehavior="false" CssClass="data-buttons" />

                    <asp:Button ID="ModalEventDetailsDelete" runat="server" Text="Delete" OnClick="ModalEventDetailsDelete_Click" CssClass="data-buttons" />


                    <button type="button" id="cancel" class="data-buttons" onclick="closeData()">Cancel</button>
                </form>
            </div>
    </div>


    <div id="main">
        <span id="nav-gliph" onclick="openNav()" class="glyphicon glyphicon-th-list"></span>
        <div class="panel">
            <h3>Daily Tasks and Repairs</h3>
            <div id="calendar"></div>
        </div>
    </div>

</body>
</html>
