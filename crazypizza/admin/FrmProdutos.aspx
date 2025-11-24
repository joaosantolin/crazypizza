<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmProdutos.aspx.cs" Inherits="crazypizza.admin.FrmProdutos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gerenciar Produtos - Admin</title>
    <!-- link do Bootstrap e CSS do admin -->
    <link href="~\css\bootstrap.css" rel="stylesheet" />
    <link href="~/css/admin.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <!-- NAVBAR ADMIN -->
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
                    <asp:LinkButton ID="btnLogout" runat="server" CssClass="btn btn-outline-light" OnClick="btnLogout_Click">
                        Sair
                    </asp:LinkButton>
                </div>
            </div>
        </nav>

        <!-- CONTEÚDO PRINCIPAL -->
        <div class="container mt-4">
            <h1 class="mb-4">Painel de Produtos</h1>

            <div class="row">
                <!-- COLUNA ESQUERDA: CADASTRAR / EDITAR PRODUTO -->
                <div class="col-md-5">
                    <div class="card mb-3">
                        <div class="card-body">
                            <h5 class="card-title">Cadastro / Edição</h5>

                            <!-- Campo oculto para ID  -->
                            <input type="hidden" id="txtId" runat="server" />

                            <!-- Informações dos produtos -->
                            <div class="mb-2">
                                <label class="form-label">Nome do Produto <span class="text-danger">*</span></label>
                                <input type="text" id="txtNome" runat="server" class="form-control" />
                            </div>

                            <div class="mb-2">
                                <label class="form-label">Descrição <span class="text-danger">*</span></label>
                                <textarea id="txtDescricao" runat="server" rows="3" class="form-control"></textarea>
                            </div>

                            <div class="mb-2">
                                <label class="form-label">Caminho da Imagem <span class="text-danger">*</span></label>
                                <input type="text" id="txtImagem" runat="server" class="form-control" />
                            </div>

                            <div class="mb-2">
                                <label class="form-label">Preço</label>
                                <input type="text" id="txtPreco" runat="server" class="form-control" />
                            </div>

                            <div class="mb-2">
                                <label class="form-label">Estoque</label>
                                <input type="text" id="txtEstoque" runat="server" class="form-control" />
                            </div>

                            <!-- CATEGORIA (DropDown preenchido no code-behind) -->
                            <div class="mb-3">
                                <label class="form-label">Categoria</label>
                                <asp:DropDownList ID="ddlCategoria" runat="server" DataTextField="Categoria1" DataValueField="Id" CssClass="form-select"></asp:DropDownList>
                            </div>

                            <div class="mb-3">
                                <asp:Button Text="Cadastrar" ID="BtnCadastrar" runat="server" CssClass="btn btn-primary" OnClick="BtnCadastrar_Click" />
                                <input type="reset" value="Limpar" class="btn btn-secondary" id="btnLimpar" runat="server" />
                                <a href="/admin/FrmProdutos.aspx" id="btnAddProduto" runat="server" visible="false" class="btn btn-success">+Adicionar Produto</a>
                                <a href="/admin/FrmProdutos.aspx" id="btnVoltarProduto" runat="server" visible="false" class="btn btn-warning">Voltar Página</a>
                            </div>

                            <!-- MENSAGEM DE ERRO / SUCESSO -->
                            <div>
                                <label id="lblMensagem" runat="server" class="text-info"></label>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- COLUNA DIREITA: LISTA DE PRODUTOS -->
                <div class="col-md-7">
                    <h5>Lista de Produtos</h5>

                    <!-- TABELA LISTANDO OS PRODUTOS -->
                    <table class="table table-hover table-sm">
                        <thead class="table-light">
                            <tr>
                                <th>#</th>
                                <th>Produto</th>
                                <th>Preço</th>
                                <th>Cadastro</th>
                                <th>Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            <!-- ListView traz os dados do banco de dados -->
                            <asp:ListView runat="server" ID="lvProdutos" OnItemCommand="LvProdutos_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <!-- Valores vindos do banco -->
                                        <th scope="row"><%# Eval("Id") %></th>
                                        <td><%# Eval("Nome") %></td>
                                        <td><%# Eval("Preco") %></td>
                                        <td><%# Eval("DataCadastro") %></td>

                                        <!-- Botões: Visualizar, Editar e Deletar -->
                                        <td>
                                            <asp:ImageButton ImageUrl="../img/view.svg" runat="server" CommandName="Visualizar" CommandArgument='<%# Eval("Id") %>' AlternateText="Visualizar" CssClass="me-1" />
                                            <asp:ImageButton ImageUrl="../img/edit.svg" runat="server" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' AlternateText="Editar" CssClass="me-1" />
                                            <asp:ImageButton ImageUrl="../img/delete.svg" runat="server" CommandName="Deletar" CommandArgument='<%# Eval("Id") %>' AlternateText="Excluir" OnClientClick="return confirm('Deseja realmente excluir?');" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
