import {
  Session,
  ExerciseSessionControllerProps,
  SetBloc,
} from "../../Types/WorkoutTypes";
import React from "react";
import { ExerciseSetBlocController } from "./ExerciseSetBlocController";
import { v4 as uuidv4 } from "uuid";

export function ExerciseSessionController({
  exerciseSession,
  updateProgramme,
  mode,
}: ExerciseSessionControllerProps) {
  const updateSession = (updater: (prev: Session) => Session) => {
    updateProgramme?.((prevProgramme) => ({
      ...prevProgramme,
      sessions: prevProgramme.sessions.map((session) =>
        (session.id ?? session.tempId) ===
        (exerciseSession.id ?? exerciseSession.tempId)
          ? updater(session)
          : session,
      ),
    }));
  };

  const addBloc = () => {
    let blocCount = exerciseSession.setBlocs.length;
    blocCount += 1;

    const newBloc: SetBloc = {
      tempId: uuidv4(),
      name: "Exercise" + blocCount,
      sets: [],
    };

    updateSession((prev) => ({
      ...prev,
      setBlocs: [...prev.setBlocs, newBloc],
    }));
  };

  const handleUpdateSession = (e: React.ChangeEvent<HTMLInputElement>) => {
    const name = e.target.value;

    updateSession((prev) => ({
      ...prev,
      name,
    }));
  };

  return (
    <>
      <label htmlFor={"sessionName"}>Session Name</label>
      <input
        name={"sessionName"}
        type={"text"}
        onChange={handleUpdateSession}
        value={exerciseSession.name}
      />
      <p>Exercises:</p>
      {exerciseSession.setBlocs.map((bloc, index) => (
        <ExerciseSetBlocController
          mode={mode}
          updateSession={updateSession}
          exerciseSetBloc={bloc}
        />
      ))}
      {mode === "create" && <button onClick={addBloc}>Add an exercise</button>}
    </>
  );
}
