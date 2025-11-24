using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using crazypizza.Models;

namespace crazypizza
{
 public partial class MyOrders : Page
 {
 protected global::System.Web.UI.WebControls.GridView gvMeusPedidos;

 protected void Page_Load(object sender, EventArgs e)
 {
 if (!IsPostBack)
 {
 // obter usuario logado - por enquanto uso1 como exemplo
 int usuarioId =1;
 var lista = PedidoDAO.ListarPorUsuario(usuarioId);
 gvMeusPedidos.DataSource = lista;
 gvMeusPedidos.DataBind();
 }
 }
 }
}