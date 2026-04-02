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
import axios, { AxiosResponse } from "axios";
import { v4 as uuidv4 } from "uuid";
import { keycloak } from "../../../keycloak.ts";

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
  const editable: boolean = mode === "create" || "edit" ? true : false;
  const getSelectedSession = (): Session | null => {
    if (!selectedSessionId) return null;

    return (
      workoutProgrammeData.sessions.find(
        (session) =>
          session.id === selectedSessionId ||
          session.tempId === selectedSessionId,
      ) ?? null
    );
  };

  const selectedSession = getSelectedSession();

  const updateProgrammeData: UpdateProgrammeData = (updater) => {
    setWorkoutProgrammeData((prevState) => updater(prevState));
  };

  //functions
  const fetchWorkoutProgrammeData = async (): Promise<
    WorkoutProgramme | undefined
  > => {
    try {
      const res = await axios.get<WorkoutProgramme>("/api/workoutProgramme", {
        headers: { Authorization: `Bearer ${keycloak.token}` },
      });
      return res.data;
    } catch (error) {
      console.error(error);
    }
  };
  const updateWorkoutProgrammeData = async (
    e: React.ChangeEvent<HTMLInputElement>,
  ): Promise<void> => {
    const { name, value } = e.target;

    setWorkoutProgrammeData((prev) => ({
      ...prev,
      [name]: value,
    }));
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

  const logData = () => {
    console.log(`session: ${workoutProgrammeData}`);
    console.log(`current session: ${selectedSessionId}`);
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
      const { data }: AxiosResponse<WorkoutProgramme> =
        await axios.post<WorkoutProgramme>(
          "https://localhost:7206/api/WorkoutProgramme/updateWorkoutProgramme",
          workoutProgrammeData,
        );
      setWorkoutProgrammeData(data);
    } catch (error) {}
    setWaitingUpdateResponse(false);
  };
  const removeSession = (id: number | string | undefined) => {
    setWorkoutProgrammeData?.((prev) => ({
      ...prev,
      sessions: prev.sessions.filter(
        (session) => (session.id ?? session.tempId) !== id,
      ),
    }));
  };
  const getWorkoutProgramme = async () => {
    try {
      keycloak.updateToken(30);
      await axios
        .get(
          "https://localhost:7206/api/WorkoutProgramme/getWorkoutProgramme?id=3",
          {
            headers: { Authorization: `Bearer ${keycloak.token}` },
          },
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
          <h2>
            <input
              name={"name"}
              disabled={editable}
              value={workoutProgrammeData?.name}
              onChange={updateWorkoutProgrammeData}
            />
          </h2>
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
              <button
                onClick={() => removeSession(session.id ?? session.tempId)}
              >
                x
              </button>
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
              removeSession={() =>
                removeSession(selectedSession.id ?? selectedSession.tempId)
              }
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
