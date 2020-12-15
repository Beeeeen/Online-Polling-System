<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PermissionManagement.aspx.cs" Inherits="Online_Polling_Project.PermissionManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Literal ID="Literal1" runat="server" Text="You can decide their permission of design new vote"></asp:Literal><br />    
    <asp:CheckBoxList ID="UserChecklist" runat="server" AutoPostBack="true" OnSelectedIndexChanged = "CheckBoxIndexChanged" ></asp:CheckBoxList>

</asp:Content>
