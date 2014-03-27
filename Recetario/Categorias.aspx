<%@ Page Title="Chef Char | Categorias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="Recetario.Categorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="btnAgregarCategoriaTop" runat="server" Text="Nueva Categoria" OnClick="btnAgregarCategoriaTop_Click" />
    <asp:GridView ID="grvCategorias" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IdCategoria" DataSourceID="sqlDSCategorias" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="IdCategoria" HeaderText="Id. Categoria" InsertVisible="False" ReadOnly="True" SortExpression="IdCategoria" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion">
            <HeaderStyle Width="200px" />
            <ItemStyle Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="Categoria" HeaderText="Categoria" SortExpression="Categoria">
            <HeaderStyle Width="200px" />
            <ItemStyle Width="200px" />
            </asp:BoundField>
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
    <asp:SqlDataSource ID="sqlDSCategorias" runat="server" ConnectionString="<%$ ConnectionStrings:RecetarioConnectionString %>" SelectCommand="SELECT DISTINCT [IdCategoria], [Descripcion], [Categoria], [Inactivo] FROM [Categorias] WHERE ([Eliminado] = @Eliminado)">
        <SelectParameters>
            <asp:Parameter DefaultValue="false" Name="Eliminado" Type="Boolean" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Button ID="btnAgregarCategoriaBotton" runat="server" Text="Nueva Categoria" OnClick="btnAgregarCategoriaBotton_Click" />
</asp:Content>
