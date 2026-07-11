using fpt_backend.Data.Models.GymModels;

namespace fpt_backend.Data.DTO.GymDTOs.ReturnDtos;

public class TodayRecordsReturnDto
{
    public Dictionary<int, ExerciseSetRecord> RecordsBySetId { get; set; } = new();

    public List<WildcardSetReturnDto> WildcardSets { get; set; } = new();
}
