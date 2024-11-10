using Microsoft.EntityFrameworkCore;
using SistemaPedidosAPI.Models;

namespace SistemaPedidosAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Produto> produtos { get; set; }
        public DbSet<Pedido> pedidos { get; set; }
        public DbSet<PedidoProduto> pedidosprodutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PedidoProduto>()
                .HasKey(pp => new { pp.pedidoid, pp.produtoid });

            modelBuilder.Entity<PedidoProduto>()
                .HasOne(pp => pp.pedido)
                .WithMany(p => p.pedidosprodutos)
                .HasForeignKey(pp => pp.pedidoid);

            modelBuilder.Entity<PedidoProduto>()
                .HasOne(pp => pp.produto)
                .WithMany()
                .HasForeignKey(pp => pp.produtoid);
        }
    }
}
