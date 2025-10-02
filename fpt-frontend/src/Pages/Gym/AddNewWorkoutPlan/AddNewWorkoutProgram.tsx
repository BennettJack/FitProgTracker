import React, {use, useEffect, useState} from 'react';
import axios from "axios";
import {SelectOption} from "../../../Components/CustomElements/Select";
import {DropdownResponse} from "../../../Components/Gym Components/AddNewExercise";
import {NewWorkoutInitialSetup} from "../../../Components/Gym Components/NewProgram/NewWorkoutInitialSetup";
import styles from './AddNewWorkoutProgram.module.css'

export type WorkoutProgramProps = {
    programName: string;
    programDescription: string;
    sessionCount: number;
    sessionProps?: SessionProps[]
}

type SessionProps = {
    key: number;
    sessionName: string;
    exercises: ExerciseProps[]
}

type ExerciseProps = {
    exerciseId: string,
    order: number,
}
export function AddNewWorkoutProgram() :React.ReactElement {
    
    const [program, setProgram] = useState<WorkoutProgramProps>({
        programName: "",
        programDescription: "",
        sessionCount: 0,
    })
    const [sessions, setSessions] = useState<SessionProps[]>([]);
    const [exercises, setExercises ] = useState<SelectOption[]>([]);
    const [stage, setStage] = useState<number>(1)
    
    const fetchData = async () => {

        await axios.get<DropdownResponse>(process.env.REACT_APP_DEV_API_HOST +"exercise/getOptionData")
            .then((response) => {
                const options: SelectOption[] = response.data.data.map((item) => ({
                    label: item.label,
                    value: String(item.value)
                }));
                setExercises(options);
            }).catch((err) => console.log(err));
    }

    fetchData().then(r => console.log(r));
    
    function handleSetupChange(change: Partial<WorkoutProgramProps>){
        setProgram((prev) => ({
            ...prev,
            ...change,
        }))
    }

    useEffect(() => {
        console.log(stage)
        for(let i = 0; i < program.sessionCount; i++ ){
            const tempSession: SessionProps = {
                key: i,
                sessionName: "Session " + i,
                exercises: []
            }
            
            setSessions((prev) =>
                [...prev, tempSession]
            )
        }
        
        console.log(sessions)
    }, [stage]);
    
    return (
        <div className={styles.wrapper}>
            {stage === 1 &&
                <NewWorkoutInitialSetup 
                    programName={program.programName}
                    programDescription={program.programDescription} 
                    sessionCount={program.sessionCount} 
                    onChange={handleSetupChange} 
                    updateStage={setStage}/>
            }
            {stage === 2 &&
                <div className={styles.container}>
                    <div className={styles.header}> <p> header</p></div>
                    <div className={styles.sidebar}>{
                        (sessions.map( session => (
                            <div className={styles.sessionSidebar}><p>{session.sessionName}</p></div>
                        )))}
                    </div>
                    <div className={styles.content}>
                        <p>content</p>
                    </div>
                    <div className={"footer"}><p>footer</p></div>
                </div>
            }
              
            
        </div>
    )
    
    
    
}