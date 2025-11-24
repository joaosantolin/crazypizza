<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmCarousel.aspx.cs" Inherits="crazypizza.admin.FrmCarousel" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin - Carousel</title>
    <link href="~/css/bootstrap.css" rel="stylesheet" />
    <link href="~/css/admin.css" rel="stylesheet" />
</head>
<body>
    <form id="form2" runat="server">
        <!-- NAVBAR ADMINISTRATIVA -->
        <nav class="navbar navbar-expand-lg navbar-dark admin-navbar">
            <div class="container-fluid">
                <a class="navbar-brand" href="Default.aspx">CrazyPizza</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav me-auto">
                        <li class="nav-item"><a class="nav-link" href="Default.aspx">Início</a></li>
                        <li class="nav-item"><a class="nav-link" href="FrmProdutos.aspx">Produtos</a></li>
                        <li class="nav-item"><a class="nav-link" href="FrmPedidos.aspx">Pedidos</a></li>
                        <li class="nav-item"><a class="nav-link" href="FrmUsuario.aspx">Usuários</a></li>
                        <li class="nav-item"><a class="nav-link active" href="FrmCarousel.aspx">Carousel</a></li>
                    </ul>
                    <asp:LinkButton ID="btnLogout" runat="server" CssClass="btn btn-outline-light" OnClick="btnLogout_Click">Sair</asp:LinkButton>
                </div>
            </div>
        </nav>

        <!-- CONTEÚDO PRINCIPAL -->
        <div class="container mt-4">
            <h1 class="text-light">Gerenciamento de Carousel</h1>
            <div class="row">
                <!-- FORMULÁRIO -->
                <div class="col-md-5">
                    <div class="card bg-dark text-light shadow mb-4">
                        <div class="card-body">
                            <h5 class="card-title">Novo / Editar Carousel</h5>
                            <input type="hidden" id="txtId" runat="server" />

                            <div class="mb-2">
                                <label for="txtTitulo">Título do Carousel:</label>
                                <input type="text" id="txtTitulo" runat="server" class="form-control" />
                            </div>

                            <div class="mb-2">
                                <label for="txtDescricao">Descrição:</label>
                                <input type="text" id="txtDescricao" runat="server" class="form-control" />
                            </div>
                            
                            <div class="mb-2">
                                <label for="txtImagem">Imagem (URL):</label>
                                <input type="text" id="txtImagem" runat="server" class="form-control" />
                            </div>

                            <div class="mb-2">
                                <label for="DdlProdutos">Produto:</label>
                                <asp:DropDownList runat="server" ID="DdlProdutos" CssClass="form-select"></asp:DropDownList>
                            </div>

                            <div class="form-check mb-3">
                                <input type="checkbox" id="cbDestaque" runat="server" class="form-check-input" />
                                <label class="form-check-label" for="cbDestaque">Destaque</label>
                            </div>

                            <div class="mb-2">
                                <asp:Button Text="Cadastrar" ID="btnCadastrar" runat="server" CssClass="btn btn-primary" OnClick="btnCadastrar_Click" />
                                <asp:Button Text="Limpar" runat="server" ID="btnLimpar" type="reset" CssClass="btn btn-secondary" />
                                <a href="/admin/FrmCarousel.aspx" id="btnAddCarousel" runat="server" visible="false" class="btn btn-success">+ Adicionar Novo</a>
                            </div>

                            <label id="lblMensagem" runat="server" class="text-info"></label>
                        </div>
                    </div>
                </div>

                <!-- TABELA -->
                <div class="col-md-7">
                     <h5 class="text-light">Carousels Cadastrados</h5>
                    <asp:ListView runat="server" ID="lvCarousels" OnItemCommand="lvCarousels_ItemCommand">
                        <LayoutTemplate>
                            <table class="table table-dark table-striped table-hover shadow">
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Título</th>
                                        <th>Imagem</th>
                                        <th>Destaque</th>
                                        <th>Ações</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Id") %></td>
                                <td><%# Eval("Titulo") %></td>
                                <td><img src='<%# Eval("Imagem") %>' alt='<%# Eval("Titulo") %>' style="max-width: 100px; max-height: 50px;" /></td>
                                <td><%# Convert.ToBoolean(Eval("Destaque")) ? "Sim" : "Não" %></td>
                                <td>
                                    <asp:LinkButton runat="server" CommandName="Visualizar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-outline-info me-1">Ver</asp:LinkButton>
                                    <asp:LinkButton runat="server" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-outline-warning me-1">Editar</asp:LinkButton>
                                    <asp:LinkButton runat="server" CommandName="Deletar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Deseja realmente excluir?');">Excluir</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                         <EmptyDataTemplate>
                            <div class="alert alert-secondary text-center">Nenhum carousel cadastrado.</div>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
