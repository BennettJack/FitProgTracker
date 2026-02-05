using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.DTO.GymDTOs.ReturnDtos;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Data.Models.GymModels.Dto;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.Services.GymServices;

public class WorkoutProgrammeService : BaseService<WorkoutProgramme>, IWorkoutProgrammeService
{
    public WorkoutProgrammeService(
        FptDbContext context) : base(context){}

    public async Task<WorkoutProgrammeReturnDto?> GetAsDtoAsync(int id)
    {
        var programme = await Context.WorkoutProgrammes
            .Where(x => x.Id == id)
            .Select(x => new WorkoutProgrammeReturnDto
            {
                Id = x.Id,
                Name = x.Name,
                Sessions = x.Sessions.Select(s => new SessionReturnDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    DisplayOrder = s.DisplayOrder,
                    SetBlocs = s.SetBlocs.Select(sb => new SetBlocReturnDto
                    {
                        Id = sb.Id,
                        DisplayOrder = sb.DisplayOrder,
                        Name = sb.Name,
                        Sets = sb.Sets.Select(set => new SetReturnDto
                        {
                            Id = set.Id,
                            RepCeiling = set.RepCeiling,
                            RepFloor = set.RepFloor,
                            DisplayOrder = set.DisplayOrder,
                        }).ToList()
                    }).ToList()
                }).ToList()
            })
            .FirstOrDefaultAsync();
        return programme;
    }
    
    public async Task<WorkoutProgrammeReturnDto?> AddAsync(WorkoutProgrammeCreateRequest req)
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
        
        return await GetAsDtoAsync(programme.Id);
    }
}