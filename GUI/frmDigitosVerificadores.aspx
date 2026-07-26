<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDigitosVerificadores.aspx.cs" Inherits="GUI.frmDigitosVerificadores" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title> PetShop - Integridad Sistema</title>
    <link href="Estilos/DigitosVerificadores_Estilos.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <header class="header-panel">
            <h1>Petshop - Seguridad e Integridad (Webmaster)</h1>
            <asp:Button ID="btnVolver" runat="server" Text="⬅️ Menú Principal" CssClass="btn btn-volver" OnClick="btnVolver_Click" />
        </header>

        <div class="container">
            
            <!-- Cartel grande de Estado de la Integridad de la BD -->
            <asp:Panel ID="pnlEstadoBD" runat="server" CssClass="panel-estado">
                <asp:Label ID="lblEstadoBD" runat="server" Text="Verificando estado de la Base de Datos..." CssClass="texto-estado"></asp:Label>
            </asp:Panel>

            <!-- Botones de Acción Global -->
            <div class="panel-acciones-dv">
                <asp:Button ID="btnVerificar"   runat="server"  Text="Verificar Integridad" CssClass="btn btn-verificar" OnClick="btnVerificar_Click" />
                <asp:Button ID="btnRecalcular"  runat="server"  Text="Recalcular Dígitos Verificadores" CssClass="btn btn-recalcular" OnClick="btnRecalcular_Click" />
                <asp:Button ID="btnRestaurar"   runat="server"  Text="Restaurar Base de Datos (Backup)" CssClass="btn btn-restaurar" OnClick="btnRestaurar_Click" />
            </div>

            <!-- Tabla de Registros / Filas Alteradas -->
            <div class="seccion-alterados">
                <h2>Registros / Filas Alteradas Detectadas</h2>
                <div class="contenedor-tabla-scroll">
                   <asp:GridView ID="gvAlterados" runat="server" AutoGenerateColumns="False" CssClass="tabla-bitacora" EmptyDataText="¡Excelente! No se detectaron alteraciones en los registros.">
                        <Columns>
                            <asp:BoundField DataField="Tabla" HeaderText="Tabla Afectada" />
                            <asp:BoundField DataField="IdFila" HeaderText="ID de Registro" />
                            <asp:BoundField DataField="Incoherencia" HeaderText="Detalle de la Incoherencia" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
