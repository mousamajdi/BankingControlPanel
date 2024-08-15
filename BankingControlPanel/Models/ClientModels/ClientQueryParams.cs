using BankingControlPanel.Enums;

namespace BankingControlPanel.Models.ClientModels
{
    public class ClientQueryParams
    {
        public string? SearchTerm { get; set; }
        public SortByOptions? SortBy { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
