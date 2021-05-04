using System.Text.Json.Serialization;

namespace ignite_client.Model
{
    public partial class Currency
    {
        [JsonPropertyName("ID")]
        public string Id { get; set; }

        [JsonPropertyName("NumCode")]
        public string NumCode { get; set; }

        [JsonPropertyName("CharCode")]
        public string CharCode { get; set; }

        [JsonPropertyName("Nominal")]
        public long Nominal { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Value")]
        public decimal Value { get; set; }

        [JsonPropertyName("Previous")]
        public decimal Previous { get; set; }
    }
}
