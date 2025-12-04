namespace fpt_backend.Data.DTO.GymDTOs;

public class ExerciseSetCreationDto
{
    public required string Name { get; set; }
    public required int RepCeiling { get; set; }
    public required int RepFloor { get; set; }
    public required int ExerciseId { get; set; }
    public int ExerciseSessionId { get; set; }
}