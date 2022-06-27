using AplicacaoEFCore.Pagination;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Odisseu.Infrastructure.TestHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace AplicacaoEFCore.Testes
{

    [TestClass]
    public class EFTests : EFConfigurationTester<MeuDbContext>
    {
        public static class Serializer<TResult>
        {
            public static TResult Deserialize(string input)
            {
                if (input is TResult resultString)
                    return resultString;

                TResult result = JsonConvert.DeserializeObject<TResult>(input);
                return result;

            }
            public static string Deserialize<TInput>(TInput input)
            {
                return System.Text.Json.JsonSerializer.Serialize<TInput>(input);
            }
        }




        [TestMethod]
        public void Test()
        {
            List<Cliente> lista = new()
            {

                new("Ulisses", "Rua Teste", "015664"),
                new("Outro", "Rua Beta", "0127"),
                new("Outro", "Rua Alfa", "0123"),
                new("NovaPagina2", "Endereco Qualquer", "456")
            };

            ClienteFilter filter = new();
            filter.OrdersBy.Add(new(SortType.Descending, nameof(ClienteFilter.Name)));
            filter.OrdersBy.Add(new(SortType.Ascending, nameof(ClienteFilter.Address)));
            filter.MaxRecordsPerPage = 3;
            filter.Page = 1;

            string teste = "Esta é uma mensagem";
            string @string = JsonConvert.SerializeObject(teste);
            string ttt = JsonConvert.DeserializeObject<string>(@string);

            Serializer<string>.Deserialize(teste);


            var q = lista.AsQueryable().GetPaginationQueriable(filter);
            var filtered = q.ToList();


            filter.Page = 2;
            var q2 = lista.AsQueryable().GetPaginationQueriable(filter);

            var filter2 = q2.ToList();


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