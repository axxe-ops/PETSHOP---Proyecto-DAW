<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCarrito.aspx.cs" Inherits="GUI.frmCarrito" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title> PetShop - Carrito </title>
    <link href="Estilos/Carrito_Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        
        <header>
            <h1>🐾 Petshop System</h1>
            <span>Carrito de Compras</span>
        </header>

        <div class="container">
            <h2>Detalle de tu Pedido</h2>

            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>

            <!-- Si hay productos, mostramos la tabla -->
            <asp:Panel ID="pnlConProductos" runat="server">
                <table class="tabla-carrito">
                    <thead>
                        <tr>
                            <th>Producto</th>
                            <th>Tipo</th>
                            <th>Precio Unit.</th>
                            <th>Cantidad</th>
                            <th>Subtotal</th>
                            <th>Acción</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptCarrito" runat="server" OnItemCommand="rptCarrito_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Producto.Nombre") %></td>
                                    <td><%# Eval("Producto.Tipo") %></td>
                                    <td>$ <%# Eval("Producto.Precio") %></td>
                                    <td><%# Eval("Cantidad") %></td>
                                    <td>$ <%# Eval("SubTotal") %></td>
                                    <td>
                                        <asp:Button ID="btnQuitar" runat="server" Text="Quitar" 
                                            CommandName="Quitar" 
                                            CommandArgument='<%# Eval("Producto.Id") %>' 
                                            CssClass="btn btn-eliminar" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>

                <div class="total-container">
                    Total a Pagar: $ <asp:Label ID="lblTotalGeneral" runat="server" Text="0.00"></asp:Label>
                </div>
            </asp:Panel>

            <!-- Si el carrito está vacío -->
            <asp:Panel ID="pnlCarritoVacio" runat="server" Visible="false" CssClass="carrito-vacio">
                <h3>Tu carrito está vacío</h3>
                <p>Aún no has agregado ningún producto al carrito de compras.</p>
            </asp:Panel>

            <!-- Botones de navegación y acción -->
            <div class="acciones">
                <asp:Button ID="btnSeguirComprando" runat="server" Text="⬅️ Seguir Comprando" CssClass="btn btn-seguir" OnClick="btnSeguirComprando_Click" />
                
                <asp:Button ID="btnConfirmarCompra" runat="server" Text="✅ Confirmar Compra" CssClass="btn btn-confirmar" OnClick="btnConfirmarCompra_Click" />
            </div>

        </div>

    </form>
</body>
</html>
