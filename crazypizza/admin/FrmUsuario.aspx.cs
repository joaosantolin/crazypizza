using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using crazypizza.Models;

namespace crazypizza.admin
{
    // Página de gerenciamento de usuários do sistema
    public partial class FrmUsuario : AdminPage
    {
        // Declaração de controles que não estão no .designer automaticamente:
        protected GridView gvUsuarios;             // Tabela de usuários
        protected HtmlInputHidden txtIdUsuario;    // Guarda o ID do usuário em edição
        protected HtmlInputCheckBox chkAdmin;      // Checkbox para definir se é administrador

        protected void Page_Load(object sender, EventArgs e)
        {
            // Evita recarregar a grid toda vez que houver postback
            if (!IsPostBack)
            {
                BindUsuarios(); // Carrega os usuários na tabela
            }
        }

        // Cadastrar ou Alterar usuário
        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            var idStr = txtIdUsuario.Value;
            bool isEdicao = !string.IsNullOrWhiteSpace(idStr);  // Verifica se é alteração ou cadastro

            var senha = txtSenha.Value;
            var repSenha = txtRepetirSenha.Value;

            // Se for cadastro, validar se as senhas conferem:
            if (!isEdicao)
            {
                if (senha != repSenha)
                {
                    lblMensagem.InnerText = "As senhas não conferem!";
                    return;
                }
            }

            // Criar objeto usuário para salvar/alterar:
            var usuario = new Usuario
            {
                NomeUsuario = txtNomeUsuario.Value,
                Login = txtLogin.Value,
                Email = txtEmail.Value,
                Admin = chkAdmin.Checked
            };

            if (isEdicao)
            {
                // Caso edição, apenas altera dados básicos
                usuario.IdUsuario = int.Parse(idStr);

                // Se a senha NÃO foi digitada, mantém a senha anterior
                if (!string.IsNullOrEmpty(senha))
                    usuario.Senha = Sha1Hasher.ComputeSha1Hash(senha);

                lblMensagem.InnerText = UsuarioDAO.AlterarUsuario(usuario);
            }
            else
            {
                // CADASTRO NOVO
                usuario.DataCadastro = DateTime.Now;
                usuario.Senha = Sha1Hasher.ComputeSha1Hash(senha);
                lblMensagem.InnerText = UsuarioDAO.CadastrarUsuario(usuario);
            }

            // Depois de salvar → limpa os campos
            LimparDados();

            // Atualiza a GridView com os novos dados
            BindUsuarios();
        }

        // Captura o comando da GridView (Editar / Remover)
        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")   // Se clicou no botão Editar
            {
                var u = UsuarioDAO.VisualizarUsuario(id);
                if (u == null) { lblMensagem.InnerText = "Usuário não encontrado"; return; }

                // Preenche os campos para edição:
                txtIdUsuario.Value = u.IdUsuario.ToString();
                txtNomeUsuario.Value = u.NomeUsuario;
                txtLogin.Value = u.Login;
                txtEmail.Value = u.Email;
                chkAdmin.Checked = u.Admin;

                lblMensagem.InnerText = "Carregado para edição";
            }
            else if (e.CommandName == "Remover") // Exclusão de usuário
            {
                lblMensagem.InnerText = UsuarioDAO.ExcluirUsuario(id);
                BindUsuarios(); // Atualiza a grid após excluir
            }
        }

        // Carrega a lista de usuários no GridView
        private void BindUsuarios()
        {
            var lista = UsuarioDAO.ListarTodos();
            gvUsuarios.DataSource = lista;
            gvUsuarios.DataBind();
        }

        // Limpa os campos após salvar/excluir
        private void LimparDados()
        {
            txtIdUsuario.Value = "";
            txtNomeUsuario.Value = "";
            txtLogin.Value = "";
            txtEmail.Value = "";
            txtSenha.Value = "";
            txtRepetirSenha.Value = "";
            chkAdmin.Checked = false;
        }

        // Logout do admin
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
    }
}
