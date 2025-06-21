using Etapa_1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalho_Tecnologias.Context;

namespace Trabalho_Tecnologias.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly LocadoraContext _context;

        public ClientesController(LocadoraContext context)
        {
            _context = context;
        }

        // GET: api/Clientes?nome=Joao&email=joao@email.com
        /// <summary>
        /// Obtém uma lista de clientes, com filtros opcionais por nome e email.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes([FromQuery] string? nome, [FromQuery] string? email)
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(c => c.Nome.Contains(nome));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(c => c.Email.Contains(email));
            }

            return await query.ToListAsync();
        }

        // GET: api/Clientes/5
        /// <summary>
        /// Obtém um cliente específico pelo seu ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound(); // Retorna 404 se o cliente não for encontrado
            }

            return cliente;
        }

        // POST: api/Clientes
        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            // Retorna 201 Created com um link para o novo recurso e o objeto criado
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.ID_Cliente }, cliente);
        }

        // PUT: api/Clientes/5
        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.ID_Cliente)
            {
                return BadRequest("O ID na URL não corresponde ao ID no corpo da requisição.");
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Clientes.Any(e => e.ID_Cliente == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Retorna 204 No Content em caso de sucesso
        }

        // DELETE: api/Clientes/5
        /// <summary>
        /// Exclui um cliente pelo seu ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
