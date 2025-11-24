<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmPedidos.aspx.cs" Inherits="crazypizza.MyOrders" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" type="img" href="img/logocrazy3.jpg" />
    <title>Meus Pedidos</title>
    <!-- link com CSS e bootstrap -->
    <link href="~\css\bootstrap.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navbar padrão -->
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
       
        <div class="container mt-3">
            <h2>Meus Pedidos</h2>

            <!-- GridView que lista os pedidos do usuário -->
            <asp:GridView ID="gvMeusPedidos" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
                <Columns>
                  
                    <asp:BoundField DataField="PedidoId" HeaderText="ID" />
                    
                    <asp:BoundField DataField="DataCriacao" HeaderText="Data" DataFormatString="{0:G}" />
                    
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                   
                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>
            <asp:HyperLink NavigateUrl="~/Default.aspx" runat="server" CssClass="btn btn-secondary">Voltar</asp:HyperLink>
        </div>
    </form>
</body>
</html>
