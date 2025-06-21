using Etapa_1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalho_Tecnologias.Context;

namespace Trabalho_Tecnologias.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly LocadoraContext _context;

        public VeiculosController(LocadoraContext context)
        {
            _context = context;
        }

        // GET: api/Veiculos (continua igual)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculos([FromQuery] string? modelo, [FromQuery] int? fabricanteId)
        {
            var query = _context.Veiculos
                                .Include(v => v.Fabricante)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(modelo))
            {
                query = query.Where(v => v.Modelo.Contains(modelo));
            }

            if (fabricanteId.HasValue)
            {
                query = query.Where(v => v.ID_Fabricante == fabricanteId.Value);
            }

            return await query.ToListAsync();
        }

        // GET: api/Veiculos/5 (continua igual)
        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> GetVeiculo(int id)
        {
            var veiculo = await _context.Veiculos
                                        .Include(v => v.Fabricante)
                                        .FirstOrDefaultAsync(v => v.ID_Veiculo == id);

            if (veiculo == null)
            {
                return NotFound();
            }

            return veiculo;
        }

        // POST: api/Veiculos (MÉTODO COM LÓGICA À PROVA DE FALHAS)
        [HttpPost]
        public async Task<ActionResult<Veiculo>> PostVeiculo(VeiculoCreateDTO veiculoDto)
        {
            // Valida se o fabricante existe
            if (!await _context.Fabricantes.AnyAsync(f => f.ID_Fabricante == veiculoDto.ID_Fabricante))
            {
                return BadRequest("O fabricante especificado não existe.");
            }

            // Cria a nova entidade Veiculo
            var novoVeiculo = new Veiculo
            {
                Modelo = veiculoDto.Modelo,
                Ano = veiculoDto.Ano,
                Quilomentragens = veiculoDto.Quilomentragens,
                ID_Fabricante = veiculoDto.ID_Fabricante
            };

            // Adiciona e Salva para obter o ID
            _context.Veiculos.Add(novoVeiculo);
            await _context.SaveChangesAsync();

            // --- A SOLUÇÃO DEFINITIVA ---
            // 1. "Desanexa" o objeto que acabámos de criar do controlo do Entity Framework.
            //    Isto limpa qualquer estado em cache que possa estar incompleto.
            _context.Entry(novoVeiculo).State = EntityState.Detached;

            // 2. Realiza uma consulta completamente nova à base de dados para obter
            //    o veículo recém-criado, forçando a inclusão do Fabricante.
            var veiculoCriadoCompleto = await _context.Veiculos
                                                      .Include(v => v.Fabricante)
                                                      .FirstOrDefaultAsync(v => v.ID_Veiculo == novoVeiculo.ID_Veiculo);

            // 3. Retorna o objeto que temos a garantia de estar 100% completo.
            return CreatedAtAction(nameof(GetVeiculo), new { id = veiculoCriadoCompleto.ID_Veiculo }, veiculoCriadoCompleto);
        }

        // PUT: api/Veiculos/5 (continua igual)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeiculo(int id, VeiculoCreateDTO veiculoDto)
        {
            var veiculoParaAtualizar = await _context.Veiculos.FindAsync(id);
            if (veiculoParaAtualizar == null)
            {
                return NotFound();
            }

            if (!await _context.Fabricantes.AnyAsync(f => f.ID_Fabricante == veiculoDto.ID_Fabricante))
            {
                return BadRequest("O fabricante especificado não existe.");
            }

            veiculoParaAtualizar.Modelo = veiculoDto.Modelo;
            veiculoParaAtualizar.Ano = veiculoDto.Ano;
            veiculoParaAtualizar.Quilomentragens = veiculoDto.Quilomentragens;
            veiculoParaAtualizar.ID_Fabricante = veiculoDto.ID_Fabricante;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Veiculos/5 (continua igual)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeiculo(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
