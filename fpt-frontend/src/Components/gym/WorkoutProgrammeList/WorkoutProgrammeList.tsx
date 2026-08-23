import styles from "./WorkoutProgrammeList.module.css";
import { useEffect, useState } from "react";
import { api } from "../../../api/apiClient";

import { Button } from "@mui/material";
import { WorkoutProgrammeCard } from "../WorkoutProgrammeCard/WorkoutProgrammeCard";
import { WorkoutProgramme } from "../../../schemas/workoutProgrammeSchema";

export default function WorkoutProgrammeList() {
  const [workoutProgrammes, setWorkoutProgrammes] = useState<
    WorkoutProgramme[]
  >([]);
  const getWorkoutProgrammes = async () => {
    await api
      .get<WorkoutProgramme[]>("/api/WorkoutProgramme/getAll")
      .then((res) => setWorkoutProgrammes(res.data));
  };

  useEffect(() => {
    getWorkoutProgrammes();
  }, []);

  useEffect(() => {
    console.log(workoutProgrammes);
  }, [workoutProgrammes]);
  return (
    <>
      <div className={styles.listWrapper}>
        {workoutProgrammes.length > 0 ? (
          workoutProgrammes.map((programme) => (
            <WorkoutProgrammeCard
              key={programme.id}
              programmeId={programme.id!}
              programmeName={programme.name}
            />
          ))
        ) : (
          <div>No workout programmes found</div>
        )}
      </div>
      <Button>New programme</Button>
    </>
  );
}
