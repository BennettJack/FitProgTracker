import React, {use, useEffect, useState} from 'react';
import styles from './WorkoutProgrammeController.module.css'
import {
    ControllerMode,
    WorkoutProgramme,
    WorkoutProgrammeControllerProps,
    ExerciseSession, UpdateProgrammeData
} from "../../../Types/WorkoutTypes";
import {ExerciseSessionController} from "../../../Components/Gym Components/ExerciseSessionController";
import axios from "axios";
import { v4 as uuidv4 } from "uuid";




export function WorkoutProgrammeController({workoutProgrammeId, mode} : WorkoutProgrammeControllerProps) :React.ReactElement {

    const [workoutProgrammeData, setWorkoutProgrammeData ] = useState<WorkoutProgramme>({
        name: "New Programme",
        workoutSessions: []
    });
    
    const [selectedSessionId, setSelectedSessionId ] = useState<number | string | null>(null);
    const [nextTempId, setNextTempId ] = useState(1);
    
    

    useEffect(() => {
        console.log(workoutProgrammeData);
    }, [workoutProgrammeData]);

    useEffect(() => {
        if (!workoutProgrammeId) {
            console.log("workoutProgrammeId not provided");
            if(mode === "create"){
                setWorkoutProgrammeData({
                    name: "New Programme",
                    workoutSessions: []
                } as WorkoutProgramme);
                return
            }
            //TODO else display message
        }

        console.log(workoutProgrammeId);
        console.log(mode);
        fetchWorkoutProgrammeData()
            .then((data) => {
                if (!data) return;
                setWorkoutProgrammeData(data);
            });

    }, [workoutProgrammeId]);

    const selectedSession = workoutProgrammeData.workoutSessions.find(
        s => (s.id ?? s.tempId) === selectedSessionId
    ) ?? null;

    const updateProgrammeData : UpdateProgrammeData = (updater) => {
        setWorkoutProgrammeData(prevState => updater(prevState));
    }

    //functions
    const fetchWorkoutProgrammeData = async ():Promise<WorkoutProgramme | undefined> => {
        try {
            const {data} = await axios.get<WorkoutProgramme>(
                "/api/workoutProgramme",
            );
            return data
        } catch (error) {
            console.error(error);
        }
    };
    
    const addSession = () =>{
        let sessionCount : number = workoutProgrammeData.workoutSessions.length
        sessionCount += 1
        const newSession: ExerciseSession = {
            tempId: uuidv4(),
            name: "session" + sessionCount,
            exerciseSetBlocs: []
        }
        setWorkoutProgrammeData(prev => ({
            ...prev,
            workoutSessions: [...prev.workoutSessions, newSession]
        }))
    }

    useEffect(() => {
        console.log(selectedSessionId);
    }, [selectedSessionId]);


    return (
        <div className={styles.wrapper}>
            <div className={styles.container}>
                <div className={styles.header}>
                    <h2>{workoutProgrammeData?.name}</h2>
                </div>
                <div className={styles.sidebar}>
                {workoutProgrammeData?.workoutSessions.map((session, index) => 
                    <div key={session.id ?? session.tempId}>
                        <p className={styles.sessionSidebar} onClick={() => setSelectedSessionId(
                            session.id ?? session.tempId ?? null
                        )}>{session.name}</p>
                    </div>
                )}
                <button onClick={addSession}>Add Session</button>
                </div>
                <div className={styles.content}>
                    {selectedSession && (
                        <ExerciseSessionController
                            exerciseSession={selectedSession}
                            updateProgramme={updateProgrammeData}
                            mode="create"
                        />
                    )}
                </div>
                <div className={styles.footer}></div>
            </div>
        </div>
    )
}