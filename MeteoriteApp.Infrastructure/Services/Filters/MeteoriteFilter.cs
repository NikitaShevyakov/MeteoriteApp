namespace MeteoriteApp.Infrastructure.Services.Filters
{   
    public class MeteoriteFilter
    {
        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
        public int? ClassificationCode { get; set; }
        public string NameContains { get; set; } = string.Empty;

        public string SortBy { get; set; } = "year";
        public string SortOrder { get; set; } = "asc";

        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 50;
    }    
}
