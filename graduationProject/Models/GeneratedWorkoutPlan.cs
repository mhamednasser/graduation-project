namespace graduationProject.Models
{
    public class GeneratedWorkoutPlan
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Link to the user who generated the plan
                                        // public string WorkoutPlanName { get; set; } // Name of the generated workout plan
        public string PlanDetails { get; set; } // AI-generated workout details
                                                // public string Notes { get; set; } // Additional instructions or notes
        public DateTime CreatedAt { get; set; } // When the plan was generated

        // Navigation property (optional, to reference the related User)
        public User User { get; set; }
    }

}