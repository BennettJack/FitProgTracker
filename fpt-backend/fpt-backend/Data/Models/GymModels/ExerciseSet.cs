using fpt_backend.DbRepositories.GymRepositories.Interfaces;

namespace fpt_backend.Data.Models.GymModels;

public class ExerciseSet : BaseModel
{
    public int ExerciseSetId { get; set; }
    public required int RepFloor { get; set; }
    public required int RepCeiling { get; set; }
    public required string Name { get; set; }

    public Exercise Exercise { get; set; }
    public ExerciseSession? ExerciseSession { get; set; }
    public List<ExerciseSetRecord>? ExerciseSetRecords { get; set; }
}