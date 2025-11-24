<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmCheckout.aspx.cs" Inherits="crazypizza.Checkout" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="img" href="img/logocrazy3.jpg" />
    <title>Checkout</title>
    <!--link com CSS e bootstrap-->
    <link href="css\bootstrap.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!--navbar padrão-->
        <nav class="navbar navbar-expand-lg navbar-custom">
            <div class="container-fluid">
                <a class="navbar-brand" href="Default.aspx">CrazyPizza's</a>
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
        <div class="container mt-5 mb-5 col-md-6">
            <div class="card p-4 shadow-sm">
                <h3 class="mb-4 text-center">Finalizar Pedido</h3>

                <!-- Campo para informar o endereço de entrega -->
                <div class="mb-3">
                    <label for="txtEndereco" class="form-label fw-bold">Endereço de Entrega</label>
                    <asp:TextBox ID="txtEndereco" runat="server" CssClass="form-control"
                                 placeholder="Rua, número, bairro, cidade, UF"></asp:TextBox>
                </div>

                <!-- Botões de ação -->
                <div class="d-flex justify-content-between mt-4">
                    <asp:HyperLink NavigateUrl="~/FrmCarrinho.aspx" runat="server" CssClass="btn btn-secondary">Voltar</asp:HyperLink>
                    <asp:Button runat="server" ID="btnConfirmar" Text="Confirmar Pedido"
                                CssClass="btn btn-success" OnClick="btnConfirmar_Click" />
                </div>

                <!-- Área para mensagens de erro ou sucesso -->
                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mt-3 d-block text-center"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
