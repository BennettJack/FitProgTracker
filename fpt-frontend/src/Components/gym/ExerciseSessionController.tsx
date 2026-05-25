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
  const [exerciseType, setExerciseType] = useState<number>(
    Number(
      exerciseTypeOptions.find((option) => option.label === "Standard")
        ?.value ?? exerciseTypeOptions[0].value,
    ),
  );
  const addSetBloc = () => {
    if (!isEditable) return;
    if (selectedSession === null) return;

    const newSetBloc: ExerciseSetBloc = {
      tempId: uuidv4(),
      name: `Exercise ${(selectedSession.setBlocs.length ?? 0) + 1}`,
      sets: [],
      type: exerciseType,
    };

    const sessionId = selectedSession.id ?? selectedSession.tempId;

    if (!sessionId) return;

    updateSession(sessionId, (prev) => ({
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
