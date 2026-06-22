import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { ExerciseSet } from "../../../Types/WorkoutTypes";
import { useEffect } from "react";
import NumberSpinner from "../../../Global styles/mui/NumberSpinner";
import { SubmitHandler, useForm } from "react-hook-form";
interface ExerciseSetRecordControllerProps {
  exerciseSet: ExerciseSet;
}

type ExerciseSetRecord = {
  reps: number;
  weight: number;
  exerciseId: number;
  sessionId: number;
};
export default function ({ exerciseSet }: ExerciseSetRecordControllerProps) {
  const { selectedSessionId } = useWorkoutProgrammeContext();
  const { control, watch, reset, handleSubmit } = useForm<ExerciseSetRecord>({
    defaultValues: {
      reps: 0,
      weight: 0,
    },
  });

  const reps = watch("reps");
  const weight = watch("weight");

  const onSubmit: SubmitHandler<ExerciseSetRecord> = (data) => {
    console.log(data);
  };

  useEffect(() => {
    if (exerciseSet.repFloor === undefined) {
      exerciseSet.repFloor = "0";
    }
    if (exerciseSet.repCeiling === undefined) {
      exerciseSet.repCeiling = "0";
    }
  });

  useEffect(() => {}, []);
  return (
    <div>
      <p>
        Target reps: {exerciseSet.repFloor ?? "oops"} -{" "}
        {exerciseSet.repCeiling ?? "oops"}
      </p>
      <form onSubmit={handleSubmit(onSubmit)}>
        <NumberSpinner
          name={"reps"}
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
