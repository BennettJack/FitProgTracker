using fpt_backend.Data.DTO.GeneralDTOs;

namespace fpt_backend.Data.DTO.GymDTOs.ReturnDtos;

public class ExerciseOptionData
{
    public List<DropdownReturnDto> EquipmentOptions { get; set; }
    public List<DropdownReturnDto> MuscleOptions { get; set; }
}
