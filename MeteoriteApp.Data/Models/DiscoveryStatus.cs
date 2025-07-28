namespace MeteoriteApp.Domain.Models
{
    public class DiscoveryStatus
    {
        public DiscoveryStatus(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
