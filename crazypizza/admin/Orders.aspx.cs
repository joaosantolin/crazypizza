using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using crazypizza.Models;

namespace crazypizza.admin
{
 public partial class Orders : Page
 {
 // control declaration for designer-less setup
 protected global::System.Web.UI.WebControls.GridView gvPedidos;

 protected void Page_Load(object sender, EventArgs e)
 {
 if (!IsPostBack)
 {
 BindGrid();
 }
 }

 private void BindGrid()
 {
 try
 {
 var lista = PedidoDAO.ListarTodos();
 if (gvPedidos == null)
 {
 Response.Write("Erro: controle gvPedidos está nulo na página. Verifique se o GridView existe no .aspx e possui runat=\"server\" e ID='gvPedidos'.");
 return;
 }

 gvPedidos.DataSource = lista;
 gvPedidos.DataBind();
 }
 catch (Exception ex)
 {
 // diagnóstico temporário
 Response.Write("Erro ao vincular Grid de pedidos: " + ex.Message + "<br/>" + ex.StackTrace);
 }
 }
 }
}