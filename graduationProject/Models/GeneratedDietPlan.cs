namespace graduationProject.Models
{
    public class GeneratedDietPlan
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Link to the user who generated the plan
                                        // public string DietCategory { get; set; } // E.g., Balanced, Mediterranean, Keto, etc.
        public string MealPlan { get; set; } // AI-generated meal plan description
                                             // public string Notes { get; set; } // Additional instructions or notes
        public DateTime CreatedAt { get; set; } // When the plan was generated

        // Navigation property (optional, to reference the related User)
        public User User { get; set; }

    }

}