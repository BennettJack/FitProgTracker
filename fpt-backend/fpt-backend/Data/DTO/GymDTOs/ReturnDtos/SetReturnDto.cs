namespace fpt_backend.Data.DTO.GymDTOs.ReturnDtos;

public class SetReturnDto : BaseReturnDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int DisplayOrder { get; set; }
    public int RepFloor { get; set; }
    public int RepCeiling { get; set; }
    public int ExerciseId { get; set; }
    public int ExerciseTypeId { get; set; }
}
