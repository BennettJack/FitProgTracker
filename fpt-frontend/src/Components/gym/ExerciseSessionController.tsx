import { ExerciseSetBloc, Session } from "../../Types/WorkoutTypes";
import React, { useState } from "react";
import { useWorkoutProgrammeContext } from "../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { v4 as uuidv4 } from "uuid";
import ExerciseSetBlocController from "./ExerciseSetBlocController";

export default function ExerciseSessionController() {
  const {
    selectedSession,
    updateSession,
    isEditable,
    exerciseTypeOptions,
    updateSetBloc,
  } = useWorkoutProgrammeContext();
  const [exerciseType, setExerciseType] = useState<string>(
    exerciseTypeOptions.find(
      (option) => option.label === "Standard" ?? exerciseTypeOptions[0].label,
    )?.value ?? exerciseTypeOptions[0].value,
  );
  const addSetBloc = () => {
    if (!isEditable) return;

    const newSetBloc: ExerciseSetBloc = {
      tempId: uuidv4(),
      name: `Exercise ${(selectedSession?.setBlocs.length ?? 0) + 1}`,
      sets: [],
      type: exerciseType,
    };

    updateSession(selectedSession?.id ?? selectedSession?.tempId, (prev) => ({
      ...prev,
      setBlocs: [...prev.setBlocs, newSetBloc],
    }));
  };
  return (
    <>
      <p>{selectedSession?.tempId ?? "No session selected"}</p>
      {selectedSession?.setBlocs.map((setBloc) => (
        <ExerciseSetBlocController setBloc={setBloc} />
      ))}
      <button onClick={() => addSetBloc()}>Add Exercise</button>
    </>
  );
}
