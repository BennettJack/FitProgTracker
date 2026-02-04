using fpt_backend.Data.Models.GymModels.JoiningModels;

namespace fpt_backend.Data.Models.GymModels.Instances;

public class SessionTemplate : BaseModel
{
    public string Name { get; set; }

    public List<WorkoutProgrammeTemplateSessionTemplate> WorkoutProgrammeTemplates { get; set; } = new();
    public List<SessionTemplateSetBlocTemplate> SetBlocs { get; set; } = new();
}