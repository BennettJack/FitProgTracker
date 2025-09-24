using fpt_backend.Data.Models.Gym;

namespace fpt_backend.Data.Models.GymModels;

public class Exercise
{
    public int ExerciseId { get; set; }
    public required string ExerciseName { get; set; }
    public required Equipment Equipment { get; set; }
    public required List<Muscle> Muscles { get; set; }
    public string? ExerciseDescription { get; set; } 
}