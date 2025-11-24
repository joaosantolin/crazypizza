<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="crazypizza.Cart" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Carrinho</title>
    <link href="~\css\bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <div class="container-fluid">
                <a class="navbar-brand" href="Default.aspx">crazyPizza's</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navUser" aria-controls="navUser" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navUser">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                        <li class="nav-item"><a class="nav-link" href="Default.aspx">Home</a></li>
                        <li class="nav-item"><a class="nav-link active" href="Cart.aspx">Carrinho</a></li>
                        <li class="nav-item"><a class="nav-link" href="MyOrders.aspx">Meus Pedidos</a></li>
                    </ul>
                    <asp:HyperLink runat="server" NavigateUrl="~/Login.aspx" CssClass="btn btn-outline-dark">Login</asp:HyperLink>
                </div>
            </div>
        </nav>
        <div class="container mt-3">
            <h2>Carrinho</h2>
            <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="Nome" HeaderText="Produto" />
                    <asp:BoundField DataField="Preco" HeaderText="Preço" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="Quantidade" HeaderText="Qtd" />
                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>
            <asp:Button runat="server" ID="btnCheckout" Text="Finalizar Compra" CssClass="btn btn-primary" OnClick="btnCheckout_Click" />
            <asp:HyperLink NavigateUrl="~/Default.aspx" runat="server" CssClass="btn btn-secondary">Voltar</asp:HyperLink>
        </div>
    </form>
</body>
</html>
