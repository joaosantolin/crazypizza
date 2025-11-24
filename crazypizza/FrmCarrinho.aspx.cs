using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using crazypizza.Models;
using System.Web.UI.WebControls; // added for TextBox, GridViewRow, HiddenField

namespace crazypizza
{
    public partial class Cart : Page
    {
        // Controles presentes na página .aspx
        protected GridView gvCart;
        protected LinkButton btnLoginLogout;
        protected Label lblMsg;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Se não estiver logado ? redireciona para login
            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/FrmLogin.aspx?msg=loginNecessario");
                return;
            }

            // Executa somente na 1ª vez que a página é carregada (não em postbacks)
            if (!IsPostBack)
            {
                ConfigureLoginLogoutButton(); // Configura texto do botão Login/Sair
                BindCart();                   // Carrega os itens do carrinho
            }
        }

        // Define o texto do botão conforme usuário logado ou não
        private void ConfigureLoginLogoutButton()
        {
            btnLoginLogout.Text = Session["UsuarioId"] == null ? "Login" : "Sair";
        }

        protected void btnLoginLogout_Click(object sender, EventArgs e)
        {
            // Se não estiver logado ? vai para página de login
            if (Session["UsuarioId"] == null) { Response.Redirect("~/FrmLogin.aspx"); return; }

            // Se estiver logado ? encerra sessão e volta para home
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }

        // Recupera o carrinho armazenado na sessão
        private List<CartItem> GetCart()
        {
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            Session["Cart"] = cart; // Garante que carrinho exista na sessão
            return cart;
        }

        // Atualiza o GridView com os itens do carrinho
        private void BindCart()
        {
            gvCart.DataSource = GetCart();
            gvCart.DataBind();
        }

        // Evento disparado ao clicar em botões de cada linha do carrinho (Ex: "+1")
        protected void gvCart_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            // Botão para adicionar 1 unidade ao item
            if (e.CommandName == "AddOne")
            {
                int produtoId = int.Parse(e.CommandArgument.ToString());
                var cart = GetCart();
                var item = cart.FirstOrDefault(c => c.ProdutoId == produtoId);

                // Item não existe mais no carrinho
                if (item == null) { lblMsg.Text = "Item não encontrado no carrinho."; return; }

                // Verifica estoque antes de adicionar +1
                using (var ctx = new crazypizzaDBEntities())
                {
                    var produto = ctx.Produtos.FirstOrDefault(p => p.Id == produtoId);
                    if (produto == null) { lblMsg.Text = "Produto não existe mais."; return; }

                    int estoque = produto.Estoque;
                    if (item.Quantidade + 1 > estoque)
                    {
                        lblMsg.Text = "Quantidade solicitada excede o estoque disponível.";
                        return;
                    }

                    item.Quantidade++; // Adiciona 1 unidade
                }

                lblMsg.CssClass = "text-success";
                lblMsg.Text = "+1 adicionado";
                BindCart(); // Atualiza página
            }
        }

        // Evento disparado quando a quantidade é alterada manualmente no TextBox
        protected void txtQtd_TextChanged(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            if (txt == null) return;

            // Descobre a linha da alteração
            var row = (GridViewRow)txt.NamingContainer;

            // Pega o ID do produto através do HiddenField
            var hf = (HiddenField)row.FindControl("hfProdId");
            int produtoId = int.Parse(hf.Value);

            // Valida nova quantidade digitada
            int novaQtd;
            if (!int.TryParse(txt.Text, out novaQtd) || novaQtd <= 0)
            {
                lblMsg.Text = "Quantidade inválida.";
                BindCart();
                return;
            }

            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProdutoId == produtoId);
            if (item == null) { lblMsg.Text = "Item não encontrado."; return; }

            // Verifica estoque antes de atualizar quantidade
            using (var ctx = new crazypizzaDBEntities())
            {
                var produto = ctx.Produtos.FirstOrDefault(p => p.Id == produtoId);
                if (produto == null) { lblMsg.Text = "Produto não existe mais."; return; }

                int estoque = produto.Estoque;
                if (novaQtd > estoque)
                {
                    lblMsg.Text = $"Estoque insuficiente. Máximo: {estoque}";
                    txt.Text = item.Quantidade.ToString(); // Mantém valor anterior no campo
                    return;
                }

                item.Quantidade = novaQtd; // Atualiza quantidade no carrinho
            }

            lblMsg.CssClass = "text-success";
            lblMsg.Text = "Quantidade atualizada";
            BindCart(); // Atualiza carrinho na tela
        }

        // Finalizar compra ? vai para página de checkout
        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FrmCheckout.aspx");
        }
    }
}
