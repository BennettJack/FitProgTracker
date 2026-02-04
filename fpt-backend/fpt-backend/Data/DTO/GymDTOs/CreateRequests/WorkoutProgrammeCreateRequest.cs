using fpt_backend.Data.Models.GymModels;

namespace fpt_backend.Data.DTO.GymDTOs.CreateRequests;

public class WorkoutProgrammeCreateRequest
{
    public string Name { get; set; }
    public List<ExerciseSessionCreateRequest> WorkoutSessions { get; set; }
}