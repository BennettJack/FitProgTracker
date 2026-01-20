namespace fpt_backend.Data.Models.GymModels;

public class WorkoutProgram : BaseModel
{
    public string WorkoutProgramName { get; set; }
    public string WorkoutProgramDescription { get; set; }
    
    public List<ExerciseSession> ExerciseSessions { get; set; } = new();
    public List<string> HasAccessToProgram { get; set; } = new();
    
    
}