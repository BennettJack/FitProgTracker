import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { useFieldArray, useFormContext } from "react-hook-form";
import {
  createEmptySession,
  WorkoutProgramme,
} from "../../../schemas/workoutProgrammeSchema";
import styles from "./ExerciseSessionList.module.css";
import { Button } from "@mui/material";

export default function ExerciseSessionList() {
  const { isEditable, setSelectedSession, selectedSession } =
    useWorkoutProgrammeContext();

  const { watch, control } = useFormContext<WorkoutProgramme>();
  const { append } = useFieldArray({
    control,
    name: "sessions",
  });
  return (
    <div className={styles.sessionList}>
      {watch("sessions").map((session, index) => (
        <div
          className={
            selectedSession?.session.name === session.id ||
            selectedSession?.session.tempId === session.tempId
              ? styles.activeSession
              : styles.sessionSelector
          }
          key={session.id ?? session.tempId}
          onClick={() => {
            setSelectedSession({ index: index, session: session });
          }}
        >
          {session.name}
        </div>
      ))}
      {isEditable && (
        <Button
          variant="contained"
          onClick={() => append(createEmptySession())}
        >
          Add Session
        </Button>
      )}
    </div>
  );
}
