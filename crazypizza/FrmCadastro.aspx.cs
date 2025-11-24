using System;
using System.Web.UI;
using crazypizza.Models;
using crazypizza.admin; // reutiliza UsuarioDAO

namespace crazypizza
{
 public partial class Register : Page
 {
 protected global::System.Web.UI.WebControls.TextBox txtNome;
 protected global::System.Web.UI.WebControls.TextBox txtLogin;
 protected global::System.Web.UI.WebControls.TextBox txtEmail;
 protected global::System.Web.UI.WebControls.TextBox txtSenha;
 protected global::System.Web.UI.WebControls.TextBox txtSenha2;
 protected global::System.Web.UI.WebControls.Label lblMsg;
 protected global::System.Web.UI.WebControls.Button btnRegistrar;

 protected void btnRegistrar_Click(object sender, EventArgs e)
 {
 string nome = txtNome.Text.Trim();
 string login = txtLogin.Text.Trim();
 string email = txtEmail.Text.Trim();
 string senha = txtSenha.Text;
 string senha2 = txtSenha2.Text;
 if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha) || string.IsNullOrWhiteSpace(senha2))
 {
 lblMsg.Text = "Preencha todos os campos.";
 return;
 }
 if (!senha.Equals(senha2))
 {
 lblMsg.Text = "Senhas não conferem.";
 return;
 }
 // validar unicidade do login
 var existente = UsuarioDAO.ListarUsuario(login);
 if (existente != null)
 {
 lblMsg.Text = "Login já em uso.";
 return;
 }
 var usuario = new Usuario
 {
 NomeUsuario = nome,
 Login = login,
 Email = email,
 DataCadastro = DateTime.Now,
 Senha = Sha1Hasher.ComputeSha1Hash(senha),
 Admin = false
 };
 var msg = UsuarioDAO.CadastrarUsuario(usuario);
 if (msg.StartsWith("Usuário cadastrado"))
 {
 // auto login opcional
 Session["UsuarioId"] = usuario.IdUsuario;
 Session["UsuarioNome"] = usuario.NomeUsuario;
 Session["EhAdmin"] = usuario.Admin;
 Response.Redirect("~/Default.aspx");
 }
 else
 {
 lblMsg.Text = msg;
 }
 }
 }
}