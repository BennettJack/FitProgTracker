namespace fpt_backend.Data.DTO.GymDTOs.ReturnDtos;

public class SetBlocReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DisplayOrder { get; set; }
    public List<SetReturnDto> Sets { get; set; }
}