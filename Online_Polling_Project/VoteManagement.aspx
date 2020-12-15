<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VoteManagement.aspx.cs" Inherits="Online_Polling_Project.VoteManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button1" runat="server" Text="新增投票" OnClick="AddVoteClick"/>
    <asp:Button ID="Button2" runat="server" Text="修改投票" OnClick="ModifyVoteClick" />
    <asp:Button ID="Button3" runat="server" Text="刪除投票" OnClick="DeleteVoteClick"/>
    
</asp:Content>
