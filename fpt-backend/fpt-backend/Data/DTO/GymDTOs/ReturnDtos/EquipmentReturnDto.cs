using fpt_backend.Data.DTO.GymDTOs.ReturnDtos;

namespace fpt_backend.Data.DTO.GymDTOs.ResponseDtos;

public class EquipmentReturnDto : BaseReturnDto
{
    public required string EquipmentName { get; init; }
}