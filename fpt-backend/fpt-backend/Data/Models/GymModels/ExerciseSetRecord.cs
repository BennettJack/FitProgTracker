namespace fpt_backend.Data.Models.GymModels;

public class ExerciseSetRecord : BaseModel
{
    public required int Reps { get; set; }
    public required int Weight { get; set; }
    
    public ExerciseSet ExerciseSet { get; set; }
    public ExerciseSessionRecord ExerciseSessionRecord { get; set; }
}