namespace MeteoriteApp.Infrastructure.Database.Entities.Meteorites
{
    public class ClassificationEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MeteoriteEntity> Meteorites { get; set; }
    }
}
