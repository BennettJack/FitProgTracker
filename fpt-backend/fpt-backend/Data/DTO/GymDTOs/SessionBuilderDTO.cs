using fpt_backend.Data.Models.GymModels;

namespace fpt_backend.Data.DTO.GymDTOs;

public class SessionBuilderDTO
{
    public int SessionId { get; set; }
    public string SessionName { get; set; }
    
    public List<ExerciseSetBloc> ExerciseSetBlocs { get; set; }
}