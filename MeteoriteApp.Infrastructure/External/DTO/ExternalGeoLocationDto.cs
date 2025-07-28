using System.Text.Json.Serialization;

namespace MeteoriteApp.Infrastructure.External.DTO
{
    public class ExternalGeoLocationDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<double> Coordinates { get; set; }

        public double Longitude => Coordinates != null && Coordinates.Count > 0 ? Coordinates[0] : 0;
        public double Latitude => Coordinates != null && Coordinates.Count > 1 ? Coordinates[1] : 0;

    }
}
