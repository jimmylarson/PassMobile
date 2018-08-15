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

            <div class="modal fade" id="modal-event-task-select" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Change the Date & Add Notes</h4>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="ModalEventDetailsId" runat="server" ClientIDMode="Static" Value="" />
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="control-label col-sm-4">Event Name:</label>
                                <asp:Label ID="ModalEventDetailsName" runat="server" ClientIDMode="Static" CssClass="col-sm-8" />
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-4">Event Type:</label>
                                <asp:Label ID="ModalEventDetailsTypeText" runat="server" ClientIDMode="Static" CssClass="col-sm-8" />
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-4">Event Frequency:</label>
                                <asp:Label ID="ModalEventDetailsFrequency" runat="server" ClientIDMode="Static" CssClass="col-sm-8" />
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-4" for="task-complete-action-by">Action By:</label>
                                <asp:Label ID="ModalEventDetailsActionBy" runat="server" ClientIDMode="Static" CssClass="col-sm-8" />
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-4">Date Actioned:</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="ModalEventDetailsActionDate" runat="server" ClientIDMode="Static" />                                
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-4">Time Actioned:</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="ModalEventDetailsActionTime" runat="server"/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-4">Completed By:</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ModalEventDetailsCompletedBy" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="pass-asp-drop-down-list"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-4">Completion Notes:</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="ModalEventDetailsNotes" runat="server" AutoPostBack="false" ClientIDMode="Static" TextMode="MultiLine" Columns="50" Rows="3" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:HiddenField ID="ModalEventDetailsType" runat="server" ClientIDMode="Static" Value="" />
                            </div>
                            <div class="form-group">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-3">
                                    <asp:Button ID="ModalEventDetailsComplete"
                                        runat="server" ClientIDMode="Static" 
                                        Text="Complete" OnClick="ModalEventDetailsComplete_Click"
                                        OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled=true; this.value='working...';"
                                        UseSubmitBehavior="false" CssClass="btn btn-primary btn-block btn-sm" />
                                </div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2">
                                    <asp:Button ID="ModalEventDetailsDelete" runat="server" Text="Delete" OnClick="ModalEventDetailsDelete_Click" CssClass="btn btn-primary btn-block btn-sm" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" onclick="return false;" data-dismiss="modal">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    
</body>
</html>
