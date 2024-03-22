using AgendaContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaContatos.Data
{
    public class BancoContent : DbContext
    {
        public BancoContent(DbContextOptions<BancoContent> options) : base(options)
        {
        }

        public DbSet<ContatoModel> Contatos { get; set; }
    }
}