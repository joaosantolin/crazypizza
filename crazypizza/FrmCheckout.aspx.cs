using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using crazypizza.Models;

namespace crazypizza
{
    public partial class Checkout : Page
    {
        // Controles da página .aspx
        protected global::System.Web.UI.WebControls.TextBox txtEndereco; // Campo para o endereço de entrega
        protected global::System.Web.UI.WebControls.Label lblMsg;        // Exibe mensagens ao usuário
        protected global::System.Web.UI.WebControls.LinkButton btnLoginLogout; // Botão Login/Sair

        protected void Page_Load(object sender, EventArgs e)
        {
            // Se o usuário não estiver logado redireciona para o login
            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/FrmLogin.aspx?msg=loginNecessario");
                return;
            }

            // Executa apenas no 1º carregamento da página (evita repetir binds em postback)
            if (!IsPostBack)
            {
                ConfigureLoginLogoutButton();
            }
        }

        // Define o texto do botão conforme login/sessão
        private void ConfigureLoginLogoutButton()
        {
            if (Session["UsuarioId"] == null)
                btnLoginLogout.Text = "Login";
            else
                btnLoginLogout.Text = "Sair";
        }

        protected void btnLoginLogout_Click(object sender, EventArgs e)
        {
            // Se estiver deslogado vai para login
            if (Session["UsuarioId"] == null) { Response.Redirect("~/FrmLogin.aspx"); return; }

            // Se estiver logado encerra sessão e volta para home
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }

        // Evento ao clicar em Confirmar Pedido
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            // Recupera o carrinho da sessão
            var cart = Session["Cart"] as List<CartItem>;

            // Se o carrinho estiver vazio bloqueia ação
            if (cart == null || cart.Count == 0)
            {
                lblMsg.Text = "Carrinho vazio";
                return;
            }

            // Se endereço não foi informado bloqueia ação
            if (string.IsNullOrWhiteSpace(txtEndereco.Text))
            {
                lblMsg.Text = "Informe o endereço de entrega.";
                return;
            }

            // Recupera o ID do usuário logado
            int usuarioId = (int)Session["UsuarioId"];

            // Cria o objeto Pedido e preenche as informações
            var pedido = new Pedido();
            pedido.UsuarioId = usuarioId;
            pedido.DataCriacao = System.DateTime.Now; 
            pedido.Status = "Pendente";               
            pedido.EnderecoEntrega = txtEndereco.Text.Trim();

            // Adiciona os itens do carrinho ao pedido
            foreach (var c in cart)
            {
                pedido.ItemPedidos.Add(new ItemPedido
                {
                    ProdutoId = c.ProdutoId,
                    Quantidade = c.Quantidade,
                    PrecoUnitario = c.Preco,
                    Subtotal = c.Subtotal
                });
            }

            // Calcula o valor total
            pedido.Total = cart.Sum(x => x.Subtotal);

            // Envia para o DAO e recebe mensagem de confirmação / erro
            var mensagem = PedidoDAO.CriarPedido(pedido);

            // Se criado com sucesso, limpa o carrinho
            if (mensagem.StartsWith("Pedido criado"))
            {
                Session["Cart"] = null; 
                lblMsg.CssClass = "text-success"; 
            }

            lblMsg.Text = mensagem; // exibe mensagem final
        }
    }
}
