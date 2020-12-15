<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddVote.aspx.cs" Inherits="Online_Polling_Project.AddVote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.4/themes/trontastic/jquery-ui.css"/>
    <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#StartDate").datepicker({
                minDate: 0,   
            });
            $("#EndDate").datepicker({
                minDate: 0,   
            });
            
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>新增投票</h2>
            <asp:TextBox ID="TextBox1" runat="server" Text="請輸入主題" Height="30" Width="300"></asp:TextBox><br /><br />
            <asp:TextBox ID="TextBox2" runat="server" Text="請輸入說明" Height="100" Width="300"></asp:TextBox><br /><br />
            <asp:TextBox ID="StartDate" runat="server" Text="請輸入開始日期"></asp:TextBox><br />
            <asp:TextBox ID="EndDate" runat="server" Text="請輸入結束日期"></asp:TextBox><br /><br />
            <h3>投票人身分
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" OnSelectedIndexChanged="Selection_Change" AutoPostBack="true">
                    <asp:ListItem>記名/不記名</asp:ListItem>
                    <asp:ListItem>manager</asp:ListItem>
                    <asp:ListItem>student</asp:ListItem>
                    <asp:ListItem>professor</asp:ListItem>
                    <asp:ListItem>staff</asp:ListItem>
                </asp:CheckBoxList>
            </h3>
            <h4>選項<br />
                <asp:PlaceHolder runat="server" id="LabelPlaceHolder" />
                <asp:Button ID="Button" runat="server" Text="新增選項" OnClick="AddOptionClick"  /><br />                
            </h4>            
            <asp:Button ID="BuildButton" runat="server" Text="確定建立" OnClick="BuildClick"  height="50" width="100"/><br />
            
            <asp:Literal ID="Literal1" runat="server" Text="" ></asp:Literal>
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
