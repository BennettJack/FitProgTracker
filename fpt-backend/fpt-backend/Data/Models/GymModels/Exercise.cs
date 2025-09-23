namespace fpt_backend.Data.Models.Gym;

public class Exercise
{
    public int ExerciseId { get; set; }
    public required string ExerciseName { get; set; }
    public required Equipment Equipment { get; set; }
    public string? ExerciseDescription { get; set; } 
}