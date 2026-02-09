namespace fpt_backend.Data.DTO.GymDTOs.CreateRequests;

public class ExerciseSetBlocCreateRequest : BaseCreateRequest
{
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public string Name {get; set;}
    public List<ExerciseSetCreateRequest> Sets { get; set; }
}