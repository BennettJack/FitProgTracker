import { useWorkoutProgrammeContext } from "./WorkoutProgrammeContext";
import { useEffect } from "react";
import styles from "./WorkoutProgrammeController.module.css";
import ExerciseSessionController from "../../../Components/gym/ExerciseSessionController";

export default function WorkoutProgrammeController() {
  const {
    workoutProgrammeData,
    addSession,
    isEditable,
    updateProgrammeField,
    selectedSession,
    setSelectedSessionId,
  } = useWorkoutProgrammeContext();

  useEffect(() => {
    console.log(workoutProgrammeData);
  }, [workoutProgrammeData]);

  return (
    <div className={styles.wrapper}>
      <div className={styles.programmeContainer}>
        <div className={styles.header}>
          {isEditable ? (
            <input
              type="text"
              value={workoutProgrammeData.name}
              onChange={(e) => updateProgrammeField("name", e.target.value)}
            />
          ) : (
            <p>{workoutProgrammeData.name}</p>
          )}
        </div>
        <div>
          {workoutProgrammeData.sessions.map((session) => (
            <div
              className={styles.sessionSidebar}
              key={session.id ?? session.tempId}
              onClick={() =>
                setSelectedSessionId(session.id ?? session.tempId ?? null)
              }
            >
              {session.tempId} {session.name}
            </div>
          ))}
          <button onClick={addSession}>Add Session</button>
        </div>
        <div className={styles.content}>
          {selectedSession && <ExerciseSessionController />}
        </div>
      </div>
    </div>
  );
}
