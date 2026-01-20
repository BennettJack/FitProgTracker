namespace fpt_backend.Data.Models.GymModels;

public class ExerciseSessionRecord : BaseModel
{
    public string ExerciseSetName { get; set; }
    
    public List<ExerciseSetRecord> ExerciseSetRecords { get; set; }
}