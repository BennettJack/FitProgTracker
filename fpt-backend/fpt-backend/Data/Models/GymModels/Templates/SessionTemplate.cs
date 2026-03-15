

namespace fpt_backend.Data.Models.GymModels.Instances;

public class SessionTemplate : BaseModel
{
    public string Name { get; set; }
    public int DisplayOrder { get; set; }
    
    public WorkoutProgrammeTemplate WorkoutProgrammeTemplate { get; set; }
    public int WorkoutProgrammeTemplateId { get; set; }
    public List<SetBlocTemplate> SetBlocTemplates { get; set; }
}