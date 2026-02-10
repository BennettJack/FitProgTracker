using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.DTO.GymDTOs.ReturnDtos;
using fpt_backend.Data.Models;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Data.Models.GymModels.Dto;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.Services.GymServices;

public class WorkoutProgrammeService : BaseService<WorkoutProgramme>, IWorkoutProgrammeService
{
    private readonly IExerciseSetService _exerciseSetService;
    private readonly IExerciseSessionService _exerciseSessionService;
    private readonly ISetBlocService _setBlocService;

    public WorkoutProgrammeService(
        FptDbContext context,
        IExerciseSessionService exerciseSessionService,
        IExerciseSetService exerciseSetService,
        ISetBlocService setBlocService) : base(context)
    {
        _exerciseSessionService = exerciseSessionService;
        _setBlocService = setBlocService;
        _exerciseSetService = exerciseSetService;
    }

    
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
                            Description = set.Description,
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
        programme.Created = DateTime.Now;
        programme.CreatedBy = "SYSTEM";
        
        foreach (var session in req.Sessions)
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
            foreach (var bloc in session.SetBlocs)
            {
                var blocToAdd = AddBloc(bloc, sessionToAdd);
                var setsToAdd = bloc.Sets
                    .Select(set =>
                        AddSet(set, blocToAdd)
                        ).ToList();
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

    //new sets dont get added to existing sessions
    //
    public async Task<WorkoutProgrammeReturnDto?> UpdateTestAsync(WorkoutProgrammeCreateRequest req)
    {
        var programme = await Context.WorkoutProgrammes
            .Include(x => x.Sessions)
            .ThenInclude(x => x.SetBlocs)
            .ThenInclude(x => x.Sets)
            .FirstAsync(x => x.Id == req.Id);
        
        
        programme.Name = req.Name;
        programme.Description = req.Description;
        programme.Modified = DateTime.Now;
        
        
        //handle removed sessions
        var sessionsToRemove = ComparisonHelper<Session>.GetRemoved(
            programme.Sessions,
            req.Sessions.Select(id => id.Id).ToList());
        foreach (var removedSession in sessionsToRemove)
        {
            programme.Sessions.Remove(removedSession);
        }
        
        foreach (var session in req.Sessions)
        {
            //if session exists
            if (session.Id is not null)
            {
                var sessionRecord = programme.Sessions.Find(s => s.Id == session.Id);
                sessionRecord!.Name = session.Name;
                sessionRecord.DisplayOrder = session.DisplayOrder;
                sessionRecord.Modified = DateTime.Now;
                
                
                //handle setBloc removals
                var setBlocsToRemove = ComparisonHelper<SetBloc>.GetRemoved(
                    sessionRecord.SetBlocs,
                    session.SetBlocs.Select(id => id.Id).ToList());
                
                foreach (var removedSetBloc in setBlocsToRemove)
                {
                    sessionRecord.SetBlocs.Remove(removedSetBloc);
                }

                //handle each set bloc in request
                foreach (var setBloc in session.SetBlocs)
                {
                    //if setBloc exists
                    if (setBloc.Id is not null)
                    {
                        var setBlocRecord = sessionRecord.SetBlocs.Find(s => s.Id == setBloc.Id);
                        setBlocRecord!.DisplayOrder = setBloc.DisplayOrder;
                        setBlocRecord.Name = setBloc.Name;
                        setBlocRecord.Modified = DateTime.Now;
                        //handle set removals
                        var setsToRemove = ComparisonHelper<Set>.GetRemoved(
                            setBlocRecord.Sets,
                            setBloc.Sets.Select(id => id.Id).ToList());

                        foreach (var removedSet in setsToRemove)
                        {
                            setBlocRecord.Sets.Remove(removedSet);
                        }
                        foreach (var set in setBloc.Sets)
                        {


                            //handle existing set
                            if (set.Id is not null)
                            {
                                var setRecord =  setBlocRecord.Sets.Find(s => s.Id == set.Id);
                                setRecord!.Description = set.Description;
                                setRecord.DisplayOrder = set.DisplayOrder;
                                setRecord.Modified = DateTime.Now;
                                setRecord.RepFloor = set.RepFloor;
                                setRecord.RepCeiling = set.RepCeiling;
                            }
                            else
                            {
                                var setToAdd = AddSet(set, setBlocRecord);
                                Context.Sets.Add(setToAdd);
                                setBlocRecord.Sets.Add(setToAdd);
                            }
                        }
                        
                    }
                    //if setBloc does not exist
                    else
                    {
                        var blocToAdd = AddBloc(setBloc, sessionRecord);
                        var setsToAdd= setBloc.Sets
                            .Select(set =>
                                AddSet(set, blocToAdd)
                            ).ToList();
                        blocToAdd.Sets = setsToAdd;
                        sessionRecord.SetBlocs.Add(blocToAdd);
                        Context.SetBlocs.Add(blocToAdd);
                        Context.Sets.AddRange(setsToAdd);
                    }
   
                }
                
            }
            //if session does not exist
            else
            {
                var sessionToAdd = AddSession(session, programme);
                foreach (var setBloc in session.SetBlocs)
                {
                    var blocToAdd = AddBloc(setBloc, sessionToAdd);
                    var setsToAdd = setBloc.Sets
                        .Select(set =>
                            AddSet(set, blocToAdd)
                        ).ToList();
                    blocToAdd.Sets = setsToAdd;
                    Context.SetBlocs.Add(blocToAdd);
                    Context.Sets.AddRange(setsToAdd);
                }
                programme.Sessions.Add(sessionToAdd);
                Context.Sessions.Add(sessionToAdd);
            }

        }
        
        await Context.SaveChangesAsync();
        return await GetAsDtoAsync(programme.Id);
    }

    private Session AddSession
    (ExerciseSessionCreateRequest sessionReq,
        WorkoutProgramme programme)
    {
        var session = new Session()
        {
            Name = sessionReq.Name,
            WorkoutProgramme = programme,
            WorkoutProgrammeId = programme.Id,
            DisplayOrder = sessionReq.DisplayOrder,
            Created = DateTime.Now,
            CreatedBy = "SYSTEM"
        };
        return session;
    }
    
    private SetBloc AddBloc
        (ExerciseSetBlocCreateRequest blocReq,
            Session session)
    {
        var blocToAdd = new SetBloc()
        {
            Name =  blocReq.Name,
            Session =  session,
            SessionId = session.Id,
            DisplayOrder = blocReq.DisplayOrder,
            Created =  DateTime.Now,
            CreatedBy = "SYSTEM"
        };
        
        return blocToAdd;
    }

    private Set AddSet
    (ExerciseSetCreateRequest setReq,
        SetBloc setBloc)
    {
        return new Set
        {
            SetBloc = setBloc,
            SetBlocId = setBloc.Id,
            Description = setReq.Description,
            RepFloor = setReq.RepFloor,
            RepCeiling = setReq.RepCeiling,
            DisplayOrder = setReq.DisplayOrder,
            Created = DateTime.Now,
            CreatedBy = "SYSTEM"
        };
    }
    
}