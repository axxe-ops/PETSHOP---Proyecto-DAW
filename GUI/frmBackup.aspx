<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmBackup.aspx.cs" Inherits="GUI.frmBackup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>PetShop - Backup</title>
    <link href="Estilos/Backup_Estilos.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Gestión de Respaldos y Restauración de Base de Datos</h2>

            <!-- SECCIÓN 1: CREAR BACKUP -->
            <div class="seccion-panel">
                <h3>Generar Nuevo Respaldo (Full)</h3>
                <asp:Button ID="btnHacerBackup" runat="server" Text="Crear Backup Full" OnClick="btnHacerBackup_Click" CssClass="btn-primary-custom" />
                <asp:Label ID="lblMensajeBackup" runat="server" />
            </div>

            <hr />

            <!-- SECCIÓN 2: RESTAURAR BACKUP -->
            <div class="seccion-panel">
                <h3>Historial de Backups Disponibles</h3>
                <asp:GridView ID="gvBackups" runat="server" AutoGenerateColumns="False" CssClass="tabla-backups" OnRowCommand="gvBackups_RowCommand" EmptyDataText="No hay respaldos registrados en el sistema.">
                    <Columns>
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha y Hora" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                        <asp:BoundField DataField="RutaArchivo" HeaderText="Ubicación del Archivo" />
                        <asp:TemplateField HeaderText="Acción">
                            <ItemTemplate>
                                <asp:Button ID="btnRestaurarItem" runat="server" Text="Restaurar esta versión" CommandName="RestaurarBD" CommandArgument='<%# Eval("RutaArchivo") %>' CssClass="btn-danger-custom" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblMensajeRestaurar" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
