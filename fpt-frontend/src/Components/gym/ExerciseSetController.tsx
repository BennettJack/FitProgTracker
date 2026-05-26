import { ExerciseSet } from "../../Types/WorkoutTypes";

type ExerciseSetProps = {
  exerciseSet: ExerciseSet;
};
export default function ExerciseSetController({
  exerciseSet,
}: ExerciseSetProps) {
  return (
    <>
      <label htmlFor={"repCeilingInput"}>Rep Ceiling</label>
      <input
        name={"repCeilingInput"}
        type={"number"}
        min={0}
        max={99}
        value={exerciseSet.repCeiling}
      />
      <label htmlFor={"repFloorInput"}>Rep Floor</label>
      <input
        name={"repFloorInput"}
        type={"number"}
        min={0}
        max={99}
        value={exerciseSet.repFloor}
      />
      <label htmlFor={"descriptionInput"}>Description</label>
      <input
        name={"descriptionInput"}
        type={"text"}
        value={exerciseSet.description}
      />
    </>
  );
}
