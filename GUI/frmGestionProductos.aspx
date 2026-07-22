<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmGestionProductos.aspx.cs" Inherits="GUI.frmGestionProductos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title> PetShop - Gestion Productos</title>
    <link href="Estilos/GestionProductos_Estilos.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <header>
            <h1>Petshop - Administración de Catálogo</h1>
            <asp:Button ID="btnVolver" runat="server" Text="⬅️ Menú Principal" CssClass="btn btn-volver" OnClick="btnVolver_Click" />
        </header>

        <div class="container">
            <h2>Gestión de Stock, Precios y Detalles de Productos</h2>
            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>

            <div class="layout-abm">
                <!-- Columna Izquierda: Tabla / Cuadrito de Productos -->
                <div class="col-tabla">
                    <h3>Listado de Productos Actuales</h3>
                    <asp:Label ID="lblAlertaStock" runat="server" CssClass="mensaje-alerta-global" Visible="false"></asp:Label>
                    <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" CssClass="tabla-productos" 
                        OnRowCommand="gvProductos_RowCommand" OnRowDataBound="gvProductos_RowDataBound" DataKeyNames="Id">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="$ {0:0.00}" />
                            <asp:BoundField DataField="StockActual" HeaderText="Stock" />
                            <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mín." />
                            <asp:TemplateField HeaderText="Acción">
                                <ItemTemplate>
                                    <asp:Button ID="btnSeleccionar" runat="server" Text="✏️ Modificar" CommandName="Seleccionar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-editar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <!-- Columna Derecha: Formulario de Modificación (Fuera del cuadrito) -->
                <div class="col-form">
                    <h3>Modificar Producto</h3>
                    
                    <!-- Campo oculto para guardar el ID del producto que se está editando -->
                    <asp:HiddenField ID="hfIdProducto" runat="server" />

                    <div class="form-group">
                        <label>Nombre del Producto:</label>
                        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Tipo (Ej: Salud, Juguete):</label>
                        <asp:TextBox ID="txtTipo" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Precio ($):</label>
                        <asp:TextBox ID="txtPrecio" runat="server" TextMode="Number" step="0.01"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Stock Actual:</label>
                        <asp:TextBox ID="txtStock" runat="server" TextMode="Number"></asp:TextBox>
                    </div>

                    <asp:Button ID="btnGuardarCambios" runat="server" Text="💾 Guardar Cambios" CssClass="btn btn-guardar" OnClick="btnGuardarCambios_Click" Enabled="false" />
                    <asp:Button ID="btnCancelarEdicion" runat="server" Text="❌ Cancelar" CssClass="btn btn-cancelar" OnClick="btnCancelarEdicion_Click" Visible="false" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
