namespace AplicacaoEFCore.Pagination
{
    public class PaginationRespose
    {
        public int MaxRecordsPerPage { get; set; }
        public int Page { get; set; }
        public int TotalRecords { get; set; }
    }
}
