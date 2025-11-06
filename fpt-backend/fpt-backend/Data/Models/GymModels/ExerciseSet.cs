using fpt_backend.DbRepositories.GymRepositories.Interfaces;

namespace fpt_backend.Data.Models.GymModels;

public class ExerciseSet : BaseModel
{
    public required int ExerciseSetId { get; set; }
    public required int RepFloor { get; set; }
    public required int RepCeiling { get; set; }
    public string Name { get; set; }

    public Exercise Exercise { get; set; }
    public ExerciseSession ExerciseSession { get; set; }
    public List<ExerciseSetRecord> ExerciseSetRecords { get; set; }
}