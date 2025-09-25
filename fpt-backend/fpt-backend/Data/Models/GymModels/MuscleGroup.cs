namespace fpt_backend.Data.Models.GymModels;

public class MuscleGroup : BaseModel
{
    public int MuscleGroupId { get; set; }
    public string MuscleGroupName { get; set; }
    
    public List<Muscle> Muscles { get; set; } = new();
}