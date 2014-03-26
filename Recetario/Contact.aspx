<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Recetario.Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Recetarios</h2>
    </hgroup>

    <section class="contact">
        <header>
            <h3>Telefono:</h3>
        </header>
        <p>
            <span class="label">Principal:</span>
            <span>2647-5676</span>
        </p>
        <p>
            <span class="label">Celular:</span>
            <span>3336-3942</span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Email:</h3>
        </header>
        <p>
            <span class="label">Soporte:</span>
            <span><a href="mailto:soporte@recetario.com">soporte@recetario.com</a></span>
        </p>
        <p>
            <span class="label">Ventas:</span>
            <span><a href="mailto:ventas@recetario.com">ventas@recetario.com</a></span>
        </p>
        <p>
            <span class="label">Informacion:</span>
            <span><a href="mailto:info@recetario.com">info@recetario.com</a></span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Direccion:</h3>
        </header>
        <p>
            Col. Carcamo, El Progreso Yoro<br />
            Calle principal, casa #1
        </p>
    </section>
</asp:Content>