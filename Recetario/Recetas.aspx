<%@ Page Title="Recetas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Recetas.aspx.cs" Inherits="Recetario.Recetas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="btnAgregarRecetaTop" runat="server" Text="Nueva Receta" OnClick="btnAgregarRecetaTop_Click" />
    <asp:GridView ID="grvRecetas" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdReceta" DataSourceID="sqlDSRecetas" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="grvRecetas_RowDataBound">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="IdReceta" HeaderText="Id. Receta" InsertVisible="False" ReadOnly="True" SortExpression="IdReceta" />
            <asp:BoundField DataField="Categoria" HeaderText="Categoria" SortExpression="Categoria">
            <HeaderStyle Width="150px" />
            <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="Nombre" HeaderText="Receta" SortExpression="Nombre">
            <HeaderStyle Width="150px" />
            <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Foto">
                <ItemTemplate>
                    <asp:Image ID="imgFoto" runat="server" Height="100px" ImageAlign="AbsMiddle" Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="Inactivo" HeaderText="Inactivo" SortExpression="Inactivo" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <asp:SqlDataSource ID="sqlDSRecetas" runat="server" ConnectionString="<%$ ConnectionStrings:RecetarioConnectionString %>" SelectCommand="SELECT Recetas.IdReceta, Categorias.Categoria, Recetas.Nombre, Recetas.Inactivo, Fotos.Foto FROM Recetas INNER JOIN Categorias ON Categorias.IdCategoria = Recetas.IdCategoria INNER JOIN Fotos ON Recetas.IdReceta = Fotos.IdReceta WHERE (Recetas.Eliminado = 0)"></asp:SqlDataSource>
    <asp:Button ID="btnAgregarRecetaBotton" runat="server" Text="Nueva Receta" OnClick="btnAgregarRecetaBotton_Click" />
</asp:Content>
