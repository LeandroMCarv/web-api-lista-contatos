using ApiContatos.Data;
using ApiContatos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

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
                return NotFound(new {mensagem = "Contato não encontrado."});
            }
            return Ok(contato);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contato contato) {
            if (!EmailValido(contato.Email))
            {
                return BadRequest(new { mensagem = "E-mail incorreto!" });
            }
            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPorId),new {id = contato.Id}, contato);
        }

        private bool EmailValido(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);
            if (contato == null)
            {
                return NotFound();
            }
            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}