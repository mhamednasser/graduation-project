namespace graduationProject.Models
{
    public class nExercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sets { get; set; }

        public string? Icon { get; set; }
        public string VideoUrl { get; set; }
        public int MuscleGroupId { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
    }
}
