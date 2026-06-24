using fpt_backend.Data.Models.GymModels.Instances;

namespace fpt_backend.Data.Models.GymModels;

public class SetBloc : BaseModel
{
    public int SessionId { get; set; }
    public Session Session { get; set; }

    public int? SetBlocTemplateId { get; set; }
    public SetBlocTemplate? SetBlocTemplate { get; set; }

    public string Name { get; set; }
    public int DisplayOrder { get; set; }

    public List<Set> Sets { get; set; } = new();
}
