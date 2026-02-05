using fpt_backend.Data.DTO.GymDTOs.ReturnDtos;
using fpt_backend.Data.DTO.UserDTOs.ExerciseDtos;

namespace fpt_backend.Data.Models.GymModels.Dto;

public class WorkoutProgrammeReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<SessionReturnDto> Sessions { get; set; }
}