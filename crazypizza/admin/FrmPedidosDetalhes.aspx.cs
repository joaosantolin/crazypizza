using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using crazypizza.Models;

namespace crazypizza.admin
{
    public partial class OrderDetails : Page
    {
        // Controles declarados manualmente 
        protected global::System.Web.UI.WebControls.Label lblPedido;
        protected global::System.Web.UI.WebControls.Label lblUsuario;
        protected global::System.Web.UI.WebControls.Label lblEndereco;
        protected global::System.Web.UI.WebControls.DropDownList ddlStatus;
        protected global::System.Web.UI.WebControls.GridView gvItems;
        protected global::System.Web.UI.WebControls.LinkButton btnLogout;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Se não for postback (primeiro carregamento da página), carregamos o pedido
            if (!IsPostBack)
            {
                LoadPedido();
            }
        }

        // Carregar as informações do pedido
        private void LoadPedido()
        {
            int id = 0;
            // Tenta obter o ID do querystring ?id=5
            int.TryParse(Request.QueryString["id"], out id);

            if (id <= 0) return; // Se não tiver ID válido, não prossegue

            var pedido = PedidoDAO.ObterPorId(id); // Busca o pedido no banco

            if (pedido == null) return; // Se não achou, encerra

            // Mostra dados gerais do pedido
            lblPedido.Text = $"Pedido {pedido.PedidoId} - Total: {pedido.Total:C} - Status: {pedido.Status}";

            // Seleciona o status atual no DropDownList
            ddlStatus.SelectedValue = pedido.Status;

            // Buscar dados do usuário (cliente do pedido)
            var usuario = (new crazypizzaDBEntities()).Usuario.FirstOrDefault(u => u.IdUsuario == pedido.UsuarioId);
            lblUsuario.Text = usuario != null
                ? $"Cliente: {usuario.NomeUsuario} ({usuario.Email})"
                : "Cliente desconhecido";

            // Endereço de entrega
            lblEndereco.Text = string.IsNullOrEmpty(pedido.EnderecoEntrega)
                ? "Endereço não informado"
                : $"Endereço: {pedido.EnderecoEntrega}";

            // Carregar itens do pedido e exibir com nome do produto
            var itens = pedido.ItemPedidos ?? new List<ItemPedido>();

            using (var ctx = new crazypizzaDBEntities())
            {
                // JOIN entre itens e produtos para exibir nome, quantidade e preços
                var data = from it in itens
                           join p in ctx.Produtos on it.ProdutoId equals p.Id into pj
                           from p in pj.DefaultIfEmpty()
                           select new
                           {
                               Nome = p == null ? "Produto removido" : p.Nome,
                               it.Quantidade,
                               it.PrecoUnitario,
                               it.Subtotal
                           };

                // Vincular ao GridView
                gvItems.DataSource = data.ToList();
                gvItems.DataBind();
            }
        }

        // Botão de atualizar status manualmente
        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            int id = 0;
            int.TryParse(Request.QueryString["id"], out id);

            if (id > 0)
            {
                var novo = ddlStatus.SelectedValue; // Obtém status selecionado
                var msg = PedidoDAO.AtualizarStatus(id, novo); // Atualiza no banco
                Response.Write(msg); // Feedback
                LoadPedido(); // Recarrega os dados
            }
        }

        // Botão para aceitar o pedido direto (mais prático)
        protected void btnAceitar_Click(object sender, EventArgs e)
        {
            int id = 0;
            int.TryParse(Request.QueryString["id"], out id);

            if (id > 0)
            {
                var msg = PedidoDAO.AtualizarStatus(id, "Aceito");
                Response.Write(msg);
                LoadPedido();
            }
        }

        // Voltar para a tela de pedidos
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmPedidos.aspx");
        }

        // Logout (encerra sessão e volta para login)
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
    }
}
