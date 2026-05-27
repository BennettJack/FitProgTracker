namespace fpt_backend.Data.DTO.GymDTOs.ReturnDtos;

public class SetBlocReturnDto : BaseReturnDto
{
    public string Name { get; set; }
    public int DisplayOrder { get; set; }
    public int ExerciseId { get; set; }
    public int ExerciseTypeId { get; set; }
    public List<SetReturnDto> Sets { get; set; }
}
