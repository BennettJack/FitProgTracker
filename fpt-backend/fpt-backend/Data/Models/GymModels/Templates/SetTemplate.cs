namespace fpt_backend.Data.Models.GymModels.Instances;

public class SetTemplate : BaseModel
{
    public int RepFloor { get; set; }
    public int RepCeiling { get; set; }
    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public int SetBlocTemplateId { get; set; }
    public SetBlocTemplate SetBlocTemplate { get; set; }
}