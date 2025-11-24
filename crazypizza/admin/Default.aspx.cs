using System;
using System.Web.UI;

namespace crazypizza.admin
{
    // Página inicial do painel administrativo
    public partial class Default : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        // Botão de Logout do administrador
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();      // Remove todas as variáveis da sessão
            Session.Abandon();    // Finaliza a sessão atual completamente
            Response.Redirect("~/Default.aspx"); // Redireciona para a página principal do site
        }
    }
}
