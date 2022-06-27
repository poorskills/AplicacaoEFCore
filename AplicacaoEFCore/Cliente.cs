using System.Text.Json.Serialization;

namespace AplicacaoEFCore
{




    public enum SortType
    {
        Ascending,
        Descending
    }
    public class OrderByInstruction
    {
        public OrderByInstruction(SortType sortType, string? propertyName)
        {
            SortType = sortType;
            PropertyName = propertyName;
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SortType SortType { get; set; }
        public string? PropertyName { get; set; }
    }


    public class Cliente
    {
        [JsonIgnore]
        public Func<Cliente, bool> GetById => (x) => x.Nome == Nome && x.Telefone == Telefone;

        public Cliente(string? nome, string? endereco, string? telefone)
        {
            Nome = nome;
            Endereco = endereco;
            Telefone = telefone;
        }

        public Guid Id { get; private set; }
        public string? Nome { get; set; }
        public string? Endereco { get; set; }
        public string? Telefone { get; set; }

    }
}
