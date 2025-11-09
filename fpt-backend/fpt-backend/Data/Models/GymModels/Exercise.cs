namespace fpt_backend.Data.Models.GymModels;

public class Exercise : BaseModel
{
    public int ExerciseId { get; set; }
    public required string ExerciseName { get; set; }
    public string? ExerciseDescription { get; set; }
    public bool GloballyVisible { get; set; }
    
    public List<Equipment>? Equipment { get; set; } = new();
    public List<ExerciseSession>? ExerciseSessions { get; set; } = new();
    public List<Muscle>? Muscles { get; set; } = new();
    
}