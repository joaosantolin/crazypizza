using System.Data.Entity;

namespace crazypizza.Models
{
 public partial class crazypizzaDBEntities : DbContext
 {
 public virtual DbSet<Pedido> Pedido { get; set; }
 public virtual DbSet<ItemPedido> ItemPedido { get; set; }
 }
}