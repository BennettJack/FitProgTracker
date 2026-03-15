namespace fpt_backend.Data.Models.GymModels;

public class Muscle : BaseModel
{
    public required string MuscleName { get; set; }
    
    public List<Exercise> Exercises { get; set; }
    
    public int MuscleGroupId { get; set; }
    public MuscleGroup MuscleGroup { get; set; }
}