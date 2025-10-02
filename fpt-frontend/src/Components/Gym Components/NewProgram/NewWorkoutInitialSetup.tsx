import React from 'react';
import {WorkoutProgramProps} from "../../../Pages/Gym/AddNewWorkoutPlan/AddNewWorkoutProgram";
import styles from './NewWorkoutInitialSetup.module.css'

export type WorkoutInitialSetupProps = {
    programName: string;
    programDescription: string;
    sessionCount: number;
    onChange: (changes: Partial<WorkoutProgramProps> ) => void;
    updateStage:(stage: number) => void;
}
export function NewWorkoutInitialSetup({programName, programDescription, sessionCount, onChange, updateStage}: WorkoutInitialSetupProps) : React.ReactElement {
    
    function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
        switch (e.target.name) {
            case "workoutProgramName":
                onChange({programName: e.target.value});
                break;
            case "workoutProgramDescription":
                onChange({programDescription: e.target.value});
                break;
            case "workoutSessionCount":
                onChange({sessionCount: Number(e.target.value)});
        }
    }
    
    return (
        <div>
            <h2>Setup</h2>
            <label htmlFor={"workoutProgramName"}>Program Name: </label>
            <input value={programName} name={"workoutProgramName"} type={"text"} onChange={(e) => handleChange(e)}/>
            
            <label htmlFor={"workoutProgramDescription"}>Program Description: </label>
            <input value={programDescription} name={"workoutProgramDescription"} type={"text"} onChange={(e) => handleChange(e)}/>
            
            <label htmlFor={"workoutSessionCount"}>Session Count: </label>
            <input value={sessionCount} name={"workoutSessionCount"} type={"number"} onChange={(e) => handleChange(e)}/>
            
            <button onClick={() => {updateStage(2)}}>Next</button>
        </div>
    )
}