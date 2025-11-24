<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="crazypizza.admin.OrderDetails" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Detalhes do Pedido</title>
 <link href="~\css\bootstrap.css" rel="stylesheet" />
</head>
<body>
 <form id="form1" runat="server">
 <nav class="navbar navbar-expand-lg bg-body-tertiary">
 <div class="container-fluid">
 <a class="navbar-brand" href="Default.aspx">crazyPizza's</a>
 <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
 <span class="navbar-toggler-icon"></span>
 </button>
 <div class="collapse navbar-collapse" id="navbarNav">
 <ul class="navbar-nav">
 <li class="nav-item"><a class="nav-link" href="Default.aspx">Início</a></li>
 <li class="nav-item"><a class="nav-link" href="FrmProdutos.aspx">Produtos</a></li>
 <li class="nav-item"><a class="nav-link" href="Orders.aspx">Pedidos</a></li>
 </ul>
 </div>
 </div>
 </nav>
 <div class="container mt-3">
 <h2>Detalhes do Pedido</h2>
 <asp:Label ID="lblPedido" runat="server" /> <br />
 <asp:Label ID="lblUsuario" runat="server" /> <br />
 <asp:Label ID="lblEndereco" runat="server" /> <br />
 <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select">
 <asp:ListItem>Pendente</asp:ListItem>
 <asp:ListItem>Aceito</asp:ListItem>
 <asp:ListItem>EmPreparo</asp:ListItem>
 <asp:ListItem>Enviado</asp:ListItem>
 <asp:ListItem>Concluido</asp:ListItem>
 <asp:ListItem>Cancelado</asp:ListItem>
 </asp:DropDownList>
 <asp:Button ID="btnAtualizar" runat="server" Text="Atualizar Status" CssClass="btn btn-primary" OnClick="btnAtualizar_Click" />
 <asp:Button ID="btnAceitar" runat="server" Text="Aceitar Pedido" CssClass="btn btn-success" OnClick="btnAceitar_Click" />
 <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn btn-secondary" OnClick="btnVoltar_Click" />
 <hr />
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