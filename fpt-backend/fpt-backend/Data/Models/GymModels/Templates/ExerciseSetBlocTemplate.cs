using fpt_backend.Data.Models.GymModels.JoiningModels;

namespace fpt_backend.Data.Models.GymModels.Instances;

public class ExerciseSetBlocTemplate : BaseModel
{
    public string Name { get; set; }

    public List<SessionTemplateSetBlocTemplate> SessionTemplates { get; set; } = new();
    public List<ExerciseSetTemplate> Sets { get; set; } = new();
}