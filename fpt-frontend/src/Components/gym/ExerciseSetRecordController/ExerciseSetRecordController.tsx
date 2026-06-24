import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { ExerciseSet } from "../../../Types/WorkoutTypes";
import { useEffect, useState } from "react";
import NumberSpinner from "../../../Global styles/mui/NumberSpinner";
import { SubmitHandler, useForm } from "react-hook-form";
import { api } from "../../../api/apiClient";
interface ExerciseSetRecordControllerProps {
  exerciseSet: ExerciseSet;
}

type ExerciseSetRecord = {
  repsCompleted: number;
  weight: number;
  exerciseId: number;
  exerciseTypeId: number;
};
export default function ({ exerciseSet }: ExerciseSetRecordControllerProps) {
  const { selectedSessionId, exerciseTypeOptions, exerciseOptions } =
    useWorkoutProgrammeContext();
  const { control, reset, handleSubmit } = useForm<ExerciseSetRecord>({
    defaultValues: {
      repsCompleted: 0,
      weight: 0,
      exerciseId: exerciseSet.exerciseId,
      exerciseTypeId: exerciseSet.exerciseTypeId,
    },
  });

  const onSubmit: SubmitHandler<ExerciseSetRecord> = async (data) => {
    try {
      const res = await api.post<ExerciseSetRecord>(
        "api/SetRecord/AddSetRecord",
        data,
      );
    } catch (e) {
      console.log(e);
    }
  };

  useEffect(() => {
    if (exerciseSet.repFloor === undefined) {
      exerciseSet.repFloor = "0";
    }
    if (exerciseSet.repCeiling === undefined) {
      exerciseSet.repCeiling = "0";
    }
  });

  return (
    <div>
      <p>
        {exerciseTypeOptions.find((o) => o.value === exerciseSet.exerciseTypeId)
          ?.label ?? ""}
      </p>
      <p>
        {exerciseOptions.find((o) => o.value === exerciseSet.exerciseId)
          ?.label ?? ""}
      </p>
      <p>
        Target reps: {exerciseSet.repFloor ?? "oops"} -{" "}
        {exerciseSet.repCeiling ?? "oops"}
      </p>
      <form onSubmit={handleSubmit(onSubmit)}>
        <NumberSpinner
          name={"repsCompleted"}
          control={control}
          min={0}
          max={99}
          label={"Reps"}
        />
        <NumberSpinner
          name={"weight"}
          control={control}
          min={0}
          max={99}
          label={"Weight"}
        />

        <input type="submit" />
      </form>
    </div>
  );
}
