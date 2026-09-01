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
  const { control, handleSubmit, reset, formState } =
    useFormContext<ExerciseSetRecord>();
  const { errors } = formState;
  const { todaySessionRecords } = useWorkoutProgrammeContext();
  const [todaySetRecord, setTodaySetRecord] = useState<ExerciseSetRecord>();
  const onSubmit: SubmitHandler<ExerciseSetRecord> = async (data) => {
    try {
      if (todaySetRecord?.id) {
        const res = await api.post<ExerciseSetRecord>(
          "api/SetRecord/UpdateSetRecord",
          data,
        );
      } else {
        const res = await api.post<ExerciseSetRecord>(
          "api/SetRecord/AddSetRecord",
          data,
        );
      }
    } catch (e) {
      console.log(e);
    }
  };
  useEffect(() => {
    if (exerciseSet.id !== undefined && exerciseSet.id !== null) {
      setTodaySetRecord(todaySessionRecords[exerciseSet.id]);
    }
  }, [exerciseSet.id, todaySessionRecords]);

  useEffect(() => {
    reset({
      id: todaySetRecord?.id ?? null,
      repsCompleted: todaySetRecord?.repsCompleted ?? 0,
      weight: todaySetRecord?.weight ?? 0,
      exerciseSetId: exerciseSet.id ?? undefined,
      exerciseId: Number(exerciseSet.exerciseId!),
      exerciseTypeId: Number(exerciseSet.exerciseTypeId!),
    });
  }, [todaySetRecord]);
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
      <div>
        {exerciseSet.id && (
          <p>Record for this set: {todaySetRecord?.repsCompleted}</p>
        )}
      </div>
      <div>
        <p> {todaySetRecord?.weight ?? "0 kg"}</p>
        <p> {`id ${todaySetRecord?.id}`}</p>
        <p> {exerciseSet.id}</p>
      </div>
    </div>
  );
}
