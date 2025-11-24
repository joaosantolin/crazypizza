<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmPedidos.aspx.cs" Inherits="crazypizza.admin.Orders" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin - Pedidos</title>
    <!-- link do CSS Bootstrap -->
    <link href="~\css\bootstrap.css" rel="stylesheet" />
    <link href="~/css/admin.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <!-- Navbar do painel administrativo -->
        <nav class="navbar navbar-expand-lg navbar-dark admin-navbar">
            <div class="container-fluid">
                <a class="navbar-brand" href="Default.aspx">CrazyPizza</a>
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

                    <asp:LinkButton ID="btnLogout" runat="server" CssClass="btn btn-outline-light" OnClick="btnLogout_Click">Sair</asp:LinkButton>
                </div>
            </div>
        </nav>

        <!-- Container de conteúdo da página -->
        <div class="container mt-3">
            <h2>Pedidos</h2>

            <!-- GridView: exibe os pedidos vindos do banco de dados -->
            <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
                <Columns>
                    
                    <asp:BoundField DataField="PedidoId" HeaderText="ID" />
                    <asp:BoundField DataField="UsuarioId" HeaderText="UsuarioId" />
                    <asp:BoundField DataField="DataCriacao" HeaderText="Data" DataFormatString="{0:G}" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink runat="server" NavigateUrl='<%# "FrmPedidosDetalhes.aspx?id=" + Eval("PedidoId") %>' CssClass="btn btn-outline-primary">Gerenciar</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <!-- Botão para retornar à página inicial do admin -->
            <asp:HyperLink NavigateUrl="~/admin/Default.aspx" runat="server" CssClass="btn btn-secondary">Voltar</asp:HyperLink>
        </div>
    </form>
</body>
</html>
