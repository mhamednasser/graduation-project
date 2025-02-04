using System.Text.Json.Serialization;

namespace graduationProject.DTOs
{
    public class DietPlanDto
    {
        [JsonPropertyName("satuday")]
        public DietPlanDayDto Satuday { get; set; }

        [JsonPropertyName("sunday")]
        public DietPlanDayDto Sunday { get; set; }

        [JsonPropertyName("monday")]
        public DietPlanDayDto Monday { get; set; }

        [JsonPropertyName("tuesday")]
        public DietPlanDayDto Tuesday { get; set; }

        [JsonPropertyName("wednesday")]
        public DietPlanDayDto Wednesday { get; set; }

        [JsonPropertyName("thursday")]
        public DietPlanDayDto Thursday { get; set; }

        [JsonPropertyName("friday")]
        public DietPlanDayDto Friday { get; set; }
    }

    public class DietPlanDayDto
    {
        [JsonPropertyName("breakfast")]
        public DietPlanMealDto Breakfast { get; set; }

        [JsonPropertyName("lunch")]
        public DietPlanMealDto Lunch { get; set; }

        [JsonPropertyName("dinner")]
        public DietPlanMealDto Dinner { get; set; }
    }

    public class DietPlanMealDto
    {
        [JsonPropertyName("main")]
        public DietPlanDishDto Main { get; set; }

        [JsonPropertyName("alternatives")]
        public DietPlanDishDto[] Alternatives { get; set; }
    }

    public class DietPlanDishDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("portion")]
        public string Portion { get; set; }

        [JsonPropertyName("calories")]
        public int Calories { get; set; }

        [JsonPropertyName("macronutrients")]
        public DietPlanMacronutrientsDto Macronutrients { get; set; }
    }

    public class DietPlanMacronutrientsDto
    {
        [JsonPropertyName("protein")]
        public int Protein { get; set; }

        [JsonPropertyName("carbs")]
        public int Carbs { get; set; }

        [JsonPropertyName("fat")]
        public int Fat { get; set; }
    }
}
