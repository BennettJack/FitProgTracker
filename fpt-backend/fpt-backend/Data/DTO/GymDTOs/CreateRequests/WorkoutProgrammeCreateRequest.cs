using fpt_backend.Data.Models.GymModels;

namespace fpt_backend.Data.DTO.GymDTOs.CreateRequests;

public class WorkoutProgrammeCreateRequest : BaseCreateRequest
{
    public string? Description { get; set; }
    public string Name { get; set; }
    public List<ExerciseSessionCreateRequest> Sessions { get; set; }
}