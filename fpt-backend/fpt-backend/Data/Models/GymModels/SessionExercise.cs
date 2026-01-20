namespace fpt_backend.Data.Models.GymModels;

public class SessionExercise : BaseModel
{
    public required Exercise Exercise { get; set; }
    public required int Sets { get; set; }
    public required int Reps { get; set; }
    public string? Tempo { get; set; }
    public string? Notes { get; set; }
    public required int Order { get; set; }
}