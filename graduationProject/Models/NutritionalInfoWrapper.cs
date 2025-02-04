using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace graduationProject.Models
{
    public class NutritionalInfoWrapper // Wrapper to structure the JSON response
    {
        [JsonPropertyName("nutritional_info")]
        public List<NutritionalItem> NutritionalInfo { get; set; }
    }

    public class NutritionalItem
    {
        [JsonPropertyName("Item")]
        public string Item { get; set; }

        [JsonPropertyName("Calories per 100 gms")]
        public string Calories_per_100_gms { get; set; }

        [JsonPropertyName("Protein per 100 gms")]
        public string Protein_per_100_gms { get; set; }

        [JsonPropertyName("Carbs per 100 gms")]
        public string Carbs_per_100_gms { get; set; }

        [JsonPropertyName("Fats per 100 gms")]
        public string Fats_per_100_gms { get; set; }

        [JsonPropertyName("Sugar per 100 gms")]
        public string Sugar_per_100_gms { get; set; }
    }

}
