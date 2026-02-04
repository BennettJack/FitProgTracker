namespace fpt_backend.Data.DTO.GymDTOs.CreateRequests;

public class ExerciseSessionCreateRequest
{
    public string Name { get; set; }
    public List<ExerciseSetBlocCreateRequest> ExerciseSetBlocs { get; set; }
}