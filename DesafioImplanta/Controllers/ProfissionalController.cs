using DesafioImplanta.Data;
using DesafioImplanta.Models;
using DesafioImplanta.Util;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;


namespace DesafioImplanta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfissionalController : ControllerBase
    {
        private readonly DesafioContext _context;

        public ProfissionalController(DesafioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> Get([FromQuery] string? nome = null,
                                             [FromQuery] int? numeroRegistroInicio = null,
                                             [FromQuery] int? numeroRegistroFim = null,
                                             [FromQuery] bool? exibirSomenteAtivos = null)
        {
            var profissionais = _context.Profissionals
                .OrderBy(x => x.NomeCompleto)
                .Select(prof => new 
                {
                    NomeCompleto = prof.NomeCompleto,
                    CPF = prof.CPF,
                    NumeroRegistro = prof.NumeroRegistro,
                    Ativo = prof.Ativo,
                    DataCriacao = prof.DataCriacao
                });

            if (!string.IsNullOrEmpty(nome))
                profissionais = profissionais.Where(x => x.NomeCompleto.Contains(nome));
            
            if (numeroRegistroInicio != null)
                profissionais = profissionais.Where(x => x.NumeroRegistro >= numeroRegistroInicio);
            
            if (numeroRegistroFim != null)
                profissionais = profissionais.Where(x => x.NumeroRegistro <= numeroRegistroFim);  
            
            if (exibirSomenteAtivos == true)
                profissionais = profissionais.Where(x => x.Ativo == true);

            return Ok(await profissionais.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Profissional>>> Get(int id)
        {
            return Ok(await _context.Profissionals.FirstOrDefaultAsync(x => x.Id == id) ?? new Profissional());
        }

        [HttpPost]
        public async Task<ActionResult<List<Profissional>>> Post([FromBody] Profissional profissional)
        {
            if (profissional.DataNascimento.AddYears(18) > DateTime.Now)
                return BadRequest("Profissional deve ter mais de 18 anos");
            
            if (!Validador.ValidarCPF(profissional.CPF))
                return BadRequest("CPF inválido");
            
            if (_context.Profissionals.Any(x => x.NomeCompleto == profissional.NomeCompleto))
                return BadRequest("Nome já existe");
            
            int ultimoRegistro = _context.Profissionals.OrderByDescending(x => x.NumeroRegistro).Select(x => x.NumeroRegistro).FirstOrDefault();
            profissional.NumeroRegistro = ultimoRegistro + 1;
            profissional.DataCriacao = DateTime.Now;

            _context.Profissionals.Add(profissional);
            await _context.SaveChangesAsync();

            return StatusCode(HttpStatusCode.Created.GetHashCode(), profissional);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Profissional>>> Put(int id, [FromBody] Profissional request)
        {
            if (request.DataNascimento.AddYears(18) > DateTime.Now)
                return BadRequest("Profissional deve ter mais de 18 anos");

            if (!Validador.ValidarCPF(request.CPF))
                return BadRequest("CPF inválido");

            if (_context.Profissionals.Any(x => x.NomeCompleto == request.NomeCompleto && x.Id != id))
                return BadRequest("Nome já existe");
            
            var profissional = _context.Profissionals.FirstOrDefault(x => x.Id == id);
            if (profissional == null)
                return BadRequest("Não existe profissional com este id");
            
            profissional.NomeCompleto = request.NomeCompleto;
            profissional.CPF = request.CPF;
            profissional.DataNascimento = request.DataNascimento;
            profissional.Sexo = request.Sexo;
            profissional.Ativo = request.Ativo;
            profissional.CEP = request.CEP;
            profissional.Cidade = request.Cidade;

            await _context.SaveChangesAsync();

            return Ok(profissional);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var profissional = _context.Profissionals.FirstOrDefault(x => x.Id == id);
            if (profissional == null)
                return BadRequest("Não existe profissional com este id");
            
            _context.Profissionals.Remove(profissional);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
