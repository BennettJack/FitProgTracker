using fpt_backend.Data.Models.GymModels.Instances;

namespace fpt_backend.Data.Models.GymModels.JoiningModels;

public class WorkoutProgrammeTemplateSessionTemplate : BaseModel
{
    public int WorkoutProgrammeTemplateId { get; set; }
    public WorkoutProgrammeTemplate WorkoutProgrammeTemplate { get; set; }
    
    public int SessionTemplateId { get; set; }
    public SessionTemplate SessionTemplate { get; set; }
    
    public int Order { get; set; }
}