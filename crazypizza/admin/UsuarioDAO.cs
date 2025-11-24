using crazypizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crazypizza.admin
{
    internal class UsuarioDAO
    {
        // MÉTODO PARA CADASTRAR NOVO USUÁRIO
        internal static string CadastrarUsuario(Usuario usuario)
        {
            string mensagem = string.Empty;
            try
            {
                using (var ctx = new crazypizzaDBEntities()) // contexto do banco (Entity Framework)
                {
                    ctx.Usuario.Add(usuario);    // insere o usuário na tabela
                    ctx.SaveChanges();           // salva no banco
                }
                mensagem = "Usuário cadastrado com sucesso!";
            }
            catch (Exception ex)
            {
                mensagem = ex.Message; // em caso de erro, retorna a mensagem da exceção
            }
            return mensagem;
        }

        // MÉTODO PARA BUSCAR UM USUÁRIO PELO LOGIN (ÚTIL EM LOGIN)
        internal static Usuario ListarUsuario(string login)
        {
            try
            {
                using (var ctx = new crazypizzaDBEntities())
                {
                    return ctx.Usuario.FirstOrDefault(u => u.Login == login);
                }
            }
            catch
            {
                return null; // se der erro ou não achar, retorna null
            }
        }

        // LISTAR TODOS OS USUÁRIOS (ORDENADOS PELO NOME)
        internal static List<Usuario> ListarTodos()
        {
            using (var ctx = new crazypizzaDBEntities())
            {
                return ctx.Usuario.OrderBy(u => u.NomeUsuario).ToList();
            }
        }

        // BUSCAR UM ÚNICO USUÁRIO PELO ID (PARA VISUALIZAR/EDITAR)
        internal static Usuario VisualizarUsuario(int id)
        {
            using (var ctx = new crazypizzaDBEntities())
            {
                return ctx.Usuario.FirstOrDefault(u => u.IdUsuario == id);
            }
        }

        // ATUALIZAR DADOS DO USUÁRIO
        internal static string AlterarUsuario(Usuario usuario)
        {
            try
            {
                using (var ctx = new crazypizzaDBEntities())
                {
                    // busca o usuário existente no banco
                    var existente = ctx.Usuario.FirstOrDefault(u => u.IdUsuario == usuario.IdUsuario);
                    if (existente == null) return "Usuário não encontrado";

                    // atualiza apenas os campos que foram enviados
                    existente.NomeUsuario = usuario.NomeUsuario;
                    existente.Login = usuario.Login;
                    existente.Email = usuario.Email;
                    existente.Admin = usuario.Admin;

                    // a senha só deve ser alterada se o campo vier preenchido
                    if (!string.IsNullOrEmpty(usuario.Senha))
                    {
                        existente.Senha = usuario.Senha; 
                    }

                    ctx.SaveChanges();
                    return "Usuário alterado com sucesso!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message; // retorna erro (se ocorrer)
            }
        }

        // EXCLUIR USUÁRIO PELO ID
        internal static string ExcluirUsuario(int id)
        {
            try
            {
                using (var ctx = new crazypizzaDBEntities())
                {
                    var usuario = ctx.Usuario.FirstOrDefault(u => u.IdUsuario == id);
                    if (usuario == null) return "Usuário não encontrado";

                    ctx.Usuario.Remove(usuario);  // remove o usuário
                    ctx.SaveChanges();
                    return "Usuário excluído com sucesso!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message; // erro se não conseguir excluir
            }
        }
    }
}
