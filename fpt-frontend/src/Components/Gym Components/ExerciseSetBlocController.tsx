import React from "react";
import {ExerciseSession, ExerciseSet, ExerciseSetBloc, ExerciseSetBlocControllerProps} from "../../Types/WorkoutTypes";
import {v4 as uuidv4} from "uuid";


export function ExerciseSetBlocController(
    {
        exerciseSetBloc, 
        updateSession, 
        mode
    } 
    :ExerciseSetBlocControllerProps): React.ReactElement {

    const updateBloc = (
        updater: (prev: ExerciseSetBloc) => ExerciseSetBloc
    ) => {
        updateSession?.(prevSession => ({
            ...prevSession,
            exerciseSets: prevSession.exerciseSetBlocs.map(bloc =>
                (bloc.id ?? bloc.tempId) === (bloc.id ?? bloc.tempId)
                    ? updater(bloc)
                    : bloc
            ),
        }));
    };

    const addSet = () =>{
        let setCount : number = exerciseSetBloc.exerciseSets.length
        setCount += 1
        const newSet: ExerciseSet = {
            tempId: uuidv4(),
            name: "set" + setCount,
            description: "",
            repCeiling: 0,
            repFloor: 0,
            
        }
        updateBloc(prev => ({
            ...prev,
            exerciseSets: [...prev.exerciseSets, newSet]
        }))
    }
    
    return (
        <>
            {exerciseSetBloc.exerciseSets.map ((set, index) =>
                <p>{set.name}</p>
            )}
            {mode === "create" && (
                <div>
                    <p>Add a set to this exercise</p>
                    <button onClick={addSet}>Add a set</button>
                </div>
            )}
        </>
    )
}