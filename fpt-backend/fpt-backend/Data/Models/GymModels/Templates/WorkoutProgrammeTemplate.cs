

namespace fpt_backend.Data.Models.GymModels.Instances;

public class WorkoutProgrammeTemplate : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<SessionTemplate> SessionTemplates { get; set; } = new();
}