using crazypizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crazypizza
{
    // Classe responsável por TODA a comunicação com a tabela Produtos no banco
    // Usamos Entity Framework (crazypizzaDBEntities)
    public class ProdutoDAO
    {
        // ALTERAR PRODUTO (UPDATE)
        // Recebe um objeto Produtos com os dados alterados e atualiza no banco.
        internal static string AlterarProduto(Produtos produtos)
        {
            string mensagem = "";

            try
            {
                using (var ctx = new crazypizzaDBEntities())
                {
                    // Localiza o produto pelo ID
                    Produtos produtoAlterado = ctx.Produtos.
                        FirstOrDefault(p => p.Id == produtos.Id);
                    // Faz a atualização dos dados
                    produtoAlterado.Descricao = produtos.Descricao;
                    produtoAlterado.Nome = produtos.Nome;
                    produtoAlterado.Imagem = produtos.Imagem;
                    produtoAlterado.Preco = produtos.Preco;
                    produtoAlterado.Estoque = produtos.Estoque;
                    produtoAlterado.IdCategoria = produtos.IdCategoria;

                    // Salva as alterações no banco
                    ctx.SaveChanges();

                    mensagem = "Produto alterado com sucesso!";
                }

            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
            }

            return mensagem;
        }


        // CADASTRAR PRODUTO (CREATE)
        // Adiciona um novo produto no banco — operação INSERT
        internal static string CadastrarProduto(Produtos produtos)
        {
            string mensagem = "";

            try
            {
                var ctx = new crazypizzaDBEntities();
                ctx.Produtos.Add(produtos); // Adiciona à tabela
                ctx.SaveChanges(); // Salva no banco

                mensagem = "O produto " + produtos.Nome +
                    " foi cadastrado com sucesso!";

            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
            }


            return mensagem;
        }

        
        // EXCLUIR PRODUTO (DELETE)
        // Remove um produto do banco com base no seu ID
        internal static string ExcluirProduto(int id)
        {
            string mensagem = "";

            try
            {
                using (var ctx = new crazypizzaDBEntities())
                {
                    Produtos produtos =
                        ctx.Produtos.FirstOrDefault(p => p.Id == id);
                    ctx.Produtos.Remove(produtos);
                    ctx.SaveChanges();
                    mensagem = "Produto excluído com sucesso!";
                }
            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
            }

            return mensagem;
        }


        // LISTAR TODOS OS PRODUTOS 
        // Retorna uma LISTA com todos os produtos do banco
        internal static List<Produtos> ListarProdutos()
        {
            crazypizzaDBEntities ctx = new crazypizzaDBEntities();
            var lista = ctx.Produtos.ToList();
            return lista;
        }


        // VISUALIZAR PRODUTO ESPECÍFICO (READ BY ID)
        // Retorna UM produto específico pelo ID
        internal static Produtos VisualizarProduto(int id)
        {
            Produtos produtos = null;
            string mensagem = "";

            try
            {
                using (var ctx = new crazypizzaDBEntities())
                {
                    produtos = ctx.Produtos.FirstOrDefault(p => p.Id == id);
                }
            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
            }

            return produtos;
        }
    }
}