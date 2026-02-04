namespace fpt_backend.Data.Models.GymModels;

public class ExerciseSetRecord : BaseModel
{
    public int SetId { get; set; }
    public Set Set { get; set; }
    
    public required decimal RepsCompleted { get; set; }
    public required decimal Weight { get; set; }
}