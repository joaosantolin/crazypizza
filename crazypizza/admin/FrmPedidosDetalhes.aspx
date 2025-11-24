<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmPedidosDetalhes.aspx.cs" Inherits="crazypizza.admin.OrderDetails" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalhes do Pedido</title>
    <!-- Importando Bootstrap e CSS próprio -->
    <link href="~\\css\\bootstrap.css" rel="stylesheet" />
    <link href="~/css/admin.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <!-- NAVBAR ADMIN -->
        <nav class="navbar navbar-expand-lg navbar-dark admin-navbar">
            <div class="container-fluid
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

        <!-- CONTEÚDO DA PÁGINA -->
        <div class="container mt-3">
            <h2>Detalhes do Pedido</h2>

            <!-- Labels que mostram informações trazidas do banco via code behind -->
            <asp:Label ID="lblPedido" runat="server" />
            <br />
            <asp:Label ID="lblUsuario" runat="server" />
            <br />
            <asp:Label ID="lblEndereco" runat="server" />
            <br />

            <!-- Seleção de status do pedido -->
            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select">
                <asp:ListItem>Pendente</asp:ListItem>
                <asp:ListItem>Aceito</asp:ListItem>
                <asp:ListItem>Em Preparo</asp:ListItem>
                <asp:ListItem>Enviado</asp:ListItem>
                <asp:ListItem>Concluido</asp:ListItem>
                <asp:ListItem>Cancelado</asp:ListItem>
            </asp:DropDownList>

            <!-- Botões com eventos criados no .cs -->
            <asp:Button ID="btnAtualizar" runat="server" Text="Atualizar Status" CssClass="btn btn-primary" OnClick="btnAtualizar_Click" />
            <asp:Button ID="btnAceitar" runat="server" Text="Aceitar Pedido" CssClass="btn btn-success" OnClick="btnAceitar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn btn-secondary" OnClick="btnVoltar_Click" />

            <hr />

            <!-- Grid com itens do pedido (produtos + quantidade + preço) -->
            <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="Nome" HeaderText="Produto" />
                    <asp:BoundField DataField="Quantidade" HeaderText="Qtd" />
                    <asp:BoundField DataField="PrecoUnitario" HeaderText="Preço" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
