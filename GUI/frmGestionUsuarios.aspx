<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmGestionUsuarios.aspx.cs" Inherits="GUI.frmGestionUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title> PetShop - Gestion Usuarios </title>
    <link href="Estilos/GestionUsuarios_Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <h1>Petshop - Administración de Usuarios</h1>
            <asp:Button ID="btnVolver" runat="server" Text="⬅️ Menú Principal" CssClass="btn btn-volver" OnClick="btnVolver_Click" />
        </header>

        <div class="container">
            <div class="header-acciones">
                <h2>Gestión de Cuentas y Permisos</h2>
                <asp:Button ID="btnModoNuevo" runat="server" Text="➕ Nuevo Usuario" CssClass="btn btn-success" OnClick="btnModoNuevo_Click" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>

            <div class="layout-abm">
                <!-- Columna Izquierda: Grilla de Usuarios -->
                <div class="col-tabla">
                    <h3>Listado de Usuarios Registrados</h3>
                    <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" CssClass="tabla-productos" 
                        OnRowCommand="gvUsuarios_RowCommand" DataKeyNames="Id">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Permiso" HeaderText="Permiso / Rol" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button ID="btnSeleccionar" runat="server" Text="✏️ Modificar" CommandName="Seleccionar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-editar" />
                                    <asp:Button ID="btnEliminar" runat="server" Text="🗑️ Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-eliminar" OnClientClick="return confirm('¿Estás seguro de eliminar este usuario?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <!-- Columna Derecha: Formulario Dinámico (Modificar / Nuevo) -->
                <div class="col-form">
                    <h3><asp:Label ID="lblTituloForm" runat="server" Text="Gestión de Usuario"></asp:Label></h3>
                    
                    <asp:HiddenField ID="hfIdUsuario" runat="server" />

                    <div class="form-group">
                        <label>Nombre de Usuario:</label>
                        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Contraseña:</label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Permiso / Rol:</label>
                        <asp:DropDownList ID="ddlPermiso" runat="server">
                            <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                            <asp:ListItem Text="Usuario" Value="Usuario"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label>Email:</label>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Teléfono:</label>
                        <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
                    </div>

                    <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar Cambios" CssClass="btn btn-guardar" OnClick="btnGuardarCambios_Click" Visible="false" />
                    <asp:Button ID="btnRegistrarNuevo" runat="server" Text="Registrar Nuevo Usuario" CssClass="btn btn-registrar" OnClick="btnRegistrarNuevo_Click" Visible="false" />
                    <asp:Button ID="btnCancelarEdicion" runat="server" Text="Cancelar" CssClass="btn btn-cancelar" OnClick="btnCancelarEdicion_Click" Visible="false" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
