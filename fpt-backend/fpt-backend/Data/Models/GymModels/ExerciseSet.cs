namespace fpt_backend.Data.Models.GymModels;

public class ExerciseSet : BaseModel
{
    public required int RepFloor { get; set; }
    public required int RepCeiling { get; set; }
    public required string Name { get; set; }

    public Exercise Exercise { get; set; }
    public List<ExerciseSession> ExerciseSessions { get; set; }
    public List<ExerciseSetRecord> ExerciseSetRecords { get; set; }
}