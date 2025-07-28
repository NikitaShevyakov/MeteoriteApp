namespace MeteoriteApp.Infrastructure.Services.Filters
{
    public class PagedResult<T>
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
