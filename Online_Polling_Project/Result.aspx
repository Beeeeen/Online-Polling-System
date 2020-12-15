<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Result.aspx.cs" Inherits="Online_Polling_Project.Result" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>已結束的投票</h2>
            <asp:BulletedList ID="VoteList" runat="server"  BulletStyle="Numbered" DisplayMode="LinkButton" OnClick="BulletedList1_Click" Height="109px" Width="263px">        
    </asp:BulletedList>
            <asp:Label ID="Label1" runat="server" Text="" Visible="false" ></asp:Label><br />
            <asp:Chart ID="Chart1" runat="server">
                <Series>
                    <asp:Series Name="Series1"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>
    </form>
</body>
</html>
