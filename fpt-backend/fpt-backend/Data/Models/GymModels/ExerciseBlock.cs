namespace fpt_backend.Data.Models.GymModels;

public class ExerciseBlock
{
    public int ExerciseBlockId { get; set; }
    public required Exercise Exercise { get; set; }
}