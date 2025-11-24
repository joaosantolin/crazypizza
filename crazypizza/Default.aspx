<%@ Page Language="C#" MaintainScrollPositionOnPostBack="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="crazypizza._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- links com css, bootstrap -->
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" type="img" href="img/logocrazy3.jpg" />
    <title>CrazyPizza</title>
    <link href="~\css\bootstrap.css" rel="stylesheet" />
    <link href="~\css\style.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">

        <!-- Navbar usuário unificada -->
        <nav class="navbar navbar-expand-lg navbar-custom">
            <div class="container-fluid">

                <a class="navbar-brand d-flex align-items-center logo-font" href="Default.aspx">
                    <img src="img/logocrazy3.jpg" class="logo-navbar me-2" alt="CrazyPizza Logo" />
                    CrazyPizza
                </a>

                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navUser" aria-controls="navUser" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navUser">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                        <li class="nav-item"><a class="nav-link active" href="Default.aspx">Home</a></li>
                        <li class="nav-item"><a class="nav-link" href="FrmCarrinho.aspx">Carrinho</a></li>
                        <li class="nav-item"><a class="nav-link" href="FrmPedidos.aspx">Meus Pedidos</a></li>
                    </ul>
                    <asp:LinkButton ID="btnLoginLogout" runat="server" CssClass="btn btn-outline-dark" OnClick="btnLoginLogout_Click"></asp:LinkButton>
                </div>
            </div>
        </nav>
        <!-- Fim Navbar -->

        <!-- Label para mensagens de erro ou notificações -->
        <div class="container mt-2">
            <asp:Label ID="lblInfo" runat="server" CssClass="text-danger"></asp:Label>
        </div>

        <!-- Carrousel -->
        <!-- REPEATER: carrega imagens do BD automaticamente -->
        <div id="carouselExampleControls" class="carousel slide" data-bs-ride="carousel">
            <div class="carousel-inner">
                <asp:Repeater ID="rptCarousel" runat="server">
                    <ItemTemplate>
                        <div class='carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>'>
                                <img src='<%# Eval("Imagem") %>' class="d-block w-100 carousel-img" alt='<%# Eval("Titulo") %>' />
                                <div class="carousel-caption d-none d-md-block">
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Previous</span>
            </button>
            <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Next</span>
            </button>
        </div>
        <!-- Fim Carrousel -->

        <!-- Filtros -->
        <!-- Caixa de filtros para buscar e ordenar os produtos -->
        <div class="container mt-4 mb-2 filter-box">
            <div class="card p-3">
                <div class="row g-2 align-items-center">
                    <div class="col-auto"><strong>Filtrar:</strong></div>
                    <div class="col-auto">
                        <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="form-select form-select-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-auto">
                        <asp:Button ID="btnTodos" runat="server" Text="Todos" CssClass="btn btn-sm btn-secondary" OnClick="btnFiltro_Click" CommandName="Todos" />
                    </div>
                    <div class="col-auto">
                        <asp:Button ID="btnPrecoAsc" runat="server" Text="Menor Preço" CssClass="btn btn-sm btn-outline-primary" OnClick="btnFiltro_Click" CommandName="PrecoAsc" />
                    </div>
                    <div class="col-auto">
                        <asp:Button ID="btnPrecoDesc" runat="server" Text="Maior Preço" CssClass="btn btn-sm btn-outline-primary" OnClick="btnFiltro_Click" CommandName="PrecoDesc" />
                    </div>
                    <div class="col-auto">
                        <asp:TextBox ID="txtBusca" runat="server" CssClass="form-control form-control-sm" placeholder="Buscar nome" />
                    </div>
                    <div class="col-auto">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-sm btn-outline-success" OnClick="btnFiltro_Click" CommandName="Buscar" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Produtos -->
        <div class="container mt-2">
            <asp:ListView runat="server" ID="lvProdutos" OnItemCommand="lvProdutos_ItemCommand">
                <LayoutTemplate>
                    <div class="row row-cols-1 row-cols-md-3 g-4">
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100">
                            <img src='<%# Eval("Imagem") %>' class="card-img-top card-img-prato" alt='<%# Eval("Nome") %>' />

                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Nome") %></h5>
                                <p class="card-text"><%# Eval("Descricao") %></p>
                                <p class="card-text">R$<%# Eval("Preco") %></p>

                                <!-- O botão vai ser empurrado para baixo automaticamente -->
                                <asp:LinkButton runat="server" CssClass="btn btn-primary mt-3"
                                    CommandName="AddToCart" CommandArgument='<%# Eval("Id") %>'>Adicionar ao Carrinho
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>

        <!-- Fim Produtos -->

        <!-- FOOTER -->
        <footer class="footer-laranja">
            <div class="container text-center py-4">
                <div class="row justify-content-center">
                    <div class="col-md-4">
                        <h4>HORÁRIOS</h4>
                        <p>Terça a sexta: 18h às 24h</p>
                        <p>Sábados e domingos: 17h às 24h</p>
                    </div>

                    <div class="col-md-4">
                        <h4>LOCALIZAÇÃO</h4>
                        <p>Rua Rio Grande do Norte, Guaçuí/ES</p>
                    </div>
                </div>
            </div>

            <div class="footer-bottom text-center">
                <p>&copy; 2025 CrazyPizza | Desenvolvido por <strong>Eduardo Rodrigues e João Santolin</strong></p>
            </div>
        </footer>


    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="js/script.js"></script>
</body>
</html>
