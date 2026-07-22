<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmGestionPedidos.aspx.cs" Inherits="GUI.frmGestionPedidos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>PetShop - Gestión Pedidos</title>
    <link href="Estilos/GestionPedidos_Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <h1>🐾 Petshop - Panel de Administración</h1>
            <asp:Button ID="btnVolver" runat="server" Text="⬅️ Menú Principal" CssClass="btn btn-detalle" OnClick="btnVolver_Click" />
        </header>

        <div class="container">
            <h2>Gestión de Pedidos de Clientes</h2>
            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>

            <!-- Tabla de Listado de Pedidos -->
            <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="False" CssClass="tabla-pedidos" OnRowCommand="gvPedidos_RowCommand" DataKeyNames="Id">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID Pedido" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:BoundField DataField="Cliente.Nombre" HeaderText="Cliente (Usuario)" />
                    <asp:BoundField DataField="Cliente.Email" HeaderText="Email" />
                    <asp:BoundField DataField="MontoTotal" HeaderText="Total" DataFormatString="$ {0:0.00}" />
                    
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class='<%# ObtenerClaseBadge(Eval("Estado").ToString()) %>'>
                                <%# Eval("Estado") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="Cambiar Estado">
                        <ItemTemplate>
                            <!-- Desplegable que precarga el estado actual del pedido -->
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="select-estado" SelectedValue='<%# Eval("Estado") %>'>
                                <asp:ListItem Text="Pendiente" Value="Pendiente" />
                                <asp:ListItem Text="Aprobado" Value="Aprobado" />
                                <asp:ListItem Text="Enviado" Value="Enviado" />
                                <asp:ListItem Text="Entregado" Value="Entregado" />
                                <asp:ListItem Text="Cancelado" Value="Cancelado" />
                            </asp:DropDownList>

                            <!-- Único botón para guardar el cambio de ese pedido -->
                            <asp:Button ID="btnActualizar" runat="server" Text="💾 Guardar" CommandName="ActualizarEstado" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-actualizar" />
        
                            <!-- Botón Ver Detalle limpio al lado -->
                            <asp:Button ID="btnVer" runat="server" Text="🔍 Detalle" CommandName="VerDetalle" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-detalle" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <!-- Panel para ver el detalle de un pedido seleccionado -->
            <asp:Panel ID="pnlDetalle" runat="server" Visible="false" CssClass="detalle-container">
                <h3>Detalle del Pedido #<asp:Label ID="lblIdPedidoDetalle" runat="server"></asp:Label></h3>
                <p><strong>Cliente:</strong> <asp:Label ID="lblClienteDetalle" runat="server"></asp:Label></p>
                <p><strong>Fecha:</strong> <asp:Label ID="lblFechaDetalle" runat="server"></asp:Label></p>
                
                <asp:GridView ID="gvDetalleItems" runat="server" AutoGenerateColumns="False" CssClass="tabla-pedidos">
                    <Columns>
                        <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                        <asp:BoundField DataField="Producto.Tipo" HeaderText="Tipo" />
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="$ {0:0.00}" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
