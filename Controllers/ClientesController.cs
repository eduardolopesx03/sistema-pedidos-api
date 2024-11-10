using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPedidosAPI.Data;
using SistemaPedidosAPI.Models;

namespace SistemaPedidosAPI.Controllers
{
    // Definindo a rota para o controlador de clientes
    [Route("v1/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        // Injeção de dependência para o contexto do banco de dados
        public ClientesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método GET para retornar todos os clientes
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> ObterTodosClientes()
        {
            // Retorna a lista de clientes a partir do contexto
            return _dbContext.clientes.ToList();
        }

        // Método GET para retornar um cliente específico por ID
        [HttpGet("{id}")]
        public ActionResult<Cliente> ObterClientePorId(int id)
        {
            // Busca o cliente pelo ID no banco de dados
            var cliente = _dbContext.clientes.FirstOrDefault(c => c.id == id);

            // Retorna um erro 404 se o cliente não for encontrado
            if (cliente == null) return NotFound();
            return cliente;
        }

        // Método POST para adicionar um novo cliente
        [HttpPost]
        public ActionResult<Cliente> AdicionarCliente(Cliente cliente)
        {
            // Adiciona o novo cliente ao banco de dados
            _dbContext.clientes.Add(cliente);
            _dbContext.SaveChanges();

            // Retorna um status 201 com a localização do novo recurso
            return CreatedAtAction(nameof(ObterClientePorId), new { id = cliente.id }, cliente);
        }

        // Método PUT para atualizar as informações de um cliente existente
        [HttpPut("{id}")]
        public IActionResult AtualizarCliente(int id, Cliente cliente)
        {
            // Verifica se o ID do cliente a ser atualizado corresponde ao ID da URL
            if (id != cliente.id) return BadRequest();

            // Marca o cliente como modificado e salva as alterações no banco
            _dbContext.Entry(cliente).State = EntityState.Modified;
            _dbContext.SaveChanges();

            // Retorna um status 204 (sem conteúdo) indicando sucesso
            return NoContent();
        }

        // Método DELETE para remover um cliente por ID
        [HttpDelete("{id}")]
        public IActionResult RemoverCliente(int id)
        {
            // Procura o cliente pelo ID no banco de dados
            var cliente = _dbContext.clientes.Find(id);

            // Retorna um erro 404 caso o cliente não exista
            if (cliente == null) return NotFound();

            // Remove o cliente encontrado e salva as alterações
            _dbContext.clientes.Remove(cliente);
            _dbContext.SaveChanges();

            // Retorna um status 204 (sem conteúdo) indicando sucesso
            return NoContent();
        }
    }
}
