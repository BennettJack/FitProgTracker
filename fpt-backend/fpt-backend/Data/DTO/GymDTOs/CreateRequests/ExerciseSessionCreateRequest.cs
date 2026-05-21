using fpt_backend.Data.Constants.GymConstants;

namespace fpt_backend.Data.DTO.GymDTOs.CreateRequests;

public class ExerciseSessionCreateRequest : BaseCreateRequest
{
    public string Name { get; set; }
    public int DisplayOrder { get; set; }
    public List<ExerciseSetBlocCreateRequest> SetBlocs { get; set; }
}