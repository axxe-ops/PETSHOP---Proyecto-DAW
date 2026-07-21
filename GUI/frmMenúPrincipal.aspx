<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMenúPrincipal.aspx.cs" Inherits="GUI.MenúPrincipal" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Petshop - Menú Principal</title>
    <link href="Estilos/MenuPrincipal_Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        
        <!-- Barra superior -->
        <header>
            <h1>🐾 Petshop System</h1>
            <div class="user-info">
                <span>Hola, <asp:Label ID="lblUsuarioLogueado" runat="server" Text="Usuario"></asp:Label></span>
                <asp:Button ID="btnCerrarSesion" runat="server" Text="Salir" CssClass="btn btn-danger" OnClick="btnCerrarSesion_Click" />
            </div>
        </header>

        <!-- Contenido -->
        <div class="container">
            
            <div class="top-bar">
                <h2>Catálogo de Productos</h2>
                <asp:Button ID="btnVerCarrito" runat="server" Text="🛒 Ver Carrito (0)" CssClass="btn btn-success" OnClick="btnVerCarrito_Click" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>

            <!-- Listado con Repeater -->
            <div class="grid-productos">
                <asp:Repeater ID="rptProductos" runat="server" OnItemCommand="rptProductos_ItemCommand">
                    <ItemTemplate>
                        <div class="card-producto">
                            <div>
                                <!-- Tipo de producto -->
                                <span class='<%# Eval("Tipo").ToString() == "Salud" ? "badge badge-salud" : "badge badge-juguete" %>'>
                                    <%# Eval("Tipo") %>
                                </span>
                                
                                <h3><%# Eval("Nombre") %></h3>
                                <div class="precio">$ <%# Eval("Precio") %></div>
                                <div class="stock">Stock disponible: <%# Eval("StockActual") %></div>
                            </div>

                            <div>
                                <div class="input-group">
                                    <label>Cant:</label>
                                    <asp:TextBox ID="txtCantidad" runat="server" Text="1" TextMode="Number" min="1"></asp:TextBox>
                                </div>
                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar al Carrito" 
                                    CommandName="Agregar" 
                                    CommandArgument='<%# Eval("Id") %>' 
                                    CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

        </div>

    </form>
</body>
</html>
