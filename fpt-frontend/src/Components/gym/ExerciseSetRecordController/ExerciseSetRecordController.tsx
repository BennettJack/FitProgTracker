import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { ExerciseSet } from "../../../Types/WorkoutTypes";
import { useEffect } from "react";
interface ExerciseSetRecordControllerProps {
  exerciseSet: ExerciseSet;
}
export default function ({ exerciseSet }: ExerciseSetRecordControllerProps) {
  const { selectedSessionId } = useWorkoutProgrammeContext();

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
        Target reps: {exerciseSet.repFloor ?? "oops"} -{" "}
        {exerciseSet.repCeiling ?? "oops"}
      </p>
    </div>
  );
}
