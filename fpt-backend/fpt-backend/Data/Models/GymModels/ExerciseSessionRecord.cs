namespace fpt_backend.Data.Models.GymModels;

public class ExerciseSessionRecord : BaseModel
{
    public required int ExerciseSessionRecordId { get; set; }
    public string ExerciseSetName { get; set; }
    
    public List<ExerciseSetRecord> ExerciseSetRecords { get; set; }
}