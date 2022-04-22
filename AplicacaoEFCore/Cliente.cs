namespace AplicacaoEFCore
{
    public class Cliente
    {
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
