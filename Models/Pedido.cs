namespace SistemaPedidosAPI.Models
{
    public class Pedido
    {
        public int id { get; set; }
        public int clienteid { get; set; }
        public Cliente cliente { get; set; }
        public string status { get; set; }
        public List<PedidoProduto> pedidosprodutos { get; set; }
    }
}
