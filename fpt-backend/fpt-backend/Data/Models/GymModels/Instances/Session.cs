using fpt_backend.Data.Models.GymModels.Instances;

namespace fpt_backend.Data.Models.GymModels;

public class Session : BaseModel
{
    public int WorkoutProgrammeId { get; set; }
    public WorkoutProgramme WorkoutProgramme { get; set; }

    public int? SessionTemplateId { get; set; }
    public SessionTemplate? SessionTemplate { get; set; }

    public string Name { get; set; }
    public int DisplayOrder { get; set; }
    
    public List<SetBloc> SetBlocs { get; set; } = new();
}