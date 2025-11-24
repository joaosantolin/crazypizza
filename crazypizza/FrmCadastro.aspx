<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmCadastro.aspx.cs" Inherits="crazypizza.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Cadastro - CrazyPizza</title>
    <link rel="icon" type="img" href="img/logocrazy3.jpg" />
    <!-- CSS e bootstrap -->
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" /> 
</head>

<body class="bg-cadastro">

<form id="form1" runat="server">
    <div class="container d-flex justify-content-center align-items-center tela-cadastro">
        <div class="card card-cadastro shadow p-4">

            <!-- Logo -->
            <div class="text-center mb-3">
                <img src="img/logocrazy3.jpg" class="logo-cadastro" alt="Logo" />
            </div>

            <h4 class="text-center mb-4">Criar Conta</h4>

            <!-- Campos -->
            <div class="mb-3">
                <label for="txtNome" class="form-label">Nome</label>
                <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="txtLogin" class="form-label">Login</label>
                <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="txtEmail" class="form-label">E-mail</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
            </div>

            <div class="mb-3">
                <label for="txtSenha" class="form-label">Senha</label>
                <asp:TextBox ID="txtSenha" runat="server" CssClass="form-control" TextMode="Password" />
            </div>

            <div class="mb-3">
                <label for="txtSenha2" class="form-label">Confirmar Senha</label>
                <asp:TextBox ID="txtSenha2" runat="server" CssClass="form-control" TextMode="Password" />
            </div>

            <!-- Botão -->
            <asp:Button ID="btnRegistrar" runat="server" Text="Cadastrar" CssClass="btn btn-warning w-100" OnClick="btnRegistrar_Click" />

            <!-- Voltar -->
            <div class="text-center mt-3">
                <asp:HyperLink ID="lnkVoltar" runat="server" NavigateUrl="~/FrmLogin.aspx" CssClass="link-cadastro">
                    Já tem conta? Entrar
                </asp:HyperLink>
            </div>

            <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mt-2"></asp:Label>
        </div>
    </div>
</form>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
