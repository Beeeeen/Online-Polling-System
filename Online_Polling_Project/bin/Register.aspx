<%@ Page Title="Register Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Online_Polling_Project.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <address>
        
        <asp:TextBox ID="ID_inputBox" runat="server" Height="20px" Text="input ID" Width="150px"></asp:TextBox>
        <asp:TextBox ID="Name_inputBox" runat="server" Height="20px" Text="input name" Width="150px"></asp:TextBox>
        <asp:TextBox ID="Password_inputBox" runat="server" Height="20px" Text="input password" Width="150px"></asp:TextBox>
        <asp:DropDownList ID="CategoryDropDownList1" runat="server" OnSelectedIndexChanged="Selection_Change" AutoPostBack=True>
            <asp:ListItem Selected="True" Value="student"></asp:ListItem>
            <asp:ListItem Selected="False" Value="professor"></asp:ListItem>
            <asp:ListItem Selected="False" Value="staff"></asp:ListItem>
            <asp:ListItem Selected="False" Value="manager"></asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="authenticationBox" runat="server" Height="20px" Text="Input Authentication Code" Width="200px" Visible="false"></asp:TextBox>        
        <asp:Button ID="RegisterButton" runat="server" Text="Register" OnClick="RegisterButtonClick"/>
        <asp:Label ID="RegisterButtonLabel" runat="server" Visible="false" Text="Hello World!" />
    </address>
</asp:Content>
