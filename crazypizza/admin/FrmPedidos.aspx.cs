using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using crazypizza.Models;

namespace crazypizza.admin
{
    public partial class Orders : AdminPage
    {
        protected global::System.Web.UI.WebControls.GridView gvPedidos;   // Grid que lista os pedidos
        protected global::System.Web.UI.WebControls.LinkButton btnLogout; // Botão de logout

        protected void Page_Load(object sender, EventArgs e)
        {
            // Só recarrega o grid de pedidos se não for um postback
            if (!IsPostBack)
            {
                BindGrid(); // Carrega os pedidos na tabela
            }
        }

        // Botão de Logout - encerra a sessão e volta para o site principal
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();    // Limpa variáveis da sessão
            Session.Abandon();  // Finaliza a sessão por completo
            Response.Redirect("~/Default.aspx"); // Redireciona para a página inicial
        }

        // Método que carrega os pedidos do banco e joga no GridView
        private void BindGrid()
        {
            try
            {
                // Busca todos os pedidos armazenados no banco (via DAO)
                var lista = PedidoDAO.ListarTodos();

                // Tratamento caso o GridView não esteja encontrado na página
                if (gvPedidos == null)
                {
                    Response.Write("Erro: controle gvPedidos está nulo na página. Verifique se o GridView existe no .aspx e possui runat=\"server\" e ID='gvPedidos'.");
                    return;
                }

                // Atribui os dados e exibe no grid
                gvPedidos.DataSource = lista;
                gvPedidos.DataBind();
            }
            catch (Exception ex)
            {
                // Tratamento de erro temporário para facilitar debug
                Response.Write("Erro ao vincular Grid de pedidos: " + ex.Message + "<br/>" + ex.StackTrace);
            }
        }
    }
}
