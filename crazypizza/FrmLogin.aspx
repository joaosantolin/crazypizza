<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmLogin.aspx.cs" Inherits="crazypizza.FrmLogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <link rel="icon" type="img" href="img/logocrazy3.jpg" />
    <title>Login - CrazyPizza</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</head>

<body class="d-flex align-items-center py-4 bg-body-tertiary login-bg">
    <form id="form1" runat="server" class="form-signin login-card">

        <div class="text-center mb-3">
            <img src="img/logocrazy3.jpg" alt="CrazyPizza" class="logo-login" />
        </div>
        <h1 class="h3 mb-3 text-center">Acessar o sistema</h1>

        <!-- Login -->
        <label for="txtUsuario" class="sr-only">Login</label>
        <input type="text" id="txtUsuario" runat="server" class="form-control mb-3" placeholder="Login" required autofocus />

        <!-- Senha -->
        <label for="txtPassword" class="sr-only">Senha</label>
        <input type="password" id="txtPassword" runat="server" class="form-control mb-3" placeholder="Digite sua senha" required />

        <!-- Checkbox -->
        <div class="checkbox mb-3 text-start">
            <label>
                <input type="checkbox" />
                Recuperar Senha
            </label>
        </div>

        <!-- Botão -->
        <asp:Button
            ID="btnLogar"
            runat="server"
            CssClass="btn btn-lg btn-primary w-100 mb-3 btn-laranja"
            Text="Entrar"
            OnClick="btnLogar_Click" />

        <!-- Cadastro -->
        <asp:HyperLink
            ID="lnkCadastrar"
            runat="server"
            NavigateUrl="~/FrmCadastro.aspx"
            CssClass="btn btn-link w-100">
            Não tem conta? Cadastre-se
        </asp:HyperLink>

        <!-- Mensagem -->
        <p class="mt-3 text-center">
            <label id="lblMensagem" runat="server"></label>
        </p>

        <p class="text-center text-muted">&copy; 2025</p>

    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>

</body>
</html>
