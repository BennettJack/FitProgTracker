namespace fpt_backend.Data.DTO.GymDTOs.ReturnDtos;

public class SessionReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DisplayOrder { get; set; }
    public List<SetBlocReturnDto> SetBlocs { get; set; }
}