import React from "react";
import {ExerciseSession, ExerciseSet, ExerciseSetBloc, ExerciseSetBlocControllerProps} from "../../Types/WorkoutTypes";
import {v4 as uuidv4} from "uuid";
import {ExerciseSetController} from "./ExerciseSetController";


export function ExerciseSetBlocController(
    {
        exerciseSetBloc,
        updateSession,
        mode,
    }: ExerciseSetBlocControllerProps): React.ReactElement {

    const updateBloc = (
        updater: (prev: ExerciseSetBloc) => ExerciseSetBloc
    ) => {
        updateSession?.(prevSession => ({
            ...prevSession,
            exerciseSetBlocs: prevSession.exerciseSetBlocs.map(bloc =>
                (bloc.id ?? bloc.tempId) ===
                (exerciseSetBloc.id ?? exerciseSetBloc.tempId)
                    ? updater(bloc)
                    : bloc
            ),
        }));
    };

    const addSet = () => {
        const setCount = exerciseSetBloc.exerciseSets.length + 1;

        const newSet: ExerciseSet = {
            tempId: uuidv4(),
            name: `set ${setCount}`,
            description: "",
            repCeiling: String(0),
            repFloor: String(0),
        };

        updateBloc(prev => ({
            ...prev,
            exerciseSets: [...prev.exerciseSets, newSet],
        }));
    };
    
    const removeExerciseSet = (id : number | string | undefined) => {
        updateBloc?.(prev => ({
            ...prev,
            exerciseSets: exerciseSetBloc.exerciseSets.filter(set =>
                (set.id ?? set.tempId) !== id
            ),
        }));
    }
    
    return (
        <>
            <p>{exerciseSetBloc.name}</p>
            {exerciseSetBloc.exerciseSets.map(set => (
                <div key={set.id ?? set.tempId}>
                    <ExerciseSetController
                        exerciseSet={set}
                        updateExerciseSetBloc={updateBloc}
                        removeExerciseSet={() => removeExerciseSet(set.id ?? set.tempId)}
                        mode={mode}
                    />
                </div>
            ))}

            {mode === "create" && (
                <div>
                    <p>Add a set to this exercise</p>
                    <button onClick={addSet}>Add a set</button>
                </div>
            )}
        </>
    );
}