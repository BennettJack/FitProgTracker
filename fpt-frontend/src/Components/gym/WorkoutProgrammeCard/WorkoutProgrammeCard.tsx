import { ThemeProvider } from "@mui/material";
import aminoTheme from "../../../Global styles/mui/aminoTheme";
import styles from "./WorkoutProgrammeCard.module.css";
import { RightArrowIconButton } from "../../../Global styles/mui/RightArrowIconButton";
import { Navigate } from "react-router-dom";
import { useNavigate } from "react-router";

interface WorkoutProgrammeCardProps {
  programmeId: number;
  programmeName: string;
}

export const WorkoutProgrammeCard = (props: WorkoutProgrammeCardProps) => {
  const navigate = useNavigate();
  return (
    <ThemeProvider theme={aminoTheme}>
      <div className={styles.cardWrapper}>
        <h2 className={styles.content}>{props.programmeName}</h2>
        <div className={styles.arrow}>
          <RightArrowIconButton
            label={"View Programme"}
            sx={(theme) => ({
              width: "100%",
              height: "100%",
              paddingRight: "1rem",
              paddingLeft: "1rem",
              borderRadius: 0,
              [`.${styles.arrow}:hover &`]: {
                color: theme.palette.primary.main,
              },
            })}
            onClick={() => {
              navigate(`/workoutProgramme/${props.programmeId}`);
            }}
          />
        </div>
      </div>
    </ThemeProvider>
  );
};
