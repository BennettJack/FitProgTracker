import {ExerciseSetControllerProps} from "../../Types/WorkoutTypes";


export function ExerciseSetController(
    {
        exerciseSet,
        updateExerciseSetBloc,
        mode
    } : ExerciseSetControllerProps): React.ReactElement{
    
    
    
    return (
        <p>{exerciseSet.name}</p>
    )
}