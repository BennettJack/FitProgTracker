namespace fpt_backend.Data.Models.GymModels;

public class ExerciseType : BaseModel
{
    public string ExerciseTypeName { get; set; }

    public List<int> SetBlocId { get; set; } = new();
    public List<SetBloc> SetBlocs { get; set; } = new();
}
