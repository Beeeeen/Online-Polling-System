<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteVote.aspx.cs" Inherits="Online_Polling_Project.DeleteVote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="Literal1" runat="server" Text="Check topic to delete"></asp:Literal>
            <asp:CheckBoxList ID="VoteList" runat="server"></asp:CheckBoxList>
            <asp:Button ID="DeleteButton" runat="server" Text="確定刪除" OnClick="DeleteButtonClick" />
        </div>
    </form>
    
    
</body>
</html>
