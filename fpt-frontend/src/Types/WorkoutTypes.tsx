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
  exerciseTypeId: number;
  exerciseId?: number;
};

export type ExerciseSet = {
  id?: number;
  tempId?: string;
  name: string;
  description: string;
  repCeiling: string;
  repFloor: string;
};

export type SetRecord = {
  exerciseSetId: number;
  repsCompleted: number;
  weight: number;
};

export type ControllerMode = "create" | "edit" | "view";

export type UnitOfWeight = "kg" | "lbs";
