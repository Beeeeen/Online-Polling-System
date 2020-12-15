<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Online_Polling_Project._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
    <!--<asp:TextBox ID="TextBox1" runat="server" Height="96px" TextMode="MultiLine" Width="649px"></asp:TextBox>-->
    
    <script type="text/javascript">

        function OpenRegisterPage()
        {
            window.open('Register.aspx', '_blank');
        }
    </script>
    <asp:TextBox ID="ID_inputBox" runat="server" Height="20px" Text="input ID" Width="150px"></asp:TextBox>
    <asp:TextBox ID="Password_inputBox" runat="server" Height="20px" Text="input password" Width="150px"></asp:TextBox>
    <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButtonClick" />
    <asp:Label ID="LoginButtonLabel" runat="server" Visible="false" Text="Wrong password!" />
    <asp:Button ID="openRegisterPage" runat="server" Text="Don't have account? Register now" OnClientClick="OpenRegisterPage(); return false;" />


</asp:Content>
