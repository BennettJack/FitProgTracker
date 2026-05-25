import { ExerciseSetBloc } from "../../Types/WorkoutTypes";
import { useWorkoutProgrammeContext } from "../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";

type ExerciseSetBlocControllerProps = {
  setBloc: ExerciseSetBloc;
};

export default function ExerciseSetBlocController({
  setBloc,
}: ExerciseSetBlocControllerProps) {
  const { updateSetBloc } = useWorkoutProgrammeContext();

  return <></>;
}
