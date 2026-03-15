export type Muscle = {
  id: number;
  muscleName: string;
  muscleGroup: MuscleGroup;
};

export type MuscleGroup = {
  id: number;
  muscleGroupName: string;
};

export type Equipment = {
  id: number;
  name: string;
};

export type Exercise = {
  id?: number;
  exerciseName: string;
  muscles: Muscle[];
  equipment: Equipment[];
};
