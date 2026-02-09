import {Set, ExerciseSetControllerProps} from "../../Types/WorkoutTypes";
import React from "react";


export function ExerciseSetController(
    {
        exerciseSet,
        updateExerciseSetBloc,
        removeExerciseSet,
        mode
    } : ExerciseSetControllerProps): React.ReactElement{
    
    const updateExerciseSet = (
        updater:(prev: Set) => Set) => {
        updateExerciseSetBloc?.(prevExerciseSetBloc => ({
            ...prevExerciseSetBloc,
            sets: prevExerciseSetBloc.sets.map(set =>
                (set.id ?? set.tempId) ===
                (exerciseSet.id ?? exerciseSet.tempId)
                    ? updater(set)
                    : set
            ),
        }))
    }

    const updateSetValues = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;

        updateExerciseSet(prev => ({
            ...prev,
            [name]:
                name === "repFloor" || name === "repCeiling"
                    ? Number(value)
                    : value,
        }));
    };
    
    const removeSet = () => {
        
    }
    
    return(
        <div>
            <label htmlFor={"repFloor"}>
                Rep floor
            </label>
            <input
                name={"repFloor"}
                onChange={updateSetValues}
                value={exerciseSet.repFloor}
                type="number"
            />
            <label htmlFor={"repCeiling"}>
                Rep Ceiling
            </label>
            <input 
                name={"repCeiling"} 
                onChange={updateSetValues} 
                value={exerciseSet.repCeiling} 
                type="number" 
            />
            <label htmlFor={"description"}>
                Description
            </label>
            <input
                name={"description"}
                onChange={updateSetValues}
                value={exerciseSet.description}
                type="text"
            />
            <button
                onClick={removeExerciseSet}
                > Remove Set</button>
        </div>
    )
}