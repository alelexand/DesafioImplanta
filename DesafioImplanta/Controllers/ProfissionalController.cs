using DesafioImplanta.Data;
using DesafioImplanta.Models;
using Microsoft.AspNetCore.Mvc;

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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProfissionalController>
        [HttpPost]
        public void Post([FromBody] Profissional profissional)
        {
            
            
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
