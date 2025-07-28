using System.Text.Json.Serialization;

namespace MeteoriteApp.Infrastructure.External.DTO
{
    public class ExternalMeteoriteDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("nametype")]
        public string Category { get; set; }
        [JsonPropertyName("Recclass")]
        public string ClassificationCode { get; set; }
        public string Mass { get; set; }

        [JsonPropertyName("fall")]
        public string DiscoveryStatus { get; set; }

        [JsonPropertyName("year")]
        public string DateDiscovered { get; set; }

        [JsonPropertyName("reclat")]
        public string Latitude { get; set; }

        [JsonPropertyName("reclong")]
        public string Longitude { get; set; }

        [JsonPropertyName("geolocation")]
        public ExternalGeoLocationDto? GeoLocation { get; set; }

        [JsonPropertyName(":@computed_region_cbhk_fwbd")]
        public string? RawRegionByDistrict { get; set; }

        [JsonPropertyName(":@computed_region_nnqa_25f4")]
        public string? RawRegionByGeozone { get; set; }
    }
}
