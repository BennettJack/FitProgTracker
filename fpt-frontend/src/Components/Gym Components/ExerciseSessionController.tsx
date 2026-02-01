import {ExerciseSessionControllerProps} from "../../Types/WorkoutTypes";


export function ExerciseSessionController({exerciseSession, mode} : ExerciseSessionControllerProps){
    
    return(
        <>
            <p>{exerciseSession.name}</p>
        </>
    )
}