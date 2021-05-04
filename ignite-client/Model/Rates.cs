using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ignite_client.Model
{
    public partial class Rates
    {
        [JsonPropertyName("Date")]
        public DateTimeOffset Date { get; set; }

        [JsonPropertyName("PreviousDate")]
        public DateTimeOffset PreviousDate { get; set; }

        [JsonPropertyName("PreviousURL")]
        public string PreviousUrl { get; set; }

        [JsonPropertyName("Timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("Valute")]
        public Dictionary<string, Currency> Currencies { get; set; }
    }   
}
