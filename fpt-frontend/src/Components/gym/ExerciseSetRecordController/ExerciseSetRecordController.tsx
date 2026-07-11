import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { ExerciseSet, ExerciseSetRecord } from "../../../Types/WorkoutTypes";
import { useEffect, useState } from "react";
import NumberSpinner from "../../../Global styles/mui/NumberSpinner";
import { SubmitHandler, useForm } from "react-hook-form";
import { api } from "../../../api/apiClient";
import * as z from "zod";
interface ExerciseSetRecordControllerProps {
  exerciseSet: ExerciseSet;
}

export default function ({ exerciseSet }: ExerciseSetRecordControllerProps) {
  const {
    selectedSessionId,
    exerciseTypeOptions,
    exerciseOptions,
    todayRecords,
  } = useWorkoutProgrammeContext();
  const { control, reset, handleSubmit } = useForm<ExerciseSetRecord>({
    defaultValues: {
      repsCompleted: 0,
      weight: 0,
      exerciseId: exerciseSet.exerciseId,
      exerciseTypeId: exerciseSet.exerciseTypeId,
      exerciseSetId: exerciseSet.tempId != null ? null : exerciseSet.id,
    },
  });

  const exerciseSetRecordSchema = z.object({
    repsCompleted: z.number().min(0).max(99),
    weight: z
      .number()
      .min(0, { message: "Weight cannot be less than 0" })
      .max(999, { message: "Weight must be less than 999" }),
    exerciseId: z.number({ message: "Please select an exercise" }),
    exerciseTypeId: z.number(),
    exerciseSetId: z.number().nullable(),
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
    console.log(exerciseSet);
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
