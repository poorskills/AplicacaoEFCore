using Microsoft.EntityFrameworkCore;

namespace AplicacaoEFCore
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cliente>? Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(o =>
            {
                o.Property(x => x.Nome).IsRequired().HasMaxLength(50);
                o.Property(x => x.Telefone).IsRequired().HasMaxLength(7);
                o.HasKey(x => x.Id);
            });
        }

    }
}
