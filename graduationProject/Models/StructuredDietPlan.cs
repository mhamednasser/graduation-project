using graduationProject.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace graduationProject.Models
{
    public class StructuredDietPlan
    {
        public int Id { get; set; } // Primary key
        public int UserId { get; set; } // Foreign key to User

        [JsonIgnore] // Ignore during serialization to avoid circular reference
        public User User { get; set; }

        // Store the entire plan as JSON
        [Column(TypeName = "nvarchar(max)")] // Use JSONB for PostgreSQL, or "nvarchar(max)" for SQL Server
        public string PlanJson { get; set; }

        // Helper property to serialize/deserialize JSON
        [NotMapped] // This property will not be mapped to the database
        public DietPlanData PlanData
        {
            get => JsonSerializer.Deserialize<DietPlanData>(PlanJson);
            set => PlanJson = JsonSerializer.Serialize(value);
        }
    }

    public class DietPlanData
    {
        [JsonPropertyName("saturday")]
        public DayPlan Saturday { get; set; }

        [JsonPropertyName("sunday")]
        public DayPlan Sunday { get; set; }

        [JsonPropertyName("monday")]
        public DayPlan Monday { get; set; }

        [JsonPropertyName("tuesday")]
        public DayPlan Tuesday { get; set; }

        [JsonPropertyName("wednesday")]
        public DayPlan Wednesday { get; set; }

        [JsonPropertyName("thursday")]
        public DayPlan Thursday { get; set; }

        [JsonPropertyName("friday")]
        public DayPlan Friday { get; set; }
    }

    public class DayPlan
    {
        [JsonPropertyName("breakfast")]
        public Meal Breakfast { get; set; }

        [JsonPropertyName("lunch")]
        public Meal Lunch { get; set; }

        [JsonPropertyName("dinner")]
        public Meal Dinner { get; set; }
    }

    public class Meal
    {
        [JsonPropertyName("main")]
        public Dish Main { get; set; }

        [JsonPropertyName("alternatives")]
        public List<Dish> Alternatives { get; set; } = new List<Dish>();
    }

    public class Dish
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("portion")]
        public string Portion { get; set; }

        [JsonPropertyName("calories")]
        public double Calories { get; set; }

        [JsonPropertyName("macronutrients")]
        public Macronutrients Macronutrients { get; set; }
    }

    public class Macronutrients
    {
        [JsonPropertyName("carbs")]
        public double Carbs { get; set; }

        [JsonPropertyName("protein")]
        public double Protein { get; set; }

        [JsonPropertyName("fat")]
        public double Fat { get; set; }
    }
}





































//namespace graduationProject.Models
//{
//    public class StructuredDietPlan
//    {
//        public string Duration { get; set; }
//        public int DailyCalories { get; set; }
//        public string MacronutrientDistribution { get; set; }
//        public List<WeeklyPlan> WeeklyPlans { get; set; } = new List<WeeklyPlan>();
//        public List<ShoppingListItem> ShoppingList { get; set; } = new List<ShoppingListItem>();
//        public List<HydrationInstruction> HydrationPlan { get; set; } = new List<HydrationInstruction>();
//        public List<ProgressTrackingInstruction> ProgressTracking { get; set; } = new List<ProgressTrackingInstruction>();
//        public List<string> Notes { get; set; } = new List<string>();
//    }

//    public class WeeklyPlan
//    {
//        public int WeekNumber { get; set; }
//        public List<DailyPlan> DailyPlans { get; set; } = new List<DailyPlan>();
//    }

//    public class DailyPlan
//    {
//        public string Day { get; set; }
//        public List<Meal> Meals { get; set; } = new List<Meal>();
//    }

//    public class Meal
//    {
//        public string Name { get; set; }
//        public List<FoodItem> Items { get; set; } = new List<FoodItem>();
//    }

//    public class FoodItem
//    {
//        public string Name { get; set; }
//        public string PortionSize { get; set; }
//        public int Calories { get; set; }
//        public Macronutrients Macronutrients { get; set; } = new Macronutrients();
//        public List<FoodItem> Alternatives { get; set; } = new List<FoodItem>();
//    }

//    public class Macronutrients
//    {
//        public string Carbs { get; set; }
//        public string Protein { get; set; }
//        public string Fat { get; set; }
//    }

//    public class ShoppingListItem
//    {
//        public string Item { get; set; }
//        public string Quantity { get; set; }
//    }

//    public class HydrationInstruction
//    {
//        public string Time { get; set; }
//        public string Instruction { get; set; }
//    }

//    public class ProgressTrackingInstruction
//    {
//        public string Instruction { get; set; }
//    }
//}































//public class StructuredDietPlan
//{
//    public List<Meal> Meals { get; set; }
//    public List<string> Notes { get; set; } // Changed to a list
//}

//public class Meal
//{
//    public string Name { get; set; } // E.g., Breakfast, Snack, Lunch, etc.
//    public List<string> Items { get; set; } // List of food items
//}