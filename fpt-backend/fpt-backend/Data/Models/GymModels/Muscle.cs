namespace fpt_backend.Data.Models.GymModels;

public class Muscle : BaseModel
{
    public required string MuscleName { get; set; }
    
    public List<Exercise> Exercises { get; set; } = new();
    public MuscleGroup MuscleGroup { get; set; } = null!;
}