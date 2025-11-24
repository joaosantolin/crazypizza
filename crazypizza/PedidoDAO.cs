using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using crazypizza.Models;
using System.Data.Entity.Core.EntityClient;

namespace crazypizza
{
    public class PedidoDAO
    {
        // MÉTODO PRINCIPAL – CRIAR PEDIDO
        // Responsável por:
        // 1. Validar itens
        // 2. Validar estoque
        // 3. Criar o pedido
        // 4. Inserir os itens
        // 5. Decrementar estoque
        // 6. Confirmar tudo em TRANSACTION
        // Mantendo versão ADO.NET; ajustando para validar e decrementar estoque
        public static string CriarPedido(Pedido pedido)
        {
            if (pedido == null) return "Pedido inválido";
            if (pedido.ItemPedidos == null || pedido.ItemPedidos.Count == 0) return "Pedido sem itens";
            try
            {
                using (var conn = new SqlConnection(GetSqlConnectionString()))
                {
                    conn.Open();
                    using (var tx = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        EnsureSchema(conn, tx);

                        //1) Validar estoque para todos os itens
                        foreach (var it in pedido.ItemPedidos)
                        {
                            const string checkSql = "SELECT Nome, Estoque FROM Produtos WITH (UPDLOCK, ROWLOCK) WHERE Id=@Id";
                            using (var cmd = new SqlCommand(checkSql, conn, tx))
                            {
                                cmd.Parameters.AddWithValue("@Id", it.ProdutoId);
                                using (var rd = cmd.ExecuteReader())
                                {
                                    if (!rd.Read())
                                    {
                                        tx.Rollback();
                                        return $"Produto {it.ProdutoId} não encontrado";
                                    }
                                    int estoque = rd.GetInt32(1);
                                    if (estoque < it.Quantidade)
                                    {
                                        tx.Rollback();
                                        return $"Estoque insuficiente para o produto {it.ProdutoId}. Disponível: {estoque}, solicitado: {it.Quantidade}";
                                    }
                                }
                            }
                        }

                        //2) Calcular total se necessário e inserir pedido
                        if (pedido.Total <= 0)
                        {
                            pedido.Total = pedido.ItemPedidos.Sum(x => x.Subtotal);
                        }
                        const string insertPedido = @"INSERT INTO Pedido(UsuarioId, DataCriacao, Status, Total, EnderecoEntrega)
                                                        VALUES(@UsuarioId, @DataCriacao, @Status, @Total, @EnderecoEntrega);
                                                        SELECT CAST(SCOPE_IDENTITY() as int);";
                        using (var cmd = new SqlCommand(insertPedido, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@UsuarioId", pedido.UsuarioId);
                            cmd.Parameters.AddWithValue("@DataCriacao", pedido.DataCriacao == default(DateTime) ? DateTime.Now : pedido.DataCriacao);
                            cmd.Parameters.AddWithValue("@Status", (object)pedido.Status ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Total", pedido.Total);
                            cmd.Parameters.AddWithValue("@EnderecoEntrega", (object)pedido.EnderecoEntrega ?? (object)DBNull.Value);
                            pedido.PedidoId = (int)cmd.ExecuteScalar();
                        }

                        //3) Inserir itens e decrementar estoque
                        const string insertItem = @"INSERT INTO ItemPedido(PedidoId, ProdutoId, Quantidade, PrecoUnitario, Subtotal)
                                                        VALUES(@PedidoId, @ProdutoId, @Quantidade, @PrecoUnitario, @Subtotal);";
                        const string updateStock = "UPDATE Produtos SET Estoque = Estoque - @Quantidade WHERE Id=@ProdutoId";
                        foreach (var it in pedido.ItemPedidos)
                        {
                            using (var cmd = new SqlCommand(insertItem, conn, tx))
                            {
                                cmd.Parameters.AddWithValue("@PedidoId", pedido.PedidoId);
                                cmd.Parameters.AddWithValue("@ProdutoId", it.ProdutoId);
                                cmd.Parameters.AddWithValue("@Quantidade", it.Quantidade);
                                cmd.Parameters.AddWithValue("@PrecoUnitario", it.PrecoUnitario);
                                cmd.Parameters.AddWithValue("@Subtotal", it.Subtotal);
                                cmd.ExecuteNonQuery();
                            }
                            using (var cmd2 = new SqlCommand(updateStock, conn, tx))
                            {
                                cmd2.Parameters.AddWithValue("@Quantidade", it.Quantidade);
                                cmd2.Parameters.AddWithValue("@ProdutoId", it.ProdutoId);
                                cmd2.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();
                        return "Pedido criado com sucesso!";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static string GetSqlConnectionString()
        {
            var entityConn = ConfigurationManager.ConnectionStrings["crazypizzaDBEntities"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(entityConn))
                throw new InvalidOperationException("Connection string 'crazypizzaDBEntities' não encontrada no Web.config.");
            var builder = new EntityConnectionStringBuilder(entityConn);
            return builder.ProviderConnectionString;
        }
        private static void EnsureSchema(SqlConnection conn, SqlTransaction tx)
        {
            string sql = @"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pedido]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[Pedido](
                                [PedidoId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                                [UsuarioId] INT NOT NULL,
                                [DataCriacao] DATETIME NOT NULL,
                                [Status] NVARCHAR(50),
                                [Total] DECIMAL(18,2) NOT NULL DEFAULT(0),
                                [EnderecoEntrega] NVARCHAR(255)
                            );
                        END;
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemPedido]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[ItemPedido](
                                [ItemPedidoId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                                [PedidoId] INT NOT NULL,
                                [ProdutoId] INT NOT NULL,
                                [Quantidade] INT NOT NULL,
                                [PrecoUnitario] DECIMAL(18,2) NOT NULL,
                                [Subtotal] DECIMAL(18,2) NOT NULL
                            );
                            ALTER TABLE [dbo].[ItemPedido] WITH CHECK ADD CONSTRAINT [FK_ItemPedido_Pedido] FOREIGN KEY([PedidoId]) REFERENCES [dbo].[Pedido] ([PedidoId]);
                        END;";
            using (var cmd = new SqlCommand(sql, conn, tx))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Pedido> ListarPorUsuario(int usuarioId)
        {
            var lista = new List<Pedido>();
            using (var conn = new SqlConnection(GetSqlConnectionString()))
            {
                conn.Open();
                const string sql = "SELECT PedidoId, UsuarioId, DataCriacao, Status, Total, EnderecoEntrega FROM Pedido WHERE UsuarioId=@UsuarioId ORDER BY PedidoId DESC";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Pedido
                            {
                                PedidoId = rd.GetInt32(0),
                                UsuarioId = rd.GetInt32(1),
                                DataCriacao = rd.GetDateTime(2),
                                Status = rd.IsDBNull(3) ? null : rd.GetString(3),
                                Total = rd.GetDecimal(4),
                                EnderecoEntrega = rd.IsDBNull(5) ? null : rd.GetString(5),
                                ItemPedidos = new List<ItemPedido>()
                            });
                        }
                    }
                }
            }
            return lista;
        }
        public static List<Pedido> ListarTodos()
        {
            var lista = new List<Pedido>();
            using (var conn = new SqlConnection(GetSqlConnectionString()))
            {
                conn.Open();
                const string sql = "SELECT PedidoId, UsuarioId, DataCriacao, Status, Total, EnderecoEntrega FROM Pedido ORDER BY PedidoId DESC";
                using (var cmd = new SqlCommand(sql, conn))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        lista.Add(new Pedido
                        {
                            PedidoId = rd.GetInt32(0),
                            UsuarioId = rd.GetInt32(1),
                            DataCriacao = rd.GetDateTime(2),
                            Status = rd.IsDBNull(3) ? null : rd.GetString(3),
                            Total = rd.GetDecimal(4),
                            EnderecoEntrega = rd.IsDBNull(5) ? null : rd.GetString(5),
                            ItemPedidos = new List<ItemPedido>()
                        });
                    }
                }
            }
            return lista;
        }
        public static Pedido ObterPorId(int id)
        {
            Pedido pedido = null;
            using (var conn = new SqlConnection(GetSqlConnectionString()))
            {
                conn.Open();
                const string sql = "SELECT PedidoId, UsuarioId, DataCriacao, Status, Total, EnderecoEntrega FROM Pedido WHERE PedidoId=@Id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            pedido = new Pedido
                            {
                                PedidoId = rd.GetInt32(0),
                                UsuarioId = rd.GetInt32(1),
                                DataCriacao = rd.GetDateTime(2),
                                Status = rd.IsDBNull(3) ? null : rd.GetString(3),
                                Total = rd.GetDecimal(4),
                                EnderecoEntrega = rd.IsDBNull(5) ? null : rd.GetString(5),
                                ItemPedidos = new List<ItemPedido>()
                            };
                        }
                    }
                }
                if (pedido != null)
                {
                    const string sqlItens = "SELECT ItemPedidoId, PedidoId, ProdutoId, Quantidade, PrecoUnitario, Subtotal FROM ItemPedido WHERE PedidoId=@PedidoId";
                    using (var cmd2 = new SqlCommand(sqlItens, conn))
                    {
                        cmd2.Parameters.AddWithValue("@PedidoId", pedido.PedidoId);
                        using (var rd2 = cmd2.ExecuteReader())
                        {
                            while (rd2.Read())
                            {
                                pedido.ItemPedidos.Add(new ItemPedido
                                {
                                    ItemPedidoId = rd2.GetInt32(0),
                                    PedidoId = rd2.GetInt32(1),
                                    ProdutoId = rd2.GetInt32(2),
                                    Quantidade = rd2.GetInt32(3),
                                    PrecoUnitario = rd2.GetDecimal(4),
                                    Subtotal = rd2.GetDecimal(5)
                                });
                            }
                        }
                    }
                }
            }
            return pedido;
        }
        public static string AtualizarStatus(int pedidoId, string status)
        {
            try
            {
                using (var conn = new SqlConnection(GetSqlConnectionString()))
                {
                    conn.Open();
                    // Cancelamento: restaura estoques e remove pedido/itens
                    if (!string.IsNullOrEmpty(status) && status.Trim().Equals("Cancelado", StringComparison.OrdinalIgnoreCase))
                    {
                        using (var tx = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                        {
                            // Buscar itens
                            var itens = new List<(int ProdutoId, int Quantidade)>();
                            const string selItens = "SELECT ProdutoId, Quantidade FROM ItemPedido WHERE PedidoId=@Id";
                            using (var cmdSel = new SqlCommand(selItens, conn, tx))
                            {
                                cmdSel.Parameters.AddWithValue("@Id", pedidoId);
                                using (var rd = cmdSel.ExecuteReader())
                                {
                                    while (rd.Read())
                                    {
                                        itens.Add((rd.GetInt32(0), rd.GetInt32(1)));
                                    }
                                }
                            }
                            // Restaurar estoque
                            const string updEstoque = "UPDATE Produtos SET Estoque = Estoque + @Qtd WHERE Id=@Prod";
                            foreach (var it in itens)
                            {
                                using (var cmdUp = new SqlCommand(updEstoque, conn, tx))
                                {
                                    cmdUp.Parameters.AddWithValue("@Qtd", it.Quantidade);
                                    cmdUp.Parameters.AddWithValue("@Prod", it.ProdutoId);
                                    cmdUp.ExecuteNonQuery();
                                }
                            }
                            // Excluir itens e pedido
                            using (var cmdDelItens = new SqlCommand("DELETE FROM ItemPedido WHERE PedidoId=@Id", conn, tx))
                            {
                                cmdDelItens.Parameters.AddWithValue("@Id", pedidoId);
                                cmdDelItens.ExecuteNonQuery();
                            }
                            using (var cmdDelPed = new SqlCommand("DELETE FROM Pedido WHERE PedidoId=@Id", conn, tx))
                            {
                                cmdDelPed.Parameters.AddWithValue("@Id", pedidoId);
                                var rows = cmdDelPed.ExecuteNonQuery();
                                tx.Commit();
                                return rows > 0 ? "Pedido cancelado e removido" : "Pedido não encontrado";
                            }
                        }
                    }
                    else
                    {
                        const string sql = "UPDATE Pedido SET Status=@Status WHERE PedidoId=@Id";
                        using (var cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@Status", (object)status ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Id", pedidoId);
                            int linhas = cmd.ExecuteNonQuery();
                            return linhas > 0 ? "Status atualizado" : "Pedido não encontrado";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}