using fpt_backend.Data.Models.GymModels;

namespace fpt_backend.Data.DTO.GymDTOs.ReturnDtos;

public class WildcardSetReturnDto
{
    public int ExerciseId { get; set; }
    public int ExerciseTypeId { get; set; }
    public List<ExerciseSetRecord> Records { get; set; } = new();
}
