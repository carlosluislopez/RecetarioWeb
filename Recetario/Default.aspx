<%@ Page Title="Chef Char | Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Recetario._Default" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/banner.jpg" />
    
    <asp:DataList ID="DataList1" runat="server" DataKeyField="IdReceta" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="Both" HorizontalAlign="Center" RepeatColumns="4" RepeatDirection="Horizontal">
        <AlternatingItemStyle BackColor="White" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <ItemStyle BackColor="#E3EAEB" />
        <ItemTemplate>
            <asp:HiddenField ID="HFIdReceta" runat="server" Value='<%# Eval("IdReceta") %>' />
            <asp:Label ID="NombreLabel" runat="server" Text='<%# Eval("Nombre") %>' />
            <br />
            <asp:ImageButton ID="btnReceta" runat="server" ImageUrl='<%# Eval("Url") %>' Height="200px" Width="200px" />
            <%--<asp:Image ID="imgReceta" runat="server" ImageUrl='<%# Eval("Url") %>' AlternateText="Imagen" DescriptionUrl="Foto de la receta" GenerateEmptyAlternateText="True" Height="200px" Width="200px" />--%>
            <br />
            Categoria:
            <asp:Label ID="CategoriaLabel" runat="server" Text='<%# Eval("Categoria") %>' />
            <br />
            Fecha:
            <asp:Label ID="Fecha_CreoLabel" runat="server" Text='<%# DateTime.Parse(Eval("Fecha_Creo").ToString()).ToShortDateString() %>' />
            <br />
            Usuario:
            <asp:Label ID="UsuarioLabel" runat="server" Text='<%# Eval("Usuario") %>' />
            &nbsp;-&nbsp;<asp:Label ID="NombreCompletoLabel" runat="server" Text='<%# Eval("NombreCompleto") %>' />
            <br />
            Calificacion:
            <asp:Label ID="RatingLabel" runat="server" Text='<%#  Eval("Rating") %>' />
        </ItemTemplate>
        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
    </asp:DataList>

    
    <asp:HiddenField ID="fieldBusqueda" runat="server" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RecetarioConnectionString %>" SelectCommand="SELECT Recetas.IdReceta, Categorias.Categoria, Recetas.Nombre, Recetas.Descripcion, CAST(Recetas.Fecha_Creo AS date) AS Fecha_Creo, Usuarios.Usuario, Usuarios.NombreCompleto, AVG(Rating.Rating) AS Rating, Fotos.Url FROM Recetas AS Recetas WITH (nolock) INNER JOIN Categorias AS Categorias WITH (nolock) ON Categorias.IdCategoria = Recetas.IdCategoria AND Recetas.Eliminado = 0 AND Recetas.Inactivo = 0 AND Categorias.Inactivo = 0 AND Categorias.Eliminado = 0 INNER JOIN Usuarios AS Usuarios WITH (nolock) ON Usuarios.IdUsuario = Recetas.IdUsuario_Creo AND Usuarios.Eliminado = 0 AND Usuarios.Inactivo = 0 INNER JOIN Fotos AS Fotos WITH (nolock) ON Recetas.IdReceta = Fotos.IdReceta LEFT OUTER JOIN Rating AS Rating WITH (nolock) ON Rating.IdReceta = Recetas.IdReceta GROUP BY Recetas.IdReceta, Categorias.Categoria, Recetas.Nombre, Recetas.Descripcion, Recetas.Fecha_Creo, Usuarios.Usuario, Usuarios.NombreCompleto, Fotos.Url ORDER BY Fecha_Creo DESC">
    </asp:SqlDataSource>
</asp:Content>
