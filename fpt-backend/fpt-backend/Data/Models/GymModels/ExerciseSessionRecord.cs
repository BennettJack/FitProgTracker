namespace fpt_backend.Data.Models.GymModels;

public class ExerciseSessionRecord : BaseModel
{
    public ExerciseSession ExerciseSession { get; set; }
    public List<ExerciseSetRecord> ExerciseSetRecords { get; set; }
}