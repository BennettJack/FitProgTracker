using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.Services.GymServices;

public class WorkoutProgrammeService : BaseService<WorkoutProgramme>, IWorkoutProgrammeService
{
    public WorkoutProgrammeService(
        FptDbContext context) : base(context){}

    public override async Task<WorkoutProgramme> GetByIdAsync(int id)
    {
       var temp = await Context.WorkoutProgrammes
           .Include(x => x.Sessions)
           .ThenInclude(x => x.SetBlocs)
           .ThenInclude(x => x.Sets)
           .FirstOrDefaultAsync(x => x.Id == id);
       return temp;
    }

    public async Task<WorkoutProgramme> AddAsync(WorkoutProgrammeCreateRequest req)
    {
        var programme = new WorkoutProgramme();
        programme.Name = req.Name;
        programme.Description = "";
        
        foreach (var session in req.WorkoutSessions)
        {
            var sessionToAdd = new Session()
            {
                Name =  session.Name,
                WorkoutProgramme =  programme,
                WorkoutProgrammeId = programme.Id,
                DisplayOrder = 9,
                Created = DateTime.Now,
                CreatedBy = "SYSTEM",
            };
            
            var blocsToAdd = new List<SetBloc>();
            foreach (var bloc in session.ExerciseSetBlocs)
            {
                var blocToAdd = new SetBloc()
                {
                    Name =  bloc.Name,
                    Session =  sessionToAdd,
                    SessionId = sessionToAdd.Id,
                    DisplayOrder = 5,
                    Created =  DateTime.Now,
                    CreatedBy = "SYSTEM"
                };
                var setsToAdd = bloc.ExerciseSets
                    .Select(set => new Set
                    {
                        SetBloc = blocToAdd,
                        SetBlocId = blocToAdd.Id,
                        RepFloor = Int32.Parse(set.RepFloor),
                        RepCeiling =  Int32.Parse(set.RepCeiling),
                        DisplayOrder = 11,
                        Created =   DateTime.Now,
                        CreatedBy = "SYSTEM"
                    }).ToList();
                blocToAdd.Sets = setsToAdd;
                blocsToAdd.Add(blocToAdd);
            }
            sessionToAdd.SetBlocs = blocsToAdd;
            programme.Sessions.Add(sessionToAdd);
        }

        Context.WorkoutProgrammes.Add(programme);
        await Context.SaveChangesAsync();
        //TODO make a good DTO
        return new WorkoutProgramme();
    }
}