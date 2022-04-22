using Microsoft.AspNetCore.Mvc;

namespace AplicacaoEFCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly MeuDbContext _context;
        public ClienteController(MeuDbContext meuDbContext)
        {
            _context = meuDbContext;
        }

        [HttpGet("ListAll")]
        public IActionResult Get()
        {
            return Ok(_context.Clientes.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var cliente = _context.Clientes.FirstOrDefault(x => x.Id == id);
            if (cliente == default)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Add(Cliente cliente)
        {
            var entry = _context.Clientes.Add(cliente);
            var result = _context.SaveChanges();
            if (result == 1)
                return CreatedAtAction(nameof(GetById), new { id = entry.Entity.Id }, entry.Entity);
            return BadRequest();
        }
    }
}
