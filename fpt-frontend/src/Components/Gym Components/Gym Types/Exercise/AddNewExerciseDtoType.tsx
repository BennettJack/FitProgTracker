export type AddNewExerciseDto = {
    exerciseName: string,
    muscleIds: number[],
    equipmentIds: number[],
    description?: string
}