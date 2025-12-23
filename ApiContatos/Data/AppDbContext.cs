using ApiContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiContatos.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Contato> Contatos { get; set; }
    }
}
