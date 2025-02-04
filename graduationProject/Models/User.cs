using graduationProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;




public enum Gender
{
    Male,
    Female,
    Other
}

public enum FitnessLevel
{
    Beginner,
    Intermediate,
    Advanced
}

public enum Goal
{
    GainingMuscle,
    BurningFat,
    ImprovingHealth,
    ReducingStress
}

public enum PreferredDiet
{
    Balanced,
    Mediterranean,
    LowCarb,
    Dash,
    Keto
}

public class User
{
    public int Id { get; set; }

    //[Required]
    //[StringLength(100)]
    //public string Username { get; set; } // sameeee appuser

    [Range(15, 60)]
    public int Age { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [Range(0.5, 3.0)]
    public double Height { get; set; }

    [Range(30, 150)]
    public double Weight { get; set; }

    [Required]
    public FitnessLevel FitnessLevel { get; set; }

    [Range(1, 7)]
    public int WeeklyWorkoutDays { get; set; }

    [Required]
    [StringLength(50)]
    public string WorkoutDuration { get; set; }

    [Required]
    public Goal Goal { get; set; }

    public string DietaryRestrictions { get; set; }

    [Required]
    public PreferredDiet PreferredDiet { get; set; }

    public string MedicalConditions { get; set; }

    //public string AppUserName { get; set; }

    // Navigation properties for plans

    [JsonIgnore]
    public ICollection<StructuredDietPlan> StructuredDietPlans { get; set; } = new List<StructuredDietPlan>();
    [JsonIgnore]
    public ICollection<StructuredWorkoutPlan> StructuredWorkoutPlans { get; set; } = new List<StructuredWorkoutPlan>();
}











//public class User
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public int Age { get; set; }
//    public string Gender { get; set; }
//    public double Height { get; set; }
//    public double Weight { get; set; }
//    public string FitnessLevel { get; set; }
//    public int WeeklyWorkoutDays { get; set; }
//    public string WorkoutDuration { get; set; }
//    public string Goal { get; set; }
//    public string DietaryRestrictions { get; set; }
//    public string PreferredDiet { get; set; }
//    public string MedicalConditions { get; set; }
//}


