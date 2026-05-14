using fpt_backend.Data.Constants.GymConstants;

namespace fpt_backend.Data.DTO.GymDTOs.CreateRequests;

public class FiveThreeOneTrackerDto
{
    public MaxTypes MaxType { get; set; }
    public int OverheadPressWeight { get; set; }
    public int BarbellSquatWeight { get; set; }
    public int BenchPressWeight { get; set; }
    public int DeadliftWeight { get; set; }
}
