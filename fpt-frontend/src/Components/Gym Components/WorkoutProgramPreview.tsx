import {ReactElement, useEffect} from "react";
import styles from "./WorkoutProgramPreview.module.css"

type Props = {
    programName?: string,
    programId?: number,
    edit?: false
}

export function WorkoutProgramPreview({edit, programId, programName}: Props): ReactElement {

    useEffect(() => {
        
        if(edit){
            //get    
        }
        
        
    }, []);
    return(
        <div className={styles.wrapper}>
            <div className={styles.valueBox}><p>test</p></div>
            <div className={styles.divider}></div>
            <div className={styles.valueBox}>
                    <p>test</p>
            </div>
            <div className={styles.divider}></div>
            <div className={styles.valueBox}><div ><p>test</p></div></div>
            <div className={styles.divider}></div>
            <div className={styles.valueBox}><div ><p>test</p></div></div>
        </div>
    )
}