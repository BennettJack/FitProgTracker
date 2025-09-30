import {WorkoutProgramPreview} from "./WorkoutProgramPreview";
import React, {useEffect, useState} from "react";
import styles from "./WorkoutProgramPreviewContainer.module.css"

export function WorkoutProgramPreviewContainer() : React.ReactElement {
    return(
        <div className={styles.wrapper}>
            <h1>My Programs</h1>
            
            <h2>Add a new program</h2>
        </div>
    )
}