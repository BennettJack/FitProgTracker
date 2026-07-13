import { useEffect, useState } from "react";
import { ArrowIconButton } from "../../../Global styles/mui/ArrowIconButton";
import styles from "./ExerciseSessionList.module.css";
import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { Button } from "@mui/material";
import { useFieldArray, useFormContext } from "react-hook-form";
import {
  createEmptySession,
  WorkoutProgramme,
} from "../../../schemas/workoutProgrammeSchema";

export default function ExerciseSessionList() {
  const [hidden, setHidden] = useState(true);
  const { isEditable, setSelectedSession, selectedSession } =
    useWorkoutProgrammeContext();

  const { watch, control } = useFormContext<WorkoutProgramme>();
  const { append } = useFieldArray({
    control,
    name: "sessions",
  });

  useEffect(() => {
    console.log(selectedSession);
  }, [selectedSession]);

  return (
    <div className={styles.wrapper}>
      <div className={`${styles.container} ${!hidden ? styles.open : ""}`}>
        <div className={styles.sessionList}>
          {watch("sessions").map((session, index) => (
            <div
              className={styles.sessionSelector}
              key={session.id ?? session.tempId}
              onClick={() => {
                setSelectedSession({ index: index, session: session });
                setHidden(true);
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
      </div>
      <div
        className={`${styles.iconButtonContainer} ${!hidden ? styles.open : ""}`}
      >
        <ArrowIconButton
          label={"View Programme"}
          direction={hidden ? "right" : "left"}
          className={styles.iconButton}
          onClick={() => setHidden(!hidden)}
          sx={{
            backgroundColor: "var(--colour-primary-background)",
            borderRight: "solid 1px var(--colour-primary-accent)",
            borderTop: "solid 1px var(--colour-primary-accent)",
            borderBottom: "solid 1px var(--colour-primary-accent)",

            borderRadius: "0 50% 50% 0",
          }}
        />
      </div>
    </div>
  );
}
