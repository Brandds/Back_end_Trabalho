using Etapa_1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalho_Tecnologias.Context;

namespace Trabalho_Tecnologias.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlugueisController : ControllerBase
    {
        private readonly LocadoraContext _context;

        public AlugueisController(LocadoraContext context)
        {
            _context = context;
        }

        // GET: api/Alugueis (continua igual)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluguel>>> GetAlugueis()
        {
            return await _context.Alugueis
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                    .ThenInclude(v => v.Fabricante)
                .ToListAsync();
        }

        // GET: api/Alugueis/5 (continua igual)
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluguel>> GetAluguel(int id)
        {
            var aluguel = await _context.Alugueis
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                    .ThenInclude(v => v.Fabricante)
                .FirstOrDefaultAsync(a => a.ID_Aluguel == id);

            if (aluguel == null)
            {
                return NotFound();
            }

            return aluguel;
        }

        // POST: api/Alugueis (MÉTODO ATUALIZADO)
        /// <summary>
        /// Cria um novo registro de aluguel.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Aluguel>> PostAluguel(AluguelCreateDTO aluguelDto)
        {
            var cliente = await _context.Clientes.FindAsync(aluguelDto.ID_Cliente);
            var veiculo = await _context.Veiculos.FindAsync(aluguelDto.ID_Veiculo);

            if (cliente == null)
            {
                return BadRequest("O cliente especificado não existe.");
            }
            if (veiculo == null)
            {
                return BadRequest("O veículo especificado não existe.");
            }

            // Cria a entidade Aluguel a partir do DTO
            var novoAluguel = new Aluguel
            {
                Data_Inicio = aluguelDto.Data_Inicio,
                Data_Fim = aluguelDto.Data_Fim,
                Valor_Total = aluguelDto.Valor_Total,
                ID_Cliente = aluguelDto.ID_Cliente,
                ID_Veiculo = aluguelDto.ID_Veiculo
            };

            _context.Alugueis.Add(novoAluguel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAluguel), new { id = novoAluguel.ID_Aluguel }, novoAluguel);
        }

        // PUT: api/Alugueis/5 (Também deve ser atualizado para usar um DTO, mas vamos focar no POST por agora)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluguel(int id, AluguelCreateDTO aluguelDto) // Usando DTO para update também
        {
            var aluguelParaAtualizar = await _context.Alugueis.FindAsync(id);
            if (aluguelParaAtualizar == null)
            {
                return NotFound();
            }

            aluguelParaAtualizar.ID_Cliente = aluguelDto.ID_Cliente;
            aluguelParaAtualizar.ID_Veiculo = aluguelDto.ID_Veiculo;
            aluguelParaAtualizar.Data_Inicio = aluguelDto.Data_Inicio;
            aluguelParaAtualizar.Data_Fim = aluguelDto.Data_Fim;
            aluguelParaAtualizar.Valor_Total = aluguelDto.Valor_Total;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Alugueis/5 (continua igual)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluguel(int id)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);
            if (aluguel == null)
            {
                return NotFound();
            }

            _context.Alugueis.Remove(aluguel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
