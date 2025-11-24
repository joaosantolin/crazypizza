<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="crazypizza.admin.Orders" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Admin - Pedidos</title>
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
 <li class="nav-item"><a class="nav-link active" href="Orders.aspx">Pedidos</a></li>
 </ul>
 </div>
 </div>
 </nav>
 <div class="container mt-3">
 <h2>Pedidos</h2>
 <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="false" CssClass="table table-striped">
 <Columns>
 <asp:BoundField DataField="PedidoId" HeaderText="ID" />
 <asp:BoundField DataField="UsuarioId" HeaderText="UsuarioId" />
 <asp:BoundField DataField="DataCriacao" HeaderText="Data" DataFormatString="{0:G}" />
 <asp:BoundField DataField="Status" HeaderText="Status" />
 <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
 <asp:TemplateField>
 <ItemTemplate>
 <asp:HyperLink runat="server" NavigateUrl='<%# "OrderDetails.aspx?id=" + Eval("PedidoId") %>' CssClass="btn btn-outline-primary">Gerenciar</asp:HyperLink>
 </ItemTemplate>
 </asp:TemplateField>
 </Columns>
 </asp:GridView>
 <asp:HyperLink NavigateUrl="~/admin/Default.aspx" runat="server" CssClass="btn btn-secondary">Voltar</asp:HyperLink>
 </div>
 </form>
</body>
</html>