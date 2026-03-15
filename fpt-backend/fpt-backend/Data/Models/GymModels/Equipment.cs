namespace fpt_backend.Data.Models.GymModels;

public class Equipment : BaseModel
{
    public string Name { get; set; }

    public List<Exercise> Exercises { get; set; } = new();
}