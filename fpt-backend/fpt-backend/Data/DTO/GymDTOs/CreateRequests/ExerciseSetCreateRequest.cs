namespace fpt_backend.Data.DTO.GymDTOs.CreateRequests;

public class ExerciseSetCreateRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string RepCeiling { get; set; }
    public string RepFloor { get; set; }
}