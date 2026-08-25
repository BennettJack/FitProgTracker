import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { useEffect, useState } from "react";
import NumberSpinner from "../../../Global styles/mui/NumberSpinner";
import { SubmitHandler, useForm, useFormContext } from "react-hook-form";
import { api } from "../../../api/apiClient";
import * as z from "zod";
import {
  ExerciseSet,
  ExerciseSetRecord,
} from "../../../schemas/workoutProgrammeSchema";
import { NumberField, RhfNumberField } from "../../Inputs/NumberField";
interface ExerciseSetRecordControllerProps {
  exerciseSet: ExerciseSet;
}

export default function ExerciseSetRecordController({
  exerciseSet,
}: ExerciseSetRecordControllerProps) {
  const { control, handleSubmit } = useFormContext<ExerciseSetRecord>();
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

  return (
    <div>
      <form onSubmit={handleSubmit((formData) => onSubmit(formData))}>
        <div>
          <RhfNumberField
            control={control}
            name="repsCompleted"
            label="Reps completed"
            min={0}
            max={99}
            size="small"
          />
        </div>
        <div>
          <RhfNumberField
            control={control}
            name="weight"
            label="Weight"
            min={0}
            max={999}
            size="small"
          />
        </div>
        <input type="submit" value="Save" />
      </form>
    </div>
  );
}
