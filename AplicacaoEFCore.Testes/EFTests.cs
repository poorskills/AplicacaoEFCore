using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Odisseu.Infrastructure.TestHelpers;
using System.Reflection;

namespace AplicacaoEFCore.Testes
{
    [TestClass]
    public class EFTests : EFConfigurationTester<MeuDbContext>
    {
        protected override MeuDbContext GetDbContext()
        {
            var connection = new SqliteConnection(@"Filename=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<MeuDbContext>().UseSqlite(connection);
            MeuDbContext _context = new(options.Options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            return _context;
        }

        protected override Assembly? GetValidatorsAssemlby()
        {
            return Assembly.GetAssembly(typeof(ClienteValidator));
        }
    }
}