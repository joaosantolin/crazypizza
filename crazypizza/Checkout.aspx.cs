using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using crazypizza.Models;

namespace crazypizza
{
 public partial class Checkout : Page
 {
 protected void Page_Load(object sender, EventArgs e)
 {
 }

 protected void btnConfirmar_Click(object sender, EventArgs e)
 {
 var cart = Session["Cart"] as List<CartItem>;
 if (cart == null || cart.Count ==0)
 {
 Response.Write("Carrinho vazio");
 return;
 }

 var pedido = new Pedido();
 pedido.UsuarioId =1; // TODO: obter usuario logado
 pedido.DataCriacao = DateTime.Now;
 pedido.Status = "Pendente";

 foreach (var c in cart)
 {
 pedido.ItemPedidos.Add(new ItemPedido { ProdutoId = c.ProdutoId, Quantidade = c.Quantidade, PrecoUnitario = c.Preco, Subtotal = c.Subtotal });
 }

 pedido.Total = cart.Sum(x => x.Subtotal);

 var mensagem = PedidoDAO.CriarPedido(pedido);
 if (mensagem.StartsWith("Pedido criado"))
 {
 Session["Cart"] = null;
 }
 Response.Write(mensagem);
 }
 }
}