using fpt_backend.Data.Models.GymModels.Instances;

namespace fpt_backend.Data.Models.GymModels.JoiningModels;

public class SessionTemplateSetBlocTemplate : BaseModel
{
    public int SessionTemplateId { get; set; }
    public SessionTemplate SessionTemplate { get; set; }
    
    public int SetBlocTemplateId { get; set; }
    public ExerciseSetBlocTemplate SetBlocTemplate { get; set; }
    
    public int DisplayOrder { get; set; }
}