import React, {use, useEffect, useState} from 'react';
import styles from './WorkoutProgrammeBuilder.module.css'
import {ProgrammeMode, WorkoutProgramme, WorkoutProgrammeBuilderProps} from "../../../Types/WorkoutTypes";
import axios from "axios";
import {data} from "react-router";




export function WorkoutProgrammeBuilder({workoutProgrammeId, mode} : WorkoutProgrammeBuilderProps) :React.ReactElement {

    const [workoutProgrammeData, setWorkoutProgrammeData ] = useState<WorkoutProgramme | null>(null);

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



    return (
        <div className={styles.wrapper}>
            <div className={styles.container}>
                <div className={styles.header}></div>
                <div className={styles.sidebar}>
                    <p className={styles.sessionSidebar}>Session 1 here!</p>
                    <p className={styles.sessionSidebar}>aa</p>
                    <p className={styles.sessionSidebar}>aa</p>
                </div>
                <div className={styles.content}></div>
            </div>
            <div className={styles.footer}></div>
        </div>
    )
}