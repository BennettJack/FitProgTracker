namespace fpt_backend.Data.DTO.GymDTOs.CreateRequests;

public class ExerciseSetRecordCreateRequest
{
    public int ExerciseId { get; set; }
    public int ExerciseTypeId { get; set; }
    public int Weight { get; set; }
    public int RepsCompleted { get; set; }

    public int? ExerciseSetId { get; set; }
}
