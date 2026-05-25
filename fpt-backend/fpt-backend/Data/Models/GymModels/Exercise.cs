using fpt_backend.Data.Constants.GymConstants;
using fpt_backend.Data.Models.GymModels.Instances;

namespace fpt_backend.Data.Models.GymModels;

public class Exercise : BaseModel
{
    public string ExerciseName { get; set; }
    public string? ExerciseDescription { get; set; }
    public bool GloballyVisible { get; set; }
    public List<Equipment> Equipment { get; set; } = new();
    public List<Muscle> Muscles { get; set; } = new();
    public List<SetTemplate> SetTemplates { get; set; } = new();
}
