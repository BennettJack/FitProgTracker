namespace fpt_backend.Data.DTO.GymDTOs.ReturnDtos;

public class SessionReturnDto : BaseReturnDto
{
    public string Name { get; set; }
    public int DisplayOrder { get; set; }
    public List<SetBlocReturnDto> SetBlocs { get; set; }
}