import React, {use, useEffect, useState} from 'react';
import {WorkoutProgram, WorkoutProgramBuilderProps} from "../../../Types/WorkoutTypes";
export function WorkoutProgramBuilder({workoutProgramId, workoutProgramName, sessionCount} : WorkoutProgramBuilderProps) :React.ReactElement {
    
    const [workoutProgramData, setWorkoutProgramData ] = useState<WorkoutProgram | null>(null);
    
    return (
        <>
        </>
    )
    
    
}