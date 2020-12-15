<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vote.aspx.cs" Inherits="Online_Polling_Project.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2><asp:Literal ID="Literal1" runat="server" Visible="false" Text="進行中的投票活動"></asp:Literal></h2>
     
    <asp:BulletedList ID="VoteList" runat="server"  BulletStyle="Numbered" DisplayMode="LinkButton" OnClick="BulletedList1_Click" Height="109px" Width="263px">        
    </asp:BulletedList>    

    <asp:Label ID="Label1" runat="server" Text="" Visible=false ></asp:Label>
    <asp:Button ID="StartVoteButton" runat="server" Text="開始投票" Visible="false" OnClick="StartVoteButtonClick" /><br />
    <asp:Button ID="ToPermissionManage" runat="server" Text="管理使用者" Visible="false" OnClick="ManageClick" OnClientClick="target ='_blank';"/>
    <asp:Button ID="VoteManage" runat="server" Text="管理投票" Visible="false" OnClick="VoteManageClick" OnClientClick="target ='_blank';"/>
    <asp:Button ID="ResulButton" runat="server" Text="查看已結束投票的結果" Visible="true" OnClick="ResultClick" OnClientClick="target ='_blank';"/>
    
    
    

</asp:Content>
