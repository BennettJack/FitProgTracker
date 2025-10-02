import {WorkoutProgramPreview} from "./WorkoutProgramPreview";
import React, {useEffect, useState} from "react";
import styles from "./WorkoutProgramPreviewContainer.module.css"
import {useNavigate} from "react-router";

export function WorkoutProgramPreviewContainer() : React.ReactElement {
    
    const navigate = useNavigate();

    return(
        <div className={styles.wrapper}>
            <h1>My Programs</h1>
            <WorkoutProgramPreview />
            <h2>Add a new program</h2>
            <button onClick={() => navigate("/newWorkoutProgram")}>Add New Workout Program</button>
        </div>
    )
}