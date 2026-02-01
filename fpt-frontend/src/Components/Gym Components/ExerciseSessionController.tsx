import {ExerciseSession, ExerciseSessionControllerProps, ExerciseSetBloc} from "../../Types/WorkoutTypes";
import React from "react";
import {ExerciseSetBlocController} from "./ExerciseSetBlocController";


export function ExerciseSessionController(
    {
        exerciseSession,
        updateProgramme,
        mode
    } : ExerciseSessionControllerProps){

    const updateSession = (
        updater: (prev: ExerciseSession) => ExerciseSession
    ) => {
        updateProgramme?.(prevProgramme => ({
            ...prevProgramme,
            workoutSessions: prevProgramme.workoutSessions.map(session =>
                (session.id ?? session.tempId) === (exerciseSession.id ?? exerciseSession.tempId)
                    ? updater(session)
                    : session
            ),
        }));
    };
    
    const addBloc = () =>{
        let blocCount = exerciseSession.exerciseSetBlocs.length
        blocCount += 1
        
        const newBloc: ExerciseSetBloc = {
            name: "Exercise" + blocCount,
            exerciseSets: []
        }
        
        updateSession(prev => ({
            ...prev,
            exerciseSetBlocs: [...prev.exerciseSetBlocs, newBloc]
        }))
    }

    const handleUpdateSession = (
        e: React.ChangeEvent<HTMLInputElement>
    ) => {
        const name = e.target.value;

        updateSession(prev => ({
            ...prev,
            name,
        }));
    };
    
    return(
        <>
            <label htmlFor={"sessionName"}>Session Name</label>
            <input
                name={"sessionName"} 
                type={"text"} 
                onChange={handleUpdateSession}
                value={exerciseSession.name}
            />
            <p>Exercises:</p>
            {exerciseSession.exerciseSetBlocs.map((bloc, index) =>
            <ExerciseSetBlocController 
                mode={mode}
                updateSession={updateSession}
                exerciseSetBloc={bloc}
            />
            )}
            {mode === "create" && (
                <button onClick={addBloc}>Add an exercise</button>
            )}
        </>
    )
}