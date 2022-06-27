namespace AplicacaoEFCore.Pagination
{
    public abstract class PaginationFilterRequest
    {
        public int MaxRecordsPerPage { get; set; }
        public int Page { get; set; }
        public int Skip => (Page - 1) * MaxRecordsPerPage;
        public List<OrderByInstruction> OrdersBy { get; } = new();

        public virtual void FixNames() { }
    }
}
