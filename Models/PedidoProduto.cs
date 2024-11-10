namespace SistemaPedidosAPI.Models
{
    public class PedidoProduto
    {
        public int pedidoid { get; set; }
        public Pedido pedido { get; set; }
        public int produtoid { get; set; }
        public Produto produto { get; set; }
        public int quantidade { get; set; }
    }
}
