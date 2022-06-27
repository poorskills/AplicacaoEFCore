using AplicacaoEFCore.Pagination;

namespace AplicacaoEFCore
{
    public class ClienteFilter : PaginationFilterRequest
    {
        public string? Name { get; set; }
        public string? Address { get; set; }

        public override void FixNames()
        {
            foreach (var item in OrdersBy)
            {
                if (item?.PropertyName?.Equals(nameof(Name), StringComparison.OrdinalIgnoreCase) ?? false)
                    item.PropertyName = nameof(Cliente.Nome);
                else if (item?.PropertyName?.Equals(nameof(Address), StringComparison.OrdinalIgnoreCase) ?? false)
                    item.PropertyName = nameof(Cliente.Endereco);
            }
        }
    }
}
