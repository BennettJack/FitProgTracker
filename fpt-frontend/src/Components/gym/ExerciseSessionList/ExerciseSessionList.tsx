import { useState } from "react";
import { ArrowIconButton } from "../../../Global styles/mui/ArrowIconButton";
import styles from "./ExerciseSessionList.module.css";
import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { Button } from "@mui/material";

export default function ExerciseSessionList() {
  const [hidden, setHidden] = useState(true);
  const { workoutProgrammeData, isEditable, setSelectedSessionId, addSession } =
    useWorkoutProgrammeContext();

  return (
    <div className={styles.wrapper}>
      <div className={`${styles.container} ${!hidden ? styles.open : ""}`}>
        <div className={styles.sessionList}>
          {workoutProgrammeData.sessions.map((session) => (
            <div
              className={styles.sessionSelector}
              key={session.id ?? session.tempId}
              onClick={() =>
                setSelectedSessionId(session.id ?? session.tempId ?? null)
              }
            >
              {session.name}
            </div>
          ))}
          {isEditable && (
            <Button variant="contained" onClick={addSession}>
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
