using crazypizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace crazypizza.admin
{
    public partial class FrmCarousel : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AtualizarDdlProdutos(ProdutoDAO.ListarProdutos());
                AtualizarLvCarousels(CarouselDAO.ListarCarousels());
            }
        }

        private void AtualizarLvCarousels(List<Carousel> carousels)
        {
            lvCarousels.DataSource = carousels;
            lvCarousels.DataBind();
        }

        private void AtualizarDdlProdutos(List<Produtos> produtos)
        {
            DdlProdutos.DataSource = produtos.OrderBy(p => p.Nome);
            DdlProdutos.DataTextField = "Nome";
            DdlProdutos.DataValueField = "Id";
            DdlProdutos.DataBind();
            // Insere opção padrão com valor vazio para facilitar validação
            DdlProdutos.Items.Insert(0, new ListItem("Selecione um produto", ""));
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            bool alterar = btnCadastrar.Text.ToUpper() == "ALTERAR";

            Carousel carousel = null;

            if (!alterar)
            {
                carousel = new Carousel(); //Cadastrando Carousel
            }
            else
            {
                //Alterando Carousel
                int id = Convert.ToInt32(txtId.Value);
                carousel = CarouselDAO.ListarCarousels(id);
            }

            carousel.Titulo = txtTitulo.Value;
            carousel.Descricao = txtDescricao.Value;

            // Validação do produto selecionado
            int produtoId;
            if (!int.TryParse(DdlProdutos.SelectedValue, out produtoId) || produtoId <=0)
            {
                lblMensagem.InnerText = "Selecione um produto válido.";
                return; // Interrompe o fluxo antes de tentar salvar
            }
            carousel.ProdutoID = produtoId;
            carousel.Imagem = txtImagem.Value;

            var destaque = cbDestaque.Checked;
            carousel.Destaque = destaque;

            if (!alterar)
            {
                lblMensagem.InnerText = CarouselDAO.CadastrarCarousel(carousel);
            }
            else
            {
                lblMensagem.InnerText = CarouselDAO.AlterarCarousel(carousel);
            }

            AtualizarLvCarousels(CarouselDAO.ListarCarousels());

            txtTitulo.Value = "";
            txtDescricao.Value = "";
            txtImagem.Value = "";
            DdlProdutos.SelectedIndex = 0;
            cbDestaque.Checked = false;

            btnAddCarousel.Visible = false;
            btnCadastrar.Text = "Cadastrar";
            btnCadastrar.CssClass = "";
            btnLimpar.CssClass = "btn-secondary";

        }

        protected void lvCarousels_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string comando = e.CommandName;

            if (e.CommandArgument != null)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Carousel carousel = null;

                switch (comando)
                {
                    case "Deletar":
                        string mensagem = CarouselDAO.ExcluirCarousel(id);
                        AtualizarLvCarousels(CarouselDAO.ListarCarousels());
                        lblMensagem.InnerText = mensagem;
                        break;

                    case "Visualizar":
                        carousel = CarouselDAO.ListarCarousels(id);
                        MostrarDadosCarousel(carousel, true);
                        break;

                    case "Editar":
                        carousel = CarouselDAO.ListarCarousels(id);
                        MostrarDadosCarousel(carousel, false);
                        HabilitarCampos();
                        btnCadastrar.Text = "Alterar";
                        btnCadastrar.CssClass = "btn btn-warning";
                        btnLimpar.CssClass = "btn btn-secondary";
                        btnAddCarousel.Visible = true;

                        break;

                    default:
                        break;
                }

            }

        }

        /// <summary>
        /// Método utilizado para exibir dados no furmulário
        /// </summary>
        /// <param name="carousel">Objeto preenchido</param>
        /// <param name="visualizar">Booleano informando ação atual</param>
        private void MostrarDadosCarousel(Carousel carousel, bool visualizar)
        {
            txtId.Value = carousel.Id.ToString();
            txtTitulo.Value = carousel.Titulo;
            txtDescricao.Value = carousel.Descricao;
            txtImagem.Value = carousel.Imagem;
            // Garante que o item exista no dropdown antes de selecionar
            var item = DdlProdutos.Items.FindByValue(carousel.ProdutoID.ToString());
            if (item != null)
            {
                DdlProdutos.SelectedValue = carousel.ProdutoID.ToString();
            }

            if (carousel.Destaque.HasValue)
            {
                cbDestaque.Checked = carousel.Destaque.Value;
            }

            if (visualizar) { DesabilitarCampos(); }
        }

        private void HabilitarCampos()
        {
            txtDescricao.Disabled = false;
            txtTitulo.Disabled = false;
            txtImagem.Disabled = false;
            cbDestaque.Disabled = false;
            DdlProdutos.Enabled = true;
            btnCadastrar.Visible = false;
            btnLimpar.Visible = false;

            btnAddCarousel.Visible = true;
        }
        private void DesabilitarCampos()
        {
            txtDescricao.Disabled = true;
            txtTitulo.Disabled = true;
            txtImagem.Disabled = true;
            cbDestaque.Disabled = true;
            DdlProdutos.Enabled = false;
            btnCadastrar.Visible = false;
            btnLimpar.Visible = false;

            btnAddCarousel.Visible = true;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
    }
}