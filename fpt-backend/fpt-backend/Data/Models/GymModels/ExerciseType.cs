namespace fpt_backend.Data.Models.GymModels;

public class ExerciseType : BaseModel
{
    public string ExerciseTypeName { get; set; }
    
    public List<int> ExerciseIds { get; set; }
    public List<Exercise> Exercises { get; set; }
}