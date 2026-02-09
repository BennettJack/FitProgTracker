using fpt_backend.Data.Models.GymModels.Instances;

namespace fpt_backend.Data.Models.GymModels;

public class WorkoutProgramme : BaseModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public int? WorkoutProgrammeTemplateID { get; set; }
    public WorkoutProgrammeTemplate? WorkoutProgrammeTemplate { get; set; }
    
   
    public List<Session> Sessions { get; set; } = new();
}