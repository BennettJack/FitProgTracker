namespace fpt_backend.Data.DTO.GymDTOs.CreateRequests;

public class ExerciseSetBlocCreateRequest
{
    public string Name {get; set;}
    public List<ExerciseSetCreateRequest> ExerciseSets { get; set; } = new();
}