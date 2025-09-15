namespace fpt_backend.Data.Models.Gym;

public class ExerciseBlock
{
    public int ExerciseBlockId { get; set; }
    public required Exercise Exercise { get; set; }
}