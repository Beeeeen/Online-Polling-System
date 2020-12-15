<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyVote.aspx.cs" Inherits="Online_Polling_Project.ModifyVote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" >
        <div>
            <h2><asp:Literal ID="Literal1" runat="server" Visible="true" Text="進行中的投票活動 點擊去修改!"></asp:Literal></h2>
     
            <asp:BulletedList ID="VoteList" runat="server"  BulletStyle="Numbered" DisplayMode="LinkButton" OnClick="BulletedList1_Click" Height="109px" Width="263px">        
            </asp:BulletedList>    

            <asp:Label ID="Label1" runat="server" Text="" Visible="false" ></asp:Label>            
            <asp:Button ID="StartModifyButton" runat="server" Text="開始修改" Visible="false" OnClick="StartModifyButtonClick" /><br />
        </div>
    </form>
</body>
</html>
