using crazypizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace crazypizza.admin
{
 public partial class FrmProdutos : AdminPage
 {
  protected void Page_Load(object sender, EventArgs e)
  {
   // Carrega apenas na primeira vez da página
   if (!Page.IsPostBack)
   {
    // Carrega as categorias no dropdown
    CarregarCategorias();

    // Atualiza a ListView com todos os produtos
    AtualizarLvProdutos(ProdutoDAO.ListarProdutos());

    // Se vier um ?cod=ID na URL, carrega os dados para visualização
    string cod = Request.QueryString["cod"];
    if (cod != null)
    {
     int id = int.Parse(cod);
     Produtos produto = ProdutoDAO.VisualizarProduto(id);
     MostrarDadosProduto(produto, true); // TRUE = apenas visualizar
    }
   }
  }

  // Botão de Logout
  protected void btnLogout_Click(object sender, EventArgs e)
  {
   Session.Clear();
   Session.Abandon();
   Response.Redirect("~/Default.aspx");
  }

  // Preenche o DropDownList com categorias vindas do banco
  private void CarregarCategorias()
  {
   ddlCategoria.DataSource = CategoriaDAO.ListarCategorias();
   ddlCategoria.DataBind();

   // Adiciona um item padrão: "Selecione uma categoria"
   ddlCategoria.Items.Insert(0, new ListItem("Selecione uma categoria", ""));
  }

  // Exibe os dados do produto nas caixas de texto e escolhe modo (visualizar / alterar)
  private void MostrarDadosProduto(Produtos produtos, bool visualizar)
  {
   txtDescricao.Value = produtos.Descricao;
   txtNome.Value = produtos.Nome;
   txtImagem.Value = produtos.Imagem;
   txtPreco.Value = produtos.Preco.ToString();
   txtEstoque.Value = produtos.Estoque.ToString();
   ddlCategoria.SelectedValue = produtos.IdCategoria.ToString();

   btnAddProduto.Visible = true;

   if (visualizar)
   {
    Visualizar();  // Somente leitura
   }
   else
   {
    Alterar();  // Modo de edição
   }
  }

  // Modo de edição no botão Cadastrar
  private void Alterar()
  {
   BtnCadastrar.Text = "Alterar"; // muda nome do botão
   VisualizarCadastrar();         // reativa os campos para edição
  }

  // Bloqueia os campos para apenas visualizar (READ ONLY)
  private void Visualizar()
  {
   txtDescricao.Disabled = true;
   txtNome.Disabled = true;
   txtImagem.Disabled = true;
   txtPreco.Disabled = true;
   txtEstoque.Disabled = true;
   ddlCategoria.Enabled = false;

   btnVoltarProduto.Visible = true;
   BtnCadastrar.Visible = false;
   btnLimpar.Visible = false;
   btnAddProduto.Visible = false;
  }

  // Atualiza a ListView com os produtos
  private void AtualizarLvProdutos(List<Produtos> produtos)
  {
   var lista = produtos.OrderBy(p => p.Nome); // ordena por nome
   lvProdutos.DataSource = lista;
   lvProdutos.DataBind();
  }

  // Ativa os campos para cadastro
  private void VisualizarCadastrar()
  {
   txtDescricao.Disabled = false;
   txtNome.Disabled = false;
   txtImagem.Disabled = false;
   txtPreco.Disabled = false;
   txtEstoque.Disabled = false;
   btnVoltarProduto.Visible = false;
   BtnCadastrar.Visible = true;
   btnLimpar.Visible = true;
   btnAddProduto.Visible = false;
   ddlCategoria.Enabled = true;
  }

  // Cadastrar ou Alterar Produto
  protected void BtnCadastrar_Click(object sender, EventArgs e)
  {
   // Verifica se selecionou categoria
   if (string.IsNullOrEmpty(ddlCategoria.SelectedValue))
   {
    lblMensagem.InnerText = "Selecione uma opção de categoria!";
    return;
   }

   bool alterando = BtnCadastrar.Text == "Alterar"; // define modo

   Produtos produtos = null;

   if (!alterando)
   {
    // Novo produto
    produtos = new Produtos();
    produtos.DataCadastro = DateTime.Now;
   }
   else
   {
    // Produto já existente → MODO ALTERAR
    int id = Int32.Parse(txtId.Value);
    produtos = ProdutoDAO.VisualizarProduto(id);
   }

   // Atribui os valores vindos dos inputs
   produtos.Nome = txtNome.Value;
   produtos.Descricao = txtDescricao.Value;
   produtos.Imagem = txtImagem.Value;
   produtos.Preco = Convert.ToDecimal(txtPreco.Value);
   produtos.Estoque = Convert.ToInt32(txtEstoque.Value);
   produtos.IdCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);

   string mensagem = "";

   if (!alterando)
   {
    // CADASTRO NOVO
    mensagem = ProdutoDAO.CadastrarProduto(produtos);
   }
   else
   {
    // ALTERAÇÃO
    mensagem = ProdutoDAO.AlterarProduto(produtos);
    Response.Redirect("~/admin/FrmProdutos.aspx");  // atualiza visualização
   }

   // Limpa tela e exibe mensagem
   LimparCampos(mensagem);

   // Atualiza ListView
   AtualizarLvProdutos(ProdutoDAO.ListarProdutos());
  }

  // Limpa os campos após cadastrar ou alterar
  private void LimparCampos(string mensagem)
  {
   lblMensagem.InnerText = mensagem;
   txtDescricao.Value = "";
   txtImagem.Value = "";
   txtNome.Value = "";
   txtPreco.Value = "";
  }

  // Eventos da ListView: Editar / Visualizar / Deletar
  protected void LvProdutos_ItemCommand(object sender, ListViewCommandEventArgs e)
  {
   string comando = e.CommandName;
   int id = Convert.ToInt32(e.CommandArgument);

   if (comando == "Deletar")
   {
    string mensagem = ProdutoDAO.ExcluirProduto(id);
    AtualizarLvProdutos(ProdutoDAO.ListarProdutos());
    lblMensagem.InnerText = mensagem;
   }
   else if (comando == "Visualizar")
   {
    Response.Redirect("~/admin/FrmProdutos.aspx?cod=" + id);
   }
   else if (comando == "Editar")
   {
    Produtos produtos = ProdutoDAO.VisualizarProduto(id);
    MostrarDadosProduto(produtos, false); // FALSE = modo edição
    txtId.Value = id.ToString();
   }
  }
 }
}
