using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

using System.Text.Json.Serialization;

namespace graduationProject.Models
{
    public class StructuredWorkoutPlan
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
        public WorkoutPlanData PlanData
        {
            get => JsonSerializer.Deserialize<WorkoutPlanData>(PlanJson);
            set => PlanJson = JsonSerializer.Serialize(value);
        }
    }

    public class WorkoutPlanData
    {
        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("goal")]
        public string Goal { get; set; }

        [JsonPropertyName("daily_plans")]
        public List<DailyWorkoutPlan> DailyPlans { get; set; } = new List<DailyWorkoutPlan>();

        [JsonPropertyName("rest_days")]
        public List<string> RestDays { get; set; } = new List<string>();

        [JsonPropertyName("notes")]
        public List<string> Notes { get; set; } = new List<string>();
    }

    public class DailyWorkoutPlan
    {
        [JsonPropertyName("day")]
        public string Day { get; set; }

        [JsonPropertyName("focus")]
        public string Focus { get; set; }

        [JsonPropertyName("exercises")]
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
    }

    public class Exercise
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("muscle_group")]
        public string MuscleGroup { get; set; }

        [JsonPropertyName("sets")]
        public int Sets { get; set; }

        [JsonPropertyName("reps")]
        public string Reps { get; set; }

        [JsonPropertyName("rest_between_sets")]
        public string RestBetweenSets { get; set; }

        [JsonPropertyName("intensity")]
        public string Intensity { get; set; }

        [JsonPropertyName("notes")]
        public List<string> Notes { get; set; } = new List<string>();
    }
}