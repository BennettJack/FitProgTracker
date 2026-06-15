import { useState } from "react";
import { ArrowIconButton } from "../../../Global styles/mui/ArrowIconButton";
import styles from "./ExerciseSessionList.module.css";

export default function ExerciseSessionList() {
  const [hidden, setHidden] = useState(true);

  return (
    <div>
      <ArrowIconButton
        label={"View Programme"}
        direction={"right"}
        className={styles.iconButton}
      />
    </div>
  );
}
