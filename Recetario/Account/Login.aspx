<%@ Page Title="Recetario - Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Recetario.Account.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .tabla
        {
            width: 500px;
        }
        .etiqueta
        {
            width: 150px;
            text-align: right;
            padding: 0px;
            margin: 0px;
        }
        .espacio
        {
            width: 10px;
            padding: 0px;
            margin: 0px;
        }
        .textos
        {
            padding: 0px;
            margin: 0px;
        }
        .auto-style3
        {
            width: 150px;
            text-align: right;
            padding: 0px;
            margin: 0px;
            height: 64px;
        }
        .auto-style4
        {
            height: 64px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table align="center" cellpadding="0" cellspacing="0" class="tabla">
        <tr>
            <td class="etiqueta">
                <asp:Label ID="Label1" runat="server" Text="Usuario"></asp:Label>
            </td>
            <td class="espacio">&nbsp;</td>
            <td class="textos">
                <asp:TextBox ID="txtUser" runat="server" Width="256px" ValidationGroup="login"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUser" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="#990000" SetFocusOnError="True" ValidationGroup="login"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Label ID="Label2" runat="server" Text="Contrasena"></asp:Label>
            </td>
            <td class="auto-style4"></td>
            <td class="auto-style4">
                <asp:TextBox ID="txtPass" runat="server" TextMode="Password" Width="256px" ValidationGroup="login"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPass" ErrorMessage="Campo Requerido" ForeColor="#990000" SetFocusOnError="True" ValidationGroup="login"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                <asp:HyperLink ID="HyperLink1" runat="server">Olvide mi Contrasena</asp:HyperLink>
                <br/>
                <asp:HyperLink ID="HyperLink2" runat="server">Registrarme</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
