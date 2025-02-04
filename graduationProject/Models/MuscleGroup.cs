namespace graduationProject.Models
{
    public class MuscleGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<nExercise> nExercises { get; set; }
    }
}