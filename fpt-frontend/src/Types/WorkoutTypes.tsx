export type WorkoutProgram = {
    id?: number
    name: string
    workoutSessions: WorkoutSession[]
}

export type WorkoutSession = {
    id?: number
    name: string
    exerciseSetBlocks: ExerciseSetBloc[]
}

export type ExerciseSetBloc = {
    id?: number
    name: string
    exerciseSets: ExerciseSet[]
}

export type ExerciseSet = {
    id?: number
    name: string
    repCeiling: number
    repFloor: number
    
}

export type ExerciseSetRecord = {
    exerciseSetId: number
    repsCompleted: number
    weight: number
}



export type WorkoutProgramBuilderProps = {
    workoutProgramId? : number
    sessionCount?: number
    workoutProgramName?: string
}

export type sessionBuilderProps = {
    sessionId? : number
    sessionName? : string
    
}