namespace fpt_backend.Data.DTO.GymDTOs.CreateRequests;

public class ExerciseSetCreateRequest : BaseCreateRequest
{
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public int RepCeiling { get; set; }
    public int RepFloor { get; set; }
}