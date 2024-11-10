namespace SistemaPedidosAPI.Models
{
    public class Cliente
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string numerocontato { get; set; }
        public DateTime datanascimento { get; set; }
    }
}
