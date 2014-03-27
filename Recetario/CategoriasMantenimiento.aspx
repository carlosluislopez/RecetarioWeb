<%@ Page Title="Chef Char | Categorias - Mantenimiento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategoriasMantenimiento.aspx.cs" Inherits="Recetario.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <table align="center" cellpadding="0" cellspacing="0" style="width: 60%;">
        <tr>
            <td valign="top">Categoria</td>
            <td style="width: 5px;">&nbsp;</td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top">Descripcion</td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="txtDescripcion" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top">Inactivo</td>
            <td>&nbsp;</td>
            <td>
                <asp:CheckBox ID="chkInactivo" runat="server" Text=" " />
            </td>
        </tr>
        <tr>
            <td valign="top" colspan="3" style="text-align: center;">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RecetarioConnectionString %>" InsertCommand="INSERT INTO Categorias(Categoria, Descripcion, Eliminado, Inactivo) VALUES (@Categoria, @Descripcion, @Eliminado, @Inactivo)" SelectCommand="SELECT Categoria, Descripcion, Inactivo FROM Categorias WHERE (Eliminado = @Eliminado) AND (IdCategoria = @IdCategoria)" UpdateCommand="UPDATE Categorias SET Categoria = @Categoria, Descripcion = @Descripcion, Eliminado = @Eliminado, Inactivo = @Inactivo WHERE (IdCategoria = @IdCategoria)">
                    <InsertParameters>
                        <asp:ControlParameter ControlID="txtNombre" Name="Categoria" PropertyName="Text" />
                        <asp:ControlParameter ControlID="txtDescripcion" Name="Descripcion" PropertyName="Text" />
                        <asp:Parameter DefaultValue="False" Name="Eliminado" />
                        <asp:ControlParameter ControlID="chkInactivo" Name="Inactivo" PropertyName="Checked" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:Parameter DefaultValue="False" Name="Eliminado" Type="Boolean" />
                        <asp:QueryStringParameter DefaultValue="-1" Name="IdCategoria" QueryStringField="ID" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:ControlParameter ControlID="txtNombre" Name="Categoria" PropertyName="Text" />
                        <asp:ControlParameter ControlID="txtDescripcion" Name="Descripcion" PropertyName="Text" />
                        <asp:Parameter DefaultValue="False" Name="Eliminado" />
                        <asp:ControlParameter ControlID="chkInactivo" Name="Inactivo" PropertyName="Checked" />
                        <asp:QueryStringParameter DefaultValue="-1" Name="IdCategoria" QueryStringField="ID" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    
</asp:Content>
