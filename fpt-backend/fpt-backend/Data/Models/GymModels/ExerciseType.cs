namespace fpt_backend.Data.Models.GymModels;

public class ExerciseType : BaseModel
{
    public string ExerciseTypeName { get; set; }
    public List<Set> Sets { get; set; } = new();
}
