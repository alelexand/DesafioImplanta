using DesafioImplanta.Data;
using DesafioImplanta.Models;
using DesafioImplanta.Util;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public IEnumerable<dynamic> Get([FromQuery] string? nome = null,
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

            return profissionais.ToList();
        }

        // GET api/<ProfissionalController>/5
        [HttpGet("{id}")]
        public Profissional Get(int id)
        {
            return _context.Profissionals.FirstOrDefault(x => x.Id == id) ?? new Profissional();
        }

        // POST api/<ProfissionalController>
        [HttpPost]
        public async Task<ActionResult<List<Profissional>>> Post([FromBody] Profissional profissional)
        {
            if (profissional.DataNascimento.AddYears(18) > DateTime.Now)
                return BadRequest("Profissional deve ter mais de 18 anos");
            
            if (Validador.ValidarCPF(profissional.CPF))
                return BadRequest("CPF inválido");
            
            if (_context.Profissionals.Any(x => x.NomeCompleto == profissional.NomeCompleto))
                return BadRequest("Nome já existe");
            
            var ultimoRegistro = _context.Profissionals.OrderByDescending(x => x.NumeroRegistro).Select(x => x.NumeroRegistro).FirstOrDefault();
            profissional.NumeroRegistro = ultimoRegistro + 1;
            profissional.DataCriacao = DateTime.Now;

            _context.Profissionals.Add(profissional);
            await _context.SaveChangesAsync();

            return Ok(await _context.Profissionals.ToListAsync());

        }

        // PUT api/<ProfissionalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProfissionalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
