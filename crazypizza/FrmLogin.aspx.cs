using System;                          
using System.Web.UI;                   
using crazypizza.admin;                
using crazypizza.Models;               

namespace crazypizza
{
    public partial class FrmLogin : Page   
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Executa SOMENTE no primeiro carregamento da página 
            if (!IsPostBack)
            {
                // Verifica se foi passado um parâmetro na URL: ?msg=loginNecessario
                var msg = Request.QueryString["msg"];

                // Se vir com este parâmetro mostra mensagem solicitando login
                if (msg == "loginNecessario")
                    lblMensagem.InnerText = "Faça login para continuar.";
            }
        }

        protected void btnLogar_Click(object sender, EventArgs e)
        {
            try
            {
                // Captura o usuário e senha digitados no formulário 
                string login = txtUsuario.Value;
                string senha = txtPassword.Value;

                // Verifica se os campos estão vazios
                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
                {
                    lblMensagem.InnerText = "Você precisa preencher todos os campos!";
                    return; // Interrompe execução
                }

                // Busca o usuário no banco de dados através do DAO
                var usuario = UsuarioDAO.ListarUsuario(login);

                // Se não encontrar o usuário retorna erro
                if (usuario == null)
                {
                    lblMensagem.InnerText = "Usuário não encontrado.";
                    return;
                }

                // Gera o hash da senha digitada pelo usuário
                string senhaHashDigitada = Sha1Hasher.ComputeSha1Hash(senha);

                // Compara o hash digitado com o hash armazenado no banco
                if (!string.Equals(usuario.Senha, senhaHashDigitada, StringComparison.OrdinalIgnoreCase))
                {
                    lblMensagem.InnerText = "Usuário ou senha inválidos.";
                    return;
                }

                
                Session["UsuarioId"] = usuario.IdUsuario;     // Id do usuário
                Session["UsuarioNome"] = usuario.NomeUsuario; // Nome do usuário
                Session["EhAdmin"] = usuario.Admin;           // Se é admin ou não

                // Redireciona conforme o tipo de usuário, se for admin vai para a área administrativa
                Response.Redirect(usuario.Admin ? "~/admin/Default.aspx" : "~/Default.aspx");
            }
            catch (Exception ex)
            {
                // Caso ocorra um erro inesperado → mostra a mensagem ao usuário
                lblMensagem.InnerText = ex.Message;
            }
        }
    }
}
