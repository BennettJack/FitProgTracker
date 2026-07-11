export type WorkoutProgramme = {
  id?: number;
  name: string;
  sessions: Session[];
};

export type Session = {
  id?: number;
  tempId?: string;
  name: string;
  setBlocs: ExerciseSetBloc[];
};

export type ExerciseSetBloc = {
  id?: number;
  tempId?: string;
  name: string;
  sets: ExerciseSet[];
};

export type ExerciseSet = {
  id?: number;
  tempId?: string;
  description: string;
  repCeiling: string;
  repFloor: string;
  exerciseTypeId: number;
  exerciseId?: number;
  todayRecord?: ExerciseSetRecord;
};

export type ExerciseSetRecord = {
  repsCompleted: number;
  weight: number;
  exerciseId: number;
  exerciseTypeId: number;
  exerciseSetId?: number | null;
};

export type ControllerMode = "create" | "edit" | "view";

export type UnitOfWeight = "kg" | "lbs";
