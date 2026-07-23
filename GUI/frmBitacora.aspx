<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmBitacora.aspx.cs" Inherits="GUI.frmBitacora" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>PetShop - Bitacora</title>
    <link href="Estilos/Bitacora_Estilos.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <h1>Petshop - Panel de Auditoría</h1>
            <asp:Button ID="btnVolver" runat="server" Text="⬅️ Menú Principal" CssClass="btn btn-volver" OnClick="btnVolver_Click" />
        </header>

        <div class="container">
            <div class="header-acciones">
                <h2>Registro de Eventos y Actividad del Sistema</h2>
            </div>

            <div class="panel-filtros">
                <div class="filtro-grupo">
                    <label>Usuario:</label>
                    <asp:TextBox ID="txtFiltroUsuario" runat="server" CssClass="input-control" placeholder="Nombre..."></asp:TextBox>
                </div>
                <div class="filtro-grupo">
                    <label>Criticidad:</label>
                    <asp:DropDownList ID="ddlFiltroCriticidad" runat="server" CssClass="input-control">
                        <asp:ListItem Text="Todas" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Nivel 1 (Info)" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Nivel 2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Nivel 3 (Advertencia)" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Nivel 4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Nivel 5 (Crítico)" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="filtro-grupo">
                    <label>Fecha Desde:</label>
                    <asp:TextBox ID="txtFiltroFecha" runat="server" TextMode="Date" CssClass="input-control"></asp:TextBox>
                </div>
                <div class="filtro-grupo">
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-filtrar" OnClick="btnFiltrar_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-limpiar" OnClick="btnLimpiar_Click" />
                </div>
            </div>

            <div class="contenedor-tabla-scroll">
                <asp:GridView ID="gvBitacora" runat="server" AutoGenerateColumns="False" CssClass="tabla-bitacora" OnRowDataBound="gvBitacora_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="FechaHora" HeaderText="Fecha y Hora" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                        <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />
                        <asp:TemplateField HeaderText="Criticidad (1-5)">
                            <ItemTemplate>
                                <asp:Label ID="lblCriticidad" runat="server" Text='<%# Eval("Criticidad") %>' CssClass="badge-criticidad"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción de la Accion" />
                    </Columns>
                </asp:GridView>
            </div>

            
        </div>
    </form>
</body>
</html>
