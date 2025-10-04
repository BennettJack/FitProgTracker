import React, {useState, useEffect} from 'react';
import {SessionProps} from "../../Pages/Gym/AddNewWorkoutPlan/AddNewWorkoutProgram";



export type ExerciseSessionProps = {
    edit: true,
    onChange:(sessionKey: number, changes: Partial<ExerciseSessionProps>) => void,
} & (SessionProps)



export function ExerciseSession({sessionKey, edit, sessionName, onChange} : ExerciseSessionProps): React.ReactElement {

    function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
        switch (e.target.name) {
            case "sessionName":
                onChange(sessionKey, {sessionName: e.target.value});
                break;
        }
    }
    
    
    return(
        <>
            {edit &&
                <div>
                    <label htmlFor={"sessionName"}>Program Name: </label>
                    <input value={sessionName} name={"sessionName"} type={"text"} onChange={(e) => handleChange(e)}/>
                </div>
            }
        </>
    )
}