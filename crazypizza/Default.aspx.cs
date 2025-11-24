using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using crazypizza.admin;
using crazypizza.Models;

namespace crazypizza
{
    public partial class _Default : System.Web.UI.Page
    {
        // Controles da página (vinculados ao .aspx)
        protected global::System.Web.UI.WebControls.ListView lvProdutos;
        protected global::System.Web.UI.WebControls.Repeater rptCarousel;
        protected global::System.Web.UI.WebControls.Label lblInfo;
        protected global::System.Web.UI.WebControls.LinkButton btnLoginLogout;
        protected global::System.Web.UI.WebControls.TextBox txtBusca;
        protected global::System.Web.UI.WebControls.DropDownList ddlCategorias;

        // Método chamado automaticamente sempre que a página carrega
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                CarregarCarousel(); // Carrega imagens do carrossel
                CarregarCategorias(); // Preenche dropdown de categorias
                CarregarProdutos(); // Carrega lista de produtos
                ConfigureLoginLogoutButton(); // Ajusta texto do botão (Login/Sair)
            }
        }

        // Carrega apenas os carrosséis marcados como destaque no banco
        private void CarregarCarousel()
        {
            var carousels = CarouselDAO.ListarCarousels().Where(c => c.Destaque == true).ToList();
            rptCarousel.DataSource = carousels;
            rptCarousel.DataBind();
        }

        // Carrega categorias e adiciona ao dropdown
        private void CarregarCategorias()
        {
            var cats = CategoriaDAO.ListarCategorias();
            ddlCategorias.Items.Clear();
            ddlCategorias.Items.Add(new ListItem("-- Categoria --", ""));
            // Adiciona todas as categorias vindas do banco
            foreach (var c in cats)
                ddlCategorias.Items.Add(new ListItem(c.Categoria1, c.Id.ToString()));
        }

        // Carrega produtos no ListView(permitindo filtros por parâmetro opcional)
        private void CarregarProdutos(IEnumerable<Produtos> fonte = null)
        {
            var lista = fonte ?? ProdutoDAO.ListarProdutos();
            lvProdutos.DataSource = lista;
            lvProdutos.DataBind();
        }


        // Ajusta o texto do botão dependendo do login
        private void ConfigureLoginLogoutButton()
        {
            btnLoginLogout.Text = Session["UsuarioId"] == null ? "Login" : "Sair";
        }


        // LOGIN / LOGOUT
        protected void btnLoginLogout_Click(object sender, EventArgs e)
        {
            // Se usuário não está logado → vai para página de login
            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/FrmLogin.aspx");
            }

            else
            {
                // Se já está logado → encerra sessão e volta para home
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Default.aspx");
            }
        }


        // ADICIONAR AO CARRINHO
        protected void lvProdutos_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                // Se usuário não estiver logado, não permite adicionar ao carrinho
                if (Session["UsuarioId"] == null)
                {
                    lblInfo.Text = "Faça login para adicionar itens ao carrinho.";
                    ConfigureLoginLogoutButton();
                    return;
                }
                // Pega o ID do produto e adiciona ao carrinho
                int id = Convert.ToInt32(e.CommandArgument);
                AddToCart(id);
                lblInfo.CssClass = "text-success";
                lblInfo.Text = "Item adicionado ao carrinho.";
            }
        }

        // FILTROS DE PRODUTOS
        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            string cmd = (sender as Button)?.CommandName;
            var todos = ProdutoDAO.ListarProdutos();
            IEnumerable<Produtos> filtrado = todos;
            switch (cmd)
            {
                case "PrecoAsc":
                    filtrado = todos.OrderBy(p => p.Preco ??0m); break;
                case "PrecoDesc":
                    filtrado = todos.OrderByDescending(p => p.Preco ??0m); break;
                case "Buscar":
                    var termo = (txtBusca.Text ?? "").Trim().ToLowerInvariant();
                    filtrado = todos.Where(p => (p.Nome ?? "").ToLowerInvariant().Contains(termo));
                    break;
                case "Todos":
                default:
                    filtrado = todos; break;
            }
            // aplicar categoria se selecionada
            if (!string.IsNullOrEmpty(ddlCategorias.SelectedValue))
            {
                int catId = int.Parse(ddlCategorias.SelectedValue);
                filtrado = filtrado.Where(p => p.IdCategoria == catId);
            }
            CarregarProdutos(filtrado);
        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            // reaplicar filtros existentes + categoria
            btnFiltro_Click(btnTodos, EventArgs.Empty);
        }

        protected void AddToCart(int produtoId)
        {
            var produto = ProdutoDAO.VisualizarProduto(produtoId);
            if (produto == null) return;
            // Se ainda não existe carrinho → cria
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                Session["Cart"] = cart;
            }
            // Se produto já está no carrinho → incrementa quantidade
            var existing = cart.Find(c => c.ProdutoId == produto.Id);
            if (existing != null)
            {
                existing.Quantidade++;
            }
            else
            {
                cart.Add(new CartItem { ProdutoId = produto.Id, Nome = produto.Nome, Preco = produto.Preco ??0m, Quantidade =1 });
            }
        }

        // Retorna o valor total do carrinho
        public static decimal GetCartTotal(List<CartItem> cart)
        {
            if (cart == null) return 0m;
            decimal total = 0m;
            foreach (var c in cart) total += c.Subtotal;
            return total;
        }
    }
}