<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="GUI.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>PETSHOP</title>
    <link href="Estilos/Login_estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-contenedor">
            <h2>Iniciar Sesión</h2>
        
            <div class="campo">                
                <div>
                    <label>Usuario:</label>
                </div>
                <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
            </div>
        
            <div class="campo">
                <div>
                    <label>Contraseña:</label>
                </div>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </div>
        
            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" OnClick="btnLogin_Click" />
                
            <br />
            <br />
                
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>            
        </div>
    </form>
</body>
</html>
