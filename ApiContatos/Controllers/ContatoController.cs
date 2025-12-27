using ApiContatos.Contracts.ContatoContracts;
using ApiContatos.Data;
using ApiContatos.Extensions;
using ApiContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiContatos.Controllers;

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

        var response = contatos.Select(contato => new GetContatoResponse
        {
            Id = contato.Id,
            Nome = contato.Nome,
            Telefone = contato.Telefone,
            Email = contato.Email,
            Endereco = contato.Endereco,
            Categoria = contato.Categoria
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var contato = await _context.Contatos.FindAsync(id);
        if (contato == null)
            return NotFound(new { mensagem = "Contato não encontrado." });

        var response = new GetContatoResponse
        {
            Id = contato.Id,
            Nome = contato.Nome,
            Telefone = contato.Telefone,
            Email = contato.Email,
            Categoria = contato.Categoria,
            Endereco = contato.Endereco
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateContatoRequest request)
    {
        if (!ValidationExtensions.EmailValido(request.Email))
            return BadRequest(new { mensagem = "E-mail incorreto!" });

        var contato = new Contato
        {
            Nome = request.Nome,
            Telefone = request.Telefone,
            Email = request.Email,
            Endereco = request.Endereco,
            Categoria = request.Categoria
        };

        await _context.Contatos.AddAsync(contato);
        await _context.SaveChangesAsync();

        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var contato = await _context.Contatos.FindAsync(id);
        if (contato == null)
            return NotFound();

        _context.Contatos.Remove(contato);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateContatoRequest request)
    {
        var contatoExistente = await _context.Contatos.FindAsync(id);

        if (contatoExistente == null)
            return NotFound();

        if (request.Email != null)
        {
            if (!ValidationExtensions.EmailValido(request.Email))
                return BadRequest(new { messagem = "E-mail invalido!" });

            contatoExistente.Email = request.Email;
        }

        if (request.Nome != null)
            contatoExistente.Nome = request.Nome;

        if (request.Telefone != null)
            contatoExistente.Telefone = request.Telefone;

        if (request.Endereco != null)
            contatoExistente.Endereco = request.Endereco;

        if (request.Categoria != null)
            contatoExistente.Categoria = request.Categoria;

        _context.Contatos.Update(contatoExistente);
        await _context.SaveChangesAsync();

        return Ok();
    }
}