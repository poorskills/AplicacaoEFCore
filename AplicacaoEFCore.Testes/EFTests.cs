using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Odisseu.Infrastructure.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AplicacaoEFCore.Testes
{

    [TestClass]
    public class EFTests : EFConfigurationTester<MeuDbContext>
    {

        

        [TestMethod]
        public void Test()
        {
            List<Cliente> lista = new()
            {
                
                new("Ulisses", "Rua Teste", "015664"),
                new("Outro", "Rua Alfa", "0123"),
                new("Outro", "Rua Alfa", "0127")

            };
            Cliente desired = new("Outro", "teste", "0123");
            
            var match = lista.Where(o => o.GetById(desired));

            var metadata = DbContextHelper.GetMetaData(GetDbContext());

        }

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