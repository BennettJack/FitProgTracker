

namespace fpt_backend.Data.Models.GymModels.Instances;

public class SetBlocTemplate : BaseModel
{
    public string Name { get; set; }
    public int DisplayOrder { get; set; }

    public int ExerciseId { get; set; }
    public Exercise Exercise { get; set; }

    public int SessionTemplateId { get; set; }
    public SessionTemplate SessionTemplate { get; set; }

    public List<SetTemplate> SetTemplates { get; set; } = new();
}