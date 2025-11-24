<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="crazypizza.admin.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" type="img" href="../img/logocrazy3.jpg" />
    <title>Painel Administrativo - CrazyPizza</title>
    <!-- link com bootstrap e CSS do painel administrativo-->
    <link href="~\css\bootstrap.css" rel="stylesheet" />
    <link href="~/css/admin.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navbar do painel administrativo -->
        <nav class="navbar navbar-expand-lg navbar-dark admin-navbar">
            <div class="container-fluid">
                <a class="navbar-brand d-flex align-items-center" href="Default.aspx">
                    <span>CrazyPizza</span>
                </a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav me-auto">
                        <li class="nav-item"><a class="nav-link active" href="Default.aspx">Início</a></li>
                        <li class="nav-item"><a class="nav-link" href="FrmProdutos.aspx">Produtos</a></li>
                        <li class="nav-item"><a class="nav-link" href="FrmPedidos.aspx">Pedidos</a></li>
                        <li class="nav-item"><a class="nav-link" href="FrmUsuario.aspx">Usuários</a></li>
                        <li class="nav-item"><a class="nav-link" href="FrmCarousel.aspx">Carousel</a></li>
                    </ul>
                    <asp:LinkButton ID="btnLogout" runat="server" CssClass="btn btn-outline-light" OnClick="btnLogout_Click">
                Sair
                    </asp:LinkButton>
                </div>
            </div>
        </nav>

        <div class="container mt-3">
            <h1>Painel administrativo</h1>
        </div>
    </form>
</body>
</html>
