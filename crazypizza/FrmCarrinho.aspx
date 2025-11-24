<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmCarrinho.aspx.cs" Inherits="crazypizza.Cart" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="img" href="img/logocrazy3.jpg" />

    <title>Carrinho</title>
    <link href="~\css\bootstrap.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <!-- Navbar padrão usada em todas as telas do usuário-->
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

        <div class="container mt-3 carrinho-area">
            <h2 class="titulo-carrinho">Meu Carrinho</h2>
            
            <!-- GRIDVIEW - LISTA DE PRODUTOS NO CARRINHO -->
            <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="false"
                CssClass="table table-bordered table-carrinho" DataKeyNames="ProdutoId">
                <Columns>
                    
                    <asp:BoundField DataField="Nome" HeaderText="Produto" />
                    
                    <asp:BoundField DataField="Preco" HeaderText="Preço" DataFormatString="{0:C}" />
                    
                    <asp:TemplateField HeaderText="Qtd">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfProdId" runat="server" Value='<%# Eval("ProdutoId") %>' />
                            <asp:TextBox ID="txtQtd" runat="server" Text='<%# Eval("Quantidade") %>'
                                TextMode="Number" CssClass="form-control form-control-sm quantidade-input"
                                AutoPostBack="true" OnTextChanged="txtQtd_TextChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>

            <!-- Botões para checkout e para voltar às compras -->
            <div class="carrinho-footer">
                <asp:Button runat="server" ID="btnCheckout" Text="Finalizar Compra" CssClass="btn-finalizar" OnClick="btnCheckout_Click" />
                <asp:HyperLink NavigateUrl="~/Default.aspx" runat="server" CssClass="btn-voltar">Continuar Comprando</asp:HyperLink>
            </div>

            <asp:Label ID="lblMsg" runat="server" CssClass="text-danger d-block mt-2"></asp:Label>
        </div>


        

    </form>
</body>
</html>
