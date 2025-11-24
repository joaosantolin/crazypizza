using System;                                     
using System.Web.UI;                              
using System.Web.UI.WebControls;                  
using crazypizza.Models;                          

namespace crazypizza
{
    public partial class MyOrders : Page          
    {
        // Controles presentes na página ASPX
        protected global::System.Web.UI.WebControls.GridView gvMeusPedidos; // Tabela para mostrar os pedidos
        protected global::System.Web.UI.WebControls.LinkButton btnLoginLogout; // Botão de Login/Sair

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica se o usuário está logado.
            // Se não estiver, redireciona para o login com uma mensagem na URL.
            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/FrmLogin.aspx?msg=loginNecessario");
                return; 
            }

            // Se NÃO for PostBack a página está abrindo pela primeira vez
            if (!IsPostBack)
            {
                ConfigureLoginLogoutButton(); // Define texto do botão (Login ou Sair)

                int usuarioId = (int)Session["UsuarioId"]; // Recupera ID do usuário logado da sessão

                // Chama o DAO para buscar os pedidos desse usuário
                var lista = PedidoDAO.ListarPorUsuario(usuarioId);

                // Define a lista como fonte de dados do GridView
                gvMeusPedidos.DataSource = lista;
                gvMeusPedidos.DataBind();
            }
        }

        // Método responsável por configurar o texto do botão Login/Sair
        private void ConfigureLoginLogoutButton()
        {
            if (Session["UsuarioId"] == null)
                btnLoginLogout.Text = "Login";   
            else
                btnLoginLogout.Text = "Sair";    
        }

        protected void btnLoginLogout_Click(object sender, EventArgs e)
        {
            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/FrmLogin.aspx");
                return;
            }

            // Se estiver logado limpa a sessão (logout)
            Session.Clear();
            Session.Abandon();

            // Volta para a página inicial
            Response.Redirect("~/Default.aspx");
        }
    }
}
