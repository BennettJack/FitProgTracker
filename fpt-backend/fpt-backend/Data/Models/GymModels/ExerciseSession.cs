namespace fpt_backend.Data.Models.GymModels;

public class ExerciseSession : BaseModel
{
    public int ExerciseSessionId { get; set; }
    public required string SessionName { get; set; }
    
    public List<Exercise> Exercises { get; set; } = new();
}