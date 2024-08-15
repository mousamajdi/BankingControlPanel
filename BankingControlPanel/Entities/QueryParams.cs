using System;

namespace BankingControlPanel.Entities
{
    public class QueryParams
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
