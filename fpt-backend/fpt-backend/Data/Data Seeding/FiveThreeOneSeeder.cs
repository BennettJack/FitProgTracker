using fpt_backend.Data;
using fpt_backend.Data.Models.GymModels.Instances;

public static class FiveThreeOneSeeder
{
    public static void Seed(FptDbContext context)
    {
        if (context.WorkoutProgrammeTemplates.Any(x =>
            x.Name == "Five-Three-One standard template"))
            return;

        var template = new WorkoutProgrammeTemplate
        {
            Name = "Five-Three-One standard template",
            Description = "Five-Three-One standard template",
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
            CreatedBy = "SYSTEM",

            SessionTemplates = new List<SessionTemplate>
            {
                CreateSession("Overhead Press", 1, 4),
                CreateSession("Deadlift", 2, 3),
                CreateSession("Bench Press", 3, 1),
                CreateSession("Squat", 4, 2)
            }
        };

        context.WorkoutProgrammeTemplates.Add(template);
        context.SaveChanges();
    }

    private static SessionTemplate CreateSession(string name, int order, int exerciseId)
    {
        return new SessionTemplate
        {
            Name = name,
            DisplayOrder = order,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
            CreatedBy = "SYSTEM",

            SetBlocTemplates = new List<SetBlocTemplate>
            {
                CreateBloc(name, exerciseId, 1, FtoWeekOne()),
                CreateBloc(name, exerciseId, 2, FtoWeekTwo()),
                CreateBloc(name, exerciseId, 3, FtoWeekThree()),
                CreateBloc(name, exerciseId, 4, FtoWeekFour())
            }
        };
    }

    private static SetBlocTemplate CreateBloc(
        string name,
        int exerciseId,
        int order,
        List<SetTemplate> sets)
    {
        return new SetBlocTemplate
        {
            Name = name,
            ExerciseId = exerciseId,
            DisplayOrder = order,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
            CreatedBy = "SYSTEM",
            SetTemplates = sets
        };
    }

    private static List<SetTemplate> FtoWeekOne()
    {
        return new List<SetTemplate>
        {
            Set(1,5,5),
            Set(2,5,5),
            Set(3,3,3),
            Set(4,5,5),
            Set(5,5,5),
            Set(6,5,5),
            Set(7,10,10),
            Set(8,10,10),
            Set(9,10,10),
            Set(10,10,10),
            Set(11,10,10)
        };
    }

    private static List<SetTemplate> FtoWeekTwo()
    {
        return new List<SetTemplate>
        {
            Set(1,5,5),
            Set(2,5,5),
            Set(3,3,3),
            Set(4,3,3),
            Set(5,3,3),
            Set(6,3,3),
            Set(7,10,10),
            Set(8,10,10),
            Set(9,10,10),
            Set(10,10,10),
            Set(11,10,10)
        };
    }

    private static List<SetTemplate> FtoWeekThree()
    {
        return new List<SetTemplate>
        {
            Set(1,5,5),
            Set(2,5,5),
            Set(3,3,3),
            Set(4,5,5),
            Set(5,3,3),
            Set(6,1,1),
            Set(7,10,10),
            Set(8,10,10),
            Set(9,10,10),
            Set(10,10,10),
            Set(11,10,10)
        };
    }

    private static List<SetTemplate> FtoWeekFour()
    {
        return new List<SetTemplate>
        {
            Set(1,5,10),
            Set(2,5,10),
            Set(3,5,10),
            Set(4,10,10),
            Set(5,10,10),
            Set(6,10,10),
            Set(7,10,10),
            Set(8,10,10)
        };
    }

    private static SetTemplate Set(int order, int floor, int ceiling)
    {
        return new SetTemplate
        {
            DisplayOrder = order,
            RepFloor = floor,
            RepCeiling = ceiling,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
            CreatedBy = "SYSTEM"
        };
    }
}