using fpt_backend.Data.Models.GymModels.Instances;

namespace fpt_backend.Data.Models.GymModels;

public class Set : BaseModel
{
    public int SetBlocId { get; set; }
    public SetBloc SetBloc { get; set; }

    public int DisplayOrder { get; set; }
    public int RepFloor { get; set; }
    public int RepCeiling { get; set; }
    public string? Description { get; set; }
    
    public int SetTemplateId { get; set; }
    public SetTemplate SetTemplate { get; set; }

    public List<ExerciseSetRecord> ExerciseSetRecords { get; set; } = new();
}