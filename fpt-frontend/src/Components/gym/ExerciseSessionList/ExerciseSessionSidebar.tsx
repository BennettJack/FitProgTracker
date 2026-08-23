import { useEffect, useState } from "react";
import { ArrowIconButton } from "../../../Global styles/mui/ArrowIconButton";
import styles from "./ExerciseSessionList.module.css";
import ExerciseSessionList from "./ExerciseSessionList";
import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";

export default function ExerciseSessionSidebar() {
  const [hidden, setHidden] = useState(true);
  const { selectedSession } = useWorkoutProgrammeContext();
  useEffect(() => {
    setHidden(true);
  }, [selectedSession]);
  return (
    <div className={styles.wrapper}>
      <div className={`${styles.container} ${!hidden ? styles.open : ""}`}>
        <ExerciseSessionList />
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
