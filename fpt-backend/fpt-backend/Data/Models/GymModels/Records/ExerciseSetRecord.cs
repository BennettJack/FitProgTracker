namespace fpt_backend.Data.Models.GymModels;

public class ExerciseSetRecord : BaseModel
{
    public required int ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public required int ExerciseTypeId { get; set; }
    public ExerciseType ExerciseType { get; set; } = null!;
    public required decimal RepsCompleted { get; set; }
    public required decimal Weight { get; set; }
    public int? PerceivedEffort { get; set; }

    public int? SetId { get; set; }
    public Set? Set { get; set; }
}
