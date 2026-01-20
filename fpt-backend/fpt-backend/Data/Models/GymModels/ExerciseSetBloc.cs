namespace fpt_backend.Data.Models.GymModels;

public class ExerciseSetBloc : BaseModel
{
    public string ExerciseSetBlocName { get; set; }
    public List<ExerciseSet> ExerciseSets { get; set; }
}