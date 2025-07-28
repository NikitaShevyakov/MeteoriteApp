namespace MeteoriteApp.Domain.Models
{
    public class Classification
    {
        public Classification(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
