export type WorkoutProgramme = {
    id?: number
    name: string
    sessions: Session[]
}

export type Session = {
    id?: number
    tempId?: string
    name: string
    setBlocs: SetBloc[]
}

export type SetBloc = {
    id?: number
    tempId?: string
    name: string
    sets: Set[]
}

export type Set = {
    id?: number
    tempId?: string
    name: string
    description: string
    repCeiling: string
    repFloor: string
    
}

export type SetRecord = {
    exerciseSetId: number
    repsCompleted: number
    weight: number
}

export type ControllerMode = "create" | "edit" | "input"

export type WorkoutProgrammeControllerProps = {
    workoutProgrammeId? : number
    workoutProgrammeName?: string
    mode: ControllerMode
}

export type ExerciseSessionControllerProps = {
    exerciseSession: Session
    mode: ControllerMode
    updateProgramme?: (
        updater: (prev: WorkoutProgramme) => WorkoutProgramme
    ) => void;
}

export type ExerciseSetBlocControllerProps = {
    exerciseSetBloc: SetBloc
    mode: ControllerMode
    updateSession?: (
        updater: (prev: Session) => Session
    ) => void
}

export type ExerciseSetControllerProps = {
    exerciseSet: Set
    mode: ControllerMode
    updateExerciseSetBloc: (
        updater:(prev: SetBloc) => SetBloc
    ) => void
    removeExerciseSet: () => void
}

export type UpdateProgrammeData = (
    updater: (prev: WorkoutProgramme) => WorkoutProgramme) => void