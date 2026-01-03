using fpt_backend.Data.Models.GymModels;

namespace fpt_backend.Data.Models.JoiningModels;

public class ExerciseSessionExerciseSetBloc
{
    public int ExerciseSessionId { get; set; }
    public ExerciseSession ExerciseSession { get; set; }
    
    public int ExerciseSetBlocId { get; set; }
    public ExerciseSetBloc ExerciseSetBloc { get; set; }

    public int Order { get; set; }
}