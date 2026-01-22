namespace fpt_backend.Data.Models.GymModels;

public class Equipment : BaseModel
{
    public required string EquipmentName { get; set; }
    
    public List<Exercise> Exercises { get; set; }
}