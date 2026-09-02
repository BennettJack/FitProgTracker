using fpt_backend.Data.Models.GymModels;

namespace fpt_backend.Data.DTO.GymDTOs.ReturnDtos;

public class RecordsReturnDto
{
    public Dictionary<int, ExerciseSetRecord> SetRecords { get; set; } = new();
}
