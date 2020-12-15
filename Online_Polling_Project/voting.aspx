<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="voting.aspx.cs" Inherits="Online_Polling_Project.voting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

    <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged = "CheckBoxIndexChanged" ></asp:CheckBoxList>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <asp:Button ID="Button1" runat="server" Text="送出" Visible=false OnClick="VoteButtonClick" />

</asp:Content>
