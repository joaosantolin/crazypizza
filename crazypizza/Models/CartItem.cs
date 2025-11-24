namespace crazypizza.Models
{
 public class CartItem
 {
 public int ProdutoId { get; set; }
 public string Nome { get; set; }
 public decimal Preco { get; set; }
 public int Quantidade { get; set; }
 public decimal Subtotal => Preco * Quantidade;
 }
}