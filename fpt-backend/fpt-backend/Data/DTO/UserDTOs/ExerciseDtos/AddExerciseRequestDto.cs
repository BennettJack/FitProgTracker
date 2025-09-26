namespace fpt_backend.Data.DTO.UserDTOs.ExerciseDtos;

public class AddExerciseRequestDto
{
    public required string ExerciseName { get; set; }
    public required List<int> MuscleIds { get; set; }
    public required List<int> EquipmentIds { get; set; }
    public string? Description { get; set; }
}