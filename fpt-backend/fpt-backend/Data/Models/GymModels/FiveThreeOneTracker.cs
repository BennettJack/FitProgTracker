namespace fpt_backend.Data.Models.GymModels;

public class FiveThreeOneTracker : BaseModel
{
    public int OverheadPressCycle { get; set; }
    public int BarbellSquatCycle { get; set; }
    public int BenchPressCycle { get; set; }
    public int DeadliftCycle { get; set; }
    
    public int OverheadPressTrainingMax { get; set; }
    public int BarbellSquatTrainingMax { get; set; }
    public int BenchPressTrainingMax { get; set; }
    public int DeadliftTrainingMax { get; set; }
    public required WorkoutProgramme WorkoutProgramme { get; set; }
    public int WorkoutProgrammeId { get; set; }
}