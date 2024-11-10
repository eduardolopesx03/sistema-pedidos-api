using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPedidosAPI.Data;
using SistemaPedidosAPI.Models;

namespace SistemaPedidosAPI.Controllers
{
    // Definindo a rota para o controlador de produtos
    [Route("v1/produtos")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        // Construtor com injeção de dependência para o contexto do banco
        public ProdutosController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Método GET para retornar todos os produtos
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> ObterTodosProdutos()
        {
            // Retorna todos os produtos da tabela produtos
            return _dbContext.produtos.ToList();
        }

        // Método GET para retornar um produto específico por ID
        [HttpGet("{id}")]
        public ActionResult<Produto> ObterProdutoPorId(int id)
        {
            // Busca o produto pelo ID
            var produto = _dbContext.produtos.Find(id);

            // Retorna um erro 404 se o produto não for encontrado
            if (produto == null) return NotFound();
            return produto;
        }

        // Método POST para criar um novo produto
        [HttpPost]
        public ActionResult<Produto> CriarProduto(Produto produto)
        {
            // Adiciona o novo produto ao banco de dados
            _dbContext.produtos.Add(produto);
            _dbContext.SaveChanges();

            // Retorna status 201 com a localização do novo produto
            return CreatedAtAction(nameof(ObterProdutoPorId), new { id = produto.id }, produto);
        }

        // Método PUT para atualizar as informações de um produto existente
        [HttpPut("{id}")]
        public IActionResult AtualizarProduto(int id, Produto produto)
        {
            // Verifica se o ID do produto a ser atualizado corresponde ao ID da URL
            if (id != produto.id) return BadRequest();

            // Marca o produto como modificado e salva as alterações
            _dbContext.Entry(produto).State = EntityState.Modified;
            _dbContext.SaveChanges();

            // Retorna status 204 (sem conteúdo) indicando que a operação foi bem-sucedida
            return NoContent();
        }

        // Método DELETE para remover um produto por ID
        [HttpDelete("{id}")]
        public IActionResult RemoverProduto(int id)
        {
            // Procura o produto pelo ID no banco de dados
            var produto = _dbContext.produtos.Find(id);

            // Retorna 404 se o produto não for encontrado
            if (produto == null) return NotFound();

            // Remove o produto e salva as alterações
            _dbContext.produtos.Remove(produto);
            _dbContext.SaveChanges();

            // Retorna status 204 (sem conteúdo) indicando que a operação foi bem-sucedida
            return NoContent();
        }
    }
}
