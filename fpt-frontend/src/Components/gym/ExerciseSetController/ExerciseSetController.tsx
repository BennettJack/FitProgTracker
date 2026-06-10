import { ExerciseSet, ExerciseSetBloc } from "../../../Types/WorkoutTypes";
import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";

type ExerciseSetProps = {
  exerciseSet: ExerciseSet;
  exerciseSetBloc: ExerciseSetBloc;
};
export default function ExerciseSetController({
  exerciseSet,
  exerciseSetBloc,
}: ExerciseSetProps) {
  const { updateExerciseSet, selectedSessionId } = useWorkoutProgrammeContext();

  const updateSetField = (field: string, value: string) => {
    const setBlocId = exerciseSetBloc.id ?? exerciseSetBloc.tempId;
    const exerciseSetId = exerciseSet.id ?? exerciseSet.tempId;

    if (exerciseSetId === undefined || setBlocId === undefined) return;
    switch (field) {
      case "repCeiling":
        updateExerciseSet(
          selectedSessionId,
          setBlocId,
          exerciseSetId,
          (prev) => ({
            ...prev,
            repCeiling: value,
          }),
        );
        break;
      case "repFloor":
        updateExerciseSet(
          selectedSessionId,
          setBlocId,
          exerciseSetId,
          (prev) => ({
            ...prev,
            repFloor: value,
          }),
        );
        break;
      case "description":
        updateExerciseSet(
          selectedSessionId,
          setBlocId,
          exerciseSetId,
          (prev) => ({
            ...prev,
            description: value,
          }),
        );
        break;
      default:
        break;
    }
  };
  return (
    <>
      <label htmlFor={"repCeilingInput"}>Rep Ceiling</label>
      <input
        name={"repCeilingInput"}
        type={"number"}
        min={0}
        max={99}
        value={exerciseSet.repCeiling}
        onChange={(e) => updateSetField("repCeiling", e.target.value)}
      />
      <label htmlFor={"repFloorInput"}>Rep Floor</label>
      <input
        name={"repFloorInput"}
        type={"number"}
        min={0}
        max={99}
        value={exerciseSet.repFloor}
        onChange={(e) => updateSetField("repFloor", e.target.value)}
      />
      <label htmlFor={"descriptionInput"}>Description</label>
      <input
        name={"descriptionInput"}
        type={"text"}
        value={exerciseSet.description}
        onChange={(e) => updateSetField("description", e.target.value)}
      />
    </>
  );
}
