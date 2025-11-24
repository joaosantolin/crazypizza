using System;
using System.Web.UI;

namespace crazypizza.admin
{
    /// <summary>
    /// Base page para todas as páginas administrativas.
    /// Verifica sessão e privilégio de administrador usando Session["UsuarioId"] e Session["EhAdmin"].
    /// </summary>
    public abstract class AdminPage : Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Usuário não logado
            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/FrmLogin.aspx?msg=loginNecessario");
                return;
            }

            // Verifica flag de administrador
            bool isAdmin = Session["EhAdmin"] is bool b && b;
            if (!isAdmin)
            {
                // Redireciona para área pública se não for admin
                Response.Redirect("~/Default.aspx");
                return;
            }
        }
    }
}
