import React, {use, useEffect, useState} from 'react';
import axios from "axios";
import {SelectOption} from "../../../Components/CustomElements/Select";
import {DropdownResponse} from "../../../Components/Gym Components/AddNewExercise";
import {NewWorkoutInitialSetup} from "../../../Components/Gym Components/NewProgram/NewWorkoutInitialSetup";
import styles from './AddNewWorkoutProgram.module.css'
import {ExerciseSession, ExerciseSessionProps} from "../../../Components/Gym Components/ExerciseSession";

export type WorkoutProgramProps = {
    programName: string;
    programDescription: string;
    sessionCount: number;
    sessionProps?: SessionProps[]
}

export type SessionProps = {
    sessionKey: number;
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
    const [currentSessionKey, setCurrentSessionKey] = useState<number | null>(null)
    const currentSession = sessions.find(s => s.sessionKey === currentSessionKey);
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
    
    
    function handleSetupChange(change: Partial<WorkoutProgramProps>){
        setProgram((prev) => ({
            ...prev,
            ...change,
        }))
    }
    
    function handleAddNewExercise(key: number, change: Partial<ExerciseSessionProps>) {
        setSessions((prev) =>
            prev.map((item) =>
                item.sessionKey === key ? { ...item, ...change } : item
            )
        );
    }
    
    
    useEffect(() => {
        switch (stage) {
            case 1:
                break;
            case 2:
                const tempSessions: SessionProps[] = []
                for(let i = 0; i < program.sessionCount; i++ ) {
                   tempSessions.push({
                       sessionKey: i,
                        sessionName: "Session " + i,
                        exercises: []
                    })
                }
                console.log(tempSessions);
                setSessions(tempSessions);
                break;
            default:
                break;
        }

    }, [stage]);

    useEffect(() => {

    }, [sessions]);
    
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
                    <div className={styles.header}> <p> {program.programName}</p></div>
                    <div className={styles.sidebar}>{
                        (sessions.map( session => (
                            <div key={session.sessionKey} className={styles.sessionSidebar} 
                                 onClick={() => setCurrentSessionKey(session.sessionKey)}>
                                <p>{session.sessionName}</p>
                            </div>
                        )))}
                    </div>
                    <div className={styles.content}>
                        {currentSession &&
                            <div>
                                <p>hi</p>
                                <ExerciseSession edit sessionKey={currentSession.sessionKey} 
                                                 sessionName={currentSession.sessionName} 
                                                 exercises={currentSession.exercises}
                                                 onChange={handleAddNewExercise}/>
                            </div>
                        }
                    </div>
                    <div className={"footer"}><p>footer</p></div>
                </div>
            }
              
            
        </div>
    )
    
    
    
}