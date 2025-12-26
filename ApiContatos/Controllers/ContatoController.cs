using ApiContatos.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiContatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ContatoController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var contatos = await _context.Contatos.ToListAsync();
            return Ok(contatos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id) {
            var contato = await _context.Contatos.FindAsync(id);
            if (contato == null)
            {
                return NotFound();
            }
            return Ok(contato);
        }
    }
}