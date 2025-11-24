using crazypizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crazypizza.admin
{
    // Classe responsável pelas operações de acesso ao banco de dados para as categorias
    public class CategoriaDAO
    {
        // Método para listar todas as categorias do banco de dados
        public static List<Categoria> ListarCategorias()
        {
            // "using" garante que o contexto será automaticamente destruído após o uso
            using (var ctx = new crazypizzaDBEntities())
            {
                // Retorna uma lista de categorias, ordenadas pelo campo "Categoria1" (nome da categoria)
                return ctx.Categoria
                         .OrderBy(c => c.Categoria1) // Ordenação alfabética
                         .ToList(); // Converte para uma lista
            }
        }
    }
}
