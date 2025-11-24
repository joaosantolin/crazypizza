<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmUsuario.aspx.cs" Inherits="crazypizza.admin.FrmUsuario" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Usuários</title>
    <link href="~/css/bootstrap.css" rel="stylesheet" />
    <link href="~/css/admin.css" rel="stylesheet" />
</head>
<body>
    <form id="formUser" runat="server" class="frm-cadastro">

        <!-- NAVBAR ADMINISTRATIVA -->
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
                    <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-outline-light" Text="Sair" OnClick="btnLogout_Click" />

                </div>
            </div>
        </nav>

        <!-- CONTEÚDO PRINCIPAL -->
        <div class="container mt-4">
            <h1 class="text-light">Gerenciamento de Usuários</h1>
            <div class="row">

                <!-- FORMULÁRIO -->
                <div class="col-md-5">
                    <div class="card bg-dark text-light shadow mb-4">
                        <div class="card-body">
                            <h5 class="card-title">Novo / Editar Usuário</h5>

                            <input type="hidden" id="txtIdUsuario" runat="server" />

                            <div class="mb-2">
                                <label>Nome Usuário:</label>
                                <input type="text" id="txtNomeUsuario" runat="server" class="form-control" />
                            </div>

                            <div class="mb-2">
                                <label>Login:</label>
                                <input type="text" id="txtLogin" runat="server" class="form-control" />
                            </div>

                            <div class="mb-2">
                                <label>E-mail:</label>
                                <input type="email" id="txtEmail" runat="server" class="form-control" />
                            </div>

                            <div class="mb-2">
                                <label>Senha:</label>
                                <input type="password" id="txtSenha" runat="server" class="form-control" />
                            </div>

                            <div class="mb-2">
                                <label>Repetir Senha:</label>
                                <input type="password" id="txtRepetirSenha" runat="server" class="form-control" />
                            </div>

                            <div class="form-check mb-2">
                                <input class="form-check-input" type="checkbox" id="chkAdmin" runat="server" />
                                <label class="form-check-label" for="chkAdmin">Administrador</label>
                            </div>

                            <div class="mb-2">
                                <asp:Button Text="Cadastrar / Salvar" ID="btnCadastrar" runat="server"
                                    CssClass="btn btn-primary" OnClick="btnCadastrar_Click" />

                                <input type="reset" value="Limpar" class="btn btn-secondary" id="btnLimpar" runat="server" />
                            </div>

                            <label id="lblMensagem" runat="server" class="text-info"></label>
                        </div>
                    </div>
                </div>

                <!-- GRID -->
                <div class="col-md-7">
                    <h5 class="text-light">Usuários Cadastrados</h5>
                    <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="false"
                                  CssClass="table table-dark table-striped table-hover shadow"
                                  OnRowCommand="gvUsuarios_RowCommand" DataKeyNames="IdUsuario">

                        <Columns>
                            <asp:BoundField DataField="IdUsuario" HeaderText="ID" />
                            <asp:BoundField DataField="NomeUsuario" HeaderText="Nome" />
                            <asp:BoundField DataField="Login" HeaderText="Login" />
                            <asp:BoundField DataField="Email" HeaderText="E-mail" />
                            <asp:CheckBoxField DataField="Admin" HeaderText="Admin" />

                            <asp:TemplateField HeaderText="Ações">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CommandName="Editar"
                                        CommandArgument='<%# Eval("IdUsuario") %>'
                                        CssClass="btn btn-sm btn-outline-primary me-1">Editar</asp:LinkButton>

                                    <asp:LinkButton runat="server" CommandName="Remover"
                                        CommandArgument='<%# Eval("IdUsuario") %>'
                                        CssClass="btn btn-sm btn-outline-danger"
                                        OnClientClick="return confirm('Deseja excluir o usuário?');">Remover</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
