﻿public class WorkoutPlan
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Exercise> Exercises { get; set; }
}
