import React, { use, useEffect, useState } from "react";
import styles from "./WorkoutProgrammeController.module.css";
import {
  ControllerMode,
  WorkoutProgramme,
  WorkoutProgrammeControllerProps,
  Session,
  UpdateProgrammeData,
} from "../../../Types/WorkoutTypes";
import { ExerciseSessionController } from "../../../Components/Gym Components/ExerciseSessionController";
import axios from "axios";
import { v4 as uuidv4 } from "uuid";

export function WorkoutProgrammeController({
  workoutProgrammeId,
  mode,
}: WorkoutProgrammeControllerProps): React.ReactElement {
  const [workoutProgrammeData, setWorkoutProgrammeData] =
    useState<WorkoutProgramme>({
      name: "New Programme",
      sessions: [],
    });

  const [waitingUpdateResponse, setWaitingUpdateResponse] =
    useState<boolean>(false);

  const [selectedSessionId, setSelectedSessionId] = useState<
    number | string | null
  >(null);

  useEffect(() => {
    console.log(workoutProgrammeData);
  }, [workoutProgrammeData]);

  useEffect(() => {
    if (!workoutProgrammeId) {
      console.log("workoutProgrammeId not provided");
      if (mode === "create") {
        setWorkoutProgrammeData({
          name: "New Programme",
          sessions: [],
        } as WorkoutProgramme);
        return;
      }
      //TODO else display message
    }

    console.log(workoutProgrammeId);
    console.log(mode);
    fetchWorkoutProgrammeData().then((data) => {
      if (!data) return;
      setWorkoutProgrammeData(data);
    });
  }, [workoutProgrammeId]);

  const selectedSession =
    workoutProgrammeData.sessions.find(
      (s) => (s.id ?? s.tempId) === selectedSessionId,
    ) ?? null;

  const updateProgrammeData: UpdateProgrammeData = (updater) => {
    setWorkoutProgrammeData((prevState) => updater(prevState));
  };

  //functions
  const fetchWorkoutProgrammeData = async (): Promise<
    WorkoutProgramme | undefined
  > => {
    try {
      const { data } = await axios.get<WorkoutProgramme>(
        "/api/workoutProgramme",
      );
      return data;
    } catch (error) {
      console.error(error);
    }
  };

  const addSession = () => {
    let sessionCount: number = workoutProgrammeData.sessions.length;
    sessionCount += 1;
    const newSession: Session = {
      tempId: uuidv4(),
      name: "session" + sessionCount,
      setBlocs: [],
    };
    setWorkoutProgrammeData((prev) => ({
      ...prev,
      sessions: [...prev.sessions, newSession],
    }));
  };

  useEffect(() => {}, [selectedSessionId]);

  const logData = () => {
    console.log(workoutProgrammeData);
  };

  const submitData = async () => {
    const json = JSON.stringify(workoutProgrammeData);
    console.log(json);
    try {
      await axios
        .post(
          "https://localhost:7206/api/WorkoutProgramme/newWorkoutProgramme",
          workoutProgrammeData,
        )
        .then((data) => {});
    } catch (error) {}
  };

  const updateProgramme = async () => {
    setWaitingUpdateResponse(true);
    try {
      await axios.post(
        "https://localhost:7206/api/WorkoutProgramme/updateWorkoutProgramme",
        workoutProgrammeData,
      );
    } catch (error) {}
    setWaitingUpdateResponse(false);
  };

  const getWorkoutProgramme = async () => {
    try {
      await axios
        .get(
          "https://localhost:7206/api/WorkoutProgramme/getWorkoutProgramme?id=1",
        )
        .then((data) => {
          setWorkoutProgrammeData(data.data);
        });
    } catch (error) {}
  };
  return (
    <div className={styles.wrapper}>
      <div className={styles.programmeContainer}>
        <div className={styles.header}>
          <h2>{workoutProgrammeData?.name}</h2>
        </div>
        <div className={styles.sidebar}>
          {workoutProgrammeData?.sessions.map((session, index) => (
            <div key={session.id ?? session.tempId}>
              <p
                className={styles.sessionSidebar}
                onClick={() =>
                  setSelectedSessionId(session.id ?? session.tempId ?? null)
                }
              >
                {session.name}
              </p>
            </div>
          ))}
          <div className={styles.addSessionBtnWrapper}>
            <button
              className={`${styles.addBtn} ${styles.addSessionBtn}`}
              onClick={addSession}
            >
              Add Session
            </button>
          </div>
        </div>
        <div className={styles.content}>
          {selectedSession && (
            <ExerciseSessionController
              exerciseSession={selectedSession}
              updateProgramme={updateProgrammeData}
              mode="create"
            />
          )}
        </div>
        <button onClick={logData}>Log Data</button>
        <button onClick={submitData}>Submit</button>
        <button onClick={getWorkoutProgramme}>Get programme</button>
        <button disabled={waitingUpdateResponse} onClick={updateProgramme}>
          Update programme
        </button>
        <div className={styles.footer}></div>
      </div>
    </div>
  );
}
