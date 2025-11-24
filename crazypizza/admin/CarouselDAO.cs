using crazypizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace crazypizza.admin
{
    public class CarouselDAO
    {
        // MÉTODO PARA CADASTRAR UM NOVO CAROUSEL
        public static string CadastrarCarousel(Carousel carousel)
        {
            string mensagem = "";

            try
            {
                crazypizzaDBEntities ctx = new crazypizzaDBEntities();
                ctx.Carousel.Add(carousel);    // Adiciona o objeto à tabela
                ctx.SaveChanges();             // Salva no banco

                mensagem = "Carousel cadastrado com sucesso!";
            }
            catch (Exception ex)
            {
                mensagem = ex.Message; // Se ocorrer erro, retorna mensagem da exceção
            }

            return mensagem;
        }

        // MÉTODO PARA ALTERAR UM CAROUSEL EXISTENTE
        public static string AlterarCarousel(Carousel carousel)
        {
            string mensagem = "";

            try
            {
                using (var ctx = new crazypizzaDBEntities())
                {
                    // Busca o carousel no banco de dados pelo ID
                    var carouselBD = ctx.Carousel.FirstOrDefault(c => c.Id == carousel.Id);

                    // Guarda o título antigo (para mensagem de retorno)
                    string tituloAntigo = carouselBD.Titulo;

                    // Atualiza os dados
                    carouselBD.Titulo = carousel.Titulo;
                    carouselBD.Descricao = carousel.Descricao;
                    carouselBD.Destaque = carousel.Destaque;
                    carouselBD.ProdutoID = carousel.ProdutoID;
                    carouselBD.Imagem = carousel.Imagem;

                    ctx.SaveChanges(); // Salva alterações

                    mensagem = $"Carousel '{tituloAntigo}' alterado com sucesso!";
                }
            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
            }

            return mensagem;
        }

        // MÉTODO PARA EXCLUIR UM CAROUSEL PELO ID
        internal static string ExcluirCarousel(int id)
        {
            string mensagem = "";
            try
            {
                using (var ctx = new crazypizzaDBEntities())
                {
                    Carousel carousel = ctx.Carousel.FirstOrDefault(c => c.Id == id);

                    ctx.Carousel.Remove(carousel); 
                    ctx.SaveChanges();

                    mensagem = "Carousel excluído com sucesso!";
                }
            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
            }

            return mensagem;
        }

        // LISTAR TODOS OS CAROUSELS
        internal static List<Carousel> ListarCarousels()
        {
            var ctx = new crazypizzaDBEntities();
            return ctx.Carousel.ToList(); // Retorna todos os registros
        }

        // LISTAR APENAS UM CAROUSEL PELO ID
        internal static Carousel ListarCarousels(int id)
        {
            using (var ctx = new crazypizzaDBEntities())
            {
                return ctx.Carousel.FirstOrDefault(c => c.Id == id);
            }
        }
    }
}
