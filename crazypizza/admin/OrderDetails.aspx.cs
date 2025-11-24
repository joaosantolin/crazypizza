using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using crazypizza.Models;

namespace crazypizza.admin
{
 public partial class OrderDetails : Page
 {
 protected global::System.Web.UI.WebControls.Label lblPedido;
 protected global::System.Web.UI.WebControls.Label lblUsuario;
 protected global::System.Web.UI.WebControls.Label lblEndereco;
 protected global::System.Web.UI.WebControls.DropDownList ddlStatus;
 protected global::System.Web.UI.WebControls.GridView gvItems;

 protected void Page_Load(object sender, EventArgs e)
 {
 if (!IsPostBack)
 {
 LoadPedido();
 }
 }

 private void LoadPedido()
 {
 int id =0;
 int.TryParse(Request.QueryString["id"], out id);
 if (id<=0) return;
 var pedido = PedidoDAO.ObterPorId(id);
 if (pedido==null) return;
 lblPedido.Text = $"Pedido {pedido.PedidoId} - Total: {pedido.Total:C} - Status: {pedido.Status}";
 ddlStatus.SelectedValue = pedido.Status;
 // usuario
 var usuario = (new crazypizzaDBEntities()).Usuario.FirstOrDefault(u => u.IdUsuario== pedido.UsuarioId);
 lblUsuario.Text = usuario!=null? $"Cliente: {usuario.NomeUsuario} ({usuario.Email})":"Cliente desconhecido";
 lblEndereco.Text = string.IsNullOrEmpty(pedido.EnderecoEntrega)? "Endereço não informado" : $"Endereço: {pedido.EnderecoEntrega}";

 // carregar itens e buscar nome do produto
 var itens = pedido.ItemPedidos ?? new List<ItemPedido>();
 using (var ctx = new crazypizzaDBEntities())
 {
 var data = from it in itens
 join p in ctx.Produtos on it.ProdutoId equals p.Id into pj
 from p in pj.DefaultIfEmpty()
 select new { Nome = p==null? "Produto removido" : p.Nome, it.Quantidade, it.PrecoUnitario, it.Subtotal };
 gvItems.DataSource = data.ToList();
 gvItems.DataBind();
 }
 }

 protected void btnAtualizar_Click(object sender, EventArgs e)
 {
 int id =0;
 int.TryParse(Request.QueryString["id"], out id);
 if (id>0)
 {
 var novo = ddlStatus.SelectedValue;
 var msg = PedidoDAO.AtualizarStatus(id, novo);
 Response.Write(msg);
 LoadPedido();
 }
 }

 protected void btnAceitar_Click(object sender, EventArgs e)
 {
 int id =0;
 int.TryParse(Request.QueryString["id"], out id);
 if (id>0)
 {
 var msg = PedidoDAO.AtualizarStatus(id, "Aceito");
 Response.Write(msg);
 LoadPedido();
 }
 }

 protected void btnVoltar_Click(object sender, EventArgs e)
 {
 Response.Redirect("Orders.aspx");
 }
 }
}