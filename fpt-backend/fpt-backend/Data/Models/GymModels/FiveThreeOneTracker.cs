namespace fpt_backend.Data.Models.GymModels;

public class FiveThreeOneTracker : BaseModel
{
    public int OverheadPressCycle { get; set; }
    public int BarbellSquatCycle { get; set; }
    public int BenchPressCycle { get; set; }
    public int DeadliftCycle { get; set; }
    
    public required WorkoutProgramme WorkoutProgramme { get; set; }
    public int WorkoutProgrammeId { get; set; }
}