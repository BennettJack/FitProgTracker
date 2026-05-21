using fpt_backend.Data.Constants.GymConstants;

namespace fpt_backend.Data.Models.GymModels;

public class FiveThreeOneTracker : BaseModel
{
    public MaxTypes MaxType { get; set; }
    public int OverheadPressCycle { get; set; }
    public int BarbellSquatCycle { get; set; }
    public int BenchPressCycle { get; set; }
    public int DeadliftCycle { get; set; }

    public int OverheadPressTrainingMax { get; set; }
    public int BarbellSquatTrainingMax { get; set; }
    public int BenchPressTrainingMax { get; set; }
    public int DeadliftTrainingMax { get; set; }
    public string UserId { get; set; }
}
