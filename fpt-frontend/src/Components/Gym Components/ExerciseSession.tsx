import React, {useState, useEffect} from 'react';
import {ExerciseProps, SessionProps} from "../../Pages/Gym/AddNewWorkoutPlan/AddNewWorkoutProgram";
import {ExerciseSet} from "./NewProgram/Sets/ExerciseSet";



export type ExerciseSessionProps = {
    edit: true,
    sessionProps: SessionProps
    updateExercise:(sessionKey: number, changes: SessionProps) => void,
}



export function ExerciseSession({edit, sessionProps, updateExercise} : ExerciseSessionProps): React.ReactElement {
    
    const [formData, setFormData] = useState(sessionProps);
    
    function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        
        updateExercise(sessionProps.sessionKey, formData);
    }
    
    function handleChange(e:  React.ChangeEvent<HTMLFormElement> ) {
        setFormData((prevState) =>{
            return{
                ...prevState,
                [e.target.name]: e.target.value
            }
        })
    }
    
    function addExercise(){
        
        const tempExercises: ExerciseProps= {
            exerciseId: String(formData.exercises.length + 1),
            order: formData.exercises.length +1,
            sets: []
            
        }
        setFormData(formData => ({ ...formData, exercises: [...formData.exercises, tempExercises]}))
    }
    
    return(
        <>
            {edit &&
                <div>
                    <form key={sessionProps.sessionKey} onSubmit={handleSubmit} onChange={handleChange}>
                        <label htmlFor={"sessionName"}>Session Name: </label>
                        <input value={formData.sessionName} name={"sessionName"} type={"text"}/>
                        <button type={"submit"}>Submit Changes</button>
                    </form>

                    <button type={"button"} onClick={addExercise}> Add an exercise</button>

                    {formData.exercises && formData.exercises.length > 0 &&
                        formData.exercises.map(exercise => (
                            <div>
                                <p key={exercise.exerciseId}>An exercise!</p>      
                            </div>
                        ))}
                </div>
            }
        </>
    )
}