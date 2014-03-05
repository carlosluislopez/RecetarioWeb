<%@ Page Title="Recetas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecetasMantenimiento.aspx.cs" Inherits="Recetario.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <table align="center" cellpadding="0" cellspacing="0" style="width: 60%;">
        <tr>
            <td valign="top">Nombre</td>
            <td style="width: 5px;">&nbsp;</td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top">Categoria</td>
            <td style="width: 5px;">&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboCategoria" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="Categoria" DataValueField="IdCategoria">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:RecetarioConnectionString %>" SelectCommand="SELECT [IdCategoria], [Categoria] FROM [Categorias] WHERE (([Eliminado] = @Eliminado) AND ([Inactivo] = @Inactivo))">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="False" Name="Eliminado" Type="Boolean" />
                        <asp:Parameter DefaultValue="False" Name="Inactivo" Type="Boolean" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        
        <tr>
            <td valign="top">Descripcion</td>
            <td style="width: 5px;">&nbsp;</td>
            <td>
                <asp:TextBox ID="txtDescripcion" runat="server" Rows="10" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td valign="top">Fotos</td>
            <td style="width: 5px;">&nbsp;</td>
            <td>
                <asp:FileUpload ID="fileFoto" runat="server" />
            &nbsp;<asp:Button ID="btnSubirImagen" runat="server" OnClick="btnSubirImagen_Click" Text="Subir" />
               <br/><asp:Label ID="lblDireccion" runat="server"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td valign="top">Inactiva&nbsp;</td>
            <td style="width: 5px;">&nbsp;</td>
            <td>
                <asp:CheckBox ID="chkInactivo" runat="server" Text=" " />
            </td>
        </tr>
        
        <tr>
            <td valign="top" style="text-align: center;" colspan="3">
                <asp:Image ID="imgFoto" runat="server" Height="100px" Width="100px" />
            &nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
        
        <tr>
            <td valign="top" colspan="3" style="text-align: center;">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RecetarioConnectionString %>" InsertCommand="INSERT INTO Recetas(IdCategoria, Nombre, Descripcion, Eliminado, Inactivo, Foto) VALUES (@IdCategoria, @Nombre, @Descripcion, @Eliminado, @Inactivo, @Foto)" SelectCommand="SELECT IdCategoria, Nombre, Descripcion, Inactivo, Foto FROM Recetas WHERE (IdReceta = @IdReceta) AND (Eliminado = @Eliminado)" UpdateCommand="UPDATE Recetas SET Nombre = @Nombre, Descripcion = @Descripcion, Eliminado = @Eliminado, Inactivo = @Inactivo, IdCategoria = @IdCategoria, Foto = @Foto WHERE (IdReceta = @IdReceta)">
                    <InsertParameters>
                        <asp:ControlParameter ControlID="cboCategoria" Name="IdCategoria" PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="txtNombre" Name="Nombre" PropertyName="Text" />
                        <asp:ControlParameter ControlID="txtDescripcion" Name="Descripcion" PropertyName="Text" />
                        <asp:Parameter DefaultValue="False" Name="Eliminado" />
                        <asp:ControlParameter ControlID="chkInactivo" DefaultValue="False" Name="Inactivo" PropertyName="Checked" />
                        <asp:ControlParameter ControlID="lblDireccion" Name="Foto" PropertyName="Text" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="IdReceta" QueryStringField="ID" Type="Int32" />
                        <asp:Parameter DefaultValue="False" Name="Eliminado" Type="Boolean" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:ControlParameter ControlID="txtNombre" Name="Nombre" PropertyName="Text" />
                        <asp:ControlParameter ControlID="txtDescripcion" Name="Descripcion" PropertyName="Text" />
                        <asp:Parameter DefaultValue="False" Name="Eliminado" Type="Boolean" />
                        <asp:ControlParameter ControlID="chkInactivo" DefaultValue="False" Name="Inactivo" PropertyName="Checked" Type="Boolean" />
                        <asp:ControlParameter ControlID="cboCategoria" Name="IdCategoria" PropertyName="SelectedValue" DefaultValue="" />
                        <asp:QueryStringParameter DefaultValue="-1" Name="IdReceta" QueryStringField="ID" />
                        <asp:ControlParameter ControlID="lblDireccion" Name="Foto" PropertyName="Text" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        
        <tr>
            <td valign="top" colspan="3" style="text-align: center;">
                &nbsp;</td>
        </tr>
        
    </table>
    
</asp:Content>
