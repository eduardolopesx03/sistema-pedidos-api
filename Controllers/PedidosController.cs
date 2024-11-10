using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPedidosAPI.Data;
using SistemaPedidosAPI.Models;

namespace SistemaPedidosAPI.Controllers
{
    // Rota para o controlador de pedidos
    [Route("v1/pedidos")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        // Construtor com injeção de dependência para o contexto do banco
        public PedidosController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método GET para retornar todos os pedidos
        [HttpGet]
        public ActionResult<IEnumerable<Pedido>> ObterTodosPedidos()
        {
            // Carrega todos os pedidos, incluindo os produtos relacionados
            return _dbContext.pedidos
                             .Include(p => p.pedidosprodutos)
                             .ThenInclude(pp => pp.produto)
                             .ToList();
        }

        // Método GET para retornar um pedido específico por ID
        [HttpGet("{id}")]
        public ActionResult<Pedido> ObterPedidoPorId(int id)
        {
            // Busca o pedido pelo ID, incluindo os produtos relacionados
            var pedido = _dbContext.pedidos
                                   .Include(p => p.pedidosprodutos)
                                   .ThenInclude(pp => pp.produto)
                                   .FirstOrDefault(p => p.id == id);

            // Retorna 404 se o pedido não for encontrado
            if (pedido == null) return NotFound();
            return pedido;
        }

        // Método POST para criar um novo pedido
        [HttpPost]
        public ActionResult<Pedido> CriarPedido(Pedido pedido)
        {
            // Adiciona o novo pedido ao contexto e salva as alterações
            _dbContext.pedidos.Add(pedido);
            _dbContext.SaveChanges();

            // Retorna o status 201 com a localização do novo pedido
            return CreatedAtAction(nameof(ObterPedidoPorId), new { id = pedido.id }, pedido);
        }

        // Método PUT para atualizar as informações de um pedido existente
        [HttpPut("{id}")]
        public IActionResult AtualizarPedido(int id, Pedido pedido)
        {
            // Verifica se o ID do pedido a ser atualizado corresponde ao ID da URL
            if (id != pedido.id) return BadRequest();

            // Marca o pedido como modificado e salva as alterações
            _dbContext.Entry(pedido).State = EntityState.Modified;
            _dbContext.SaveChanges();

            // Retorna status 204 (sem conteúdo) indicando que a operação foi bem-sucedida
            return NoContent();
        }

        // Método DELETE para remover um pedido por ID
        [HttpDelete("{id}")]
        public IActionResult RemoverPedido(int id)
        {
            // Procura o pedido pelo ID no banco de dados
            var pedido = _dbContext.pedidos.Find(id);

            // Retorna 404 se o pedido não for encontrado
            if (pedido == null) return NotFound();

            // Remove o pedido e salva as alterações
            _dbContext.pedidos.Remove(pedido);
            _dbContext.SaveChanges();

            // Retorna status 204 (sem conteúdo) indicando que a operação foi bem-sucedida
            return NoContent();
        }
    }
}
