<%@ Page Title="Chef Char | Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Recetario.Account.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style3
        {
            width: 500px;
        }
        .auto-style4
        {
            height: 47px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table align="center" cellpadding="0" cellspacing="0" class="auto-style3">
        <tr>
            <td style="width: 110px;">
                <asp:Label ID="Label1" runat="server" Text="Usuario"></asp:Label>
            </td>
            <td style="width: 10px;">&nbsp;</td>
            <td>
                <asp:TextBox ID="txtUsuario" runat="server" MaxLength="80" ValidationGroup="register"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsuario" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="#990000" SetFocusOnError="True" ValidationGroup="register"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                <asp:Label ID="Label2" runat="server" Text="Correo Electronico"></asp:Label>
            </td>
            <td class="auto-style4"></td>
            <td class="auto-style4">
                <asp:TextBox ID="txtCorreo" runat="server" MaxLength="100" ValidationGroup="register"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCorreo" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="#990000" SetFocusOnError="True" ValidationGroup="register"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCorreo" Display="Dynamic" ErrorMessage="Direccion de correo invalida" ForeColor="#990000" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="register"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Contrasena"></asp:Label>
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="txtPass" runat="server" MaxLength="20" TextMode="Password" ValidationGroup="register"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPass" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="#990000" SetFocusOnError="True" ValidationGroup="register"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                <asp:Label ID="Label4" runat="server" Text="Confirmar contrasena"></asp:Label>
            </td>
            <td class="auto-style4"></td>
            <td class="auto-style4">
                <asp:TextBox ID="txtPass2" runat="server" MaxLength="20" TextMode="Password" ValidationGroup="register"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPass2" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="#990000" SetFocusOnError="True" ValidationGroup="register"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPass" ControlToValidate="txtPass2" Display="Dynamic" ErrorMessage="Contrasenas no concuerdan" ForeColor="#990000" SetFocusOnError="True" ValidationGroup="register"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Nombre Completo"></asp:Label>
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" MaxLength="120" ValidationGroup="register"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="Campo Requerido" ForeColor="#990000" SetFocusOnError="True" ValidationGroup="register"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Foto"></asp:Label>
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:FileUpload ID="fileFoto" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblError" runat="server" ForeColor="#990000"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <asp:Button ID="btnCrear" runat="server" Text="Crear Usuario" OnClick="btnCrear_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
