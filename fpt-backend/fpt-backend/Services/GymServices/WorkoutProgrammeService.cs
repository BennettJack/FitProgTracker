using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.Constants.GymConstants;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.DTO.GymDTOs.CreateRequests;
using fpt_backend.Data.DTO.GymDTOs.ReturnDtos;
using fpt_backend.Data.Models;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.Data.Models.GymModels.Dto;
using fpt_backend.Data.Models.GymModels.Instances;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;
using fpt_backend.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        ISetBlocService setBlocService,
        ICurrentUserService currentUserService
    )
        : base(context, currentUserService)
    {
        _exerciseSessionService = exerciseSessionService;
        _setBlocService = setBlocService;
        _exerciseSetService = exerciseSetService;
    }

    public WorkoutProgrammeReturnDto WorkoutProgrammeToDto(WorkoutProgramme programme)
    {
        return new WorkoutProgrammeReturnDto
        {
            Id = programme.Id,
            Name = programme.Name,
            Sessions = programme
                .Sessions.Select(s => new SessionReturnDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    DisplayOrder = s.DisplayOrder,
                    SetBlocs = s
                        .SetBlocs.Select(sb => new SetBlocReturnDto
                        {
                            Id = sb.Id,
                            DisplayOrder = sb.DisplayOrder,
                            Name = sb.Name,
                            ExerciseId = sb.ExerciseId,
                            ExerciseTypeId = sb.ExerciseType.Id,
                            Sets = sb
                                .Sets.Select(set => new SetReturnDto
                                {
                                    Id = set.Id,
                                    Description = set.Description,
                                    RepCeiling = set.RepCeiling,
                                    RepFloor = set.RepFloor,
                                    DisplayOrder = set.DisplayOrder,
                                })
                                .ToList(),
                        })
                        .ToList(),
                })
                .ToList(),
        };
    }

    public async Task<WorkoutProgrammeReturnDto?> GetAsDtoAsync(int id)
    {
        var programme = await Context
            .WorkoutProgrammes.Where(x => x.Id == id)
            .Select(x => new WorkoutProgrammeReturnDto
            {
                Id = x.Id,
                Name = x.Name,
                Sessions = x
                    .Sessions.Select(s => new SessionReturnDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        DisplayOrder = s.DisplayOrder,
                        SetBlocs = s
                            .SetBlocs.Select(sb => new SetBlocReturnDto
                            {
                                Id = sb.Id,
                                DisplayOrder = sb.DisplayOrder,
                                Name = sb.Name,
                                ExerciseId = sb.ExerciseId,
                                ExerciseTypeId = sb.ExerciseType.Id,
                                Sets = sb
                                    .Sets.Select(set => new SetReturnDto
                                    {
                                        Id = set.Id,
                                        Description = set.Description,
                                        RepCeiling = set.RepCeiling,
                                        RepFloor = set.RepFloor,
                                        DisplayOrder = set.DisplayOrder,
                                    })
                                    .ToList(),
                            })
                            .ToList(),
                    })
                    .ToList(),
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
        programme.CreatedBy = CurrentUserId;

        foreach (var session in req.Sessions)
        {
            var sessionToAdd = new Session()
            {
                Name = session.Name,
                WorkoutProgramme = programme,
                DisplayOrder = 9,
                Created = DateTime.Now,
                CreatedBy = CurrentUserId,
            };

            var blocsToAdd = new List<SetBloc>();
            foreach (var bloc in session.SetBlocs)
            {
                var blocToAdd = await AddBloc(bloc, sessionToAdd);
                var setsToAdd = bloc.Sets.Select(set => AddSet(set, blocToAdd)).ToList();
                blocToAdd.Sets = setsToAdd;
                blocsToAdd.Add(blocToAdd);
            }
            sessionToAdd.SetBlocs = blocsToAdd;
            programme.Sessions.Add(sessionToAdd);
        }

        Context.WorkoutProgrammes.Add(programme);
        await Context.SaveChangesAsync();

        return WorkoutProgrammeToDto(programme);
    }

    //
    public async Task<WorkoutProgrammeReturnDto?> UpdateTestAsync(WorkoutProgrammeCreateRequest req)
    {
        var programme = await Context
            .WorkoutProgrammes.Include(x => x.Sessions)
                .ThenInclude(x => x.SetBlocs)
                    .ThenInclude(x => x.Sets)
            .Include(x => x.Sessions)
                .ThenInclude(x => x.SetBlocs)
                    .ThenInclude(x => x.ExerciseType)
            .Where(x => x.CreatedBy == CurrentUserId)
            .FirstAsync(x => x.Id == req.Id);

        programme.Name = req.Name;
        programme.Description = req.Description;
        programme.Modified = DateTime.Now;

        var createdSessionsIdMap = new Dictionary<Session, string>();
        var createdSetBlocsIdMap = new Dictionary<SetBloc, string>();
        var createdSetsIdMap = new Dictionary<Set, string>();
        //handle removed sessions
        var sessionsToRemove = ComparisonHelper<Session>.GetRemoved(
            programme.Sessions,
            req.Sessions.Select(id => id.Id).ToList()
        );
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
                    session.SetBlocs.Select(id => id.Id).ToList()
                );

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
                        setBlocRecord.ExerciseId = setBloc.ExerciseId;
                        //handle set removals
                        var setsToRemove = ComparisonHelper<Set>.GetRemoved(
                            setBlocRecord.Sets,
                            setBloc.Sets.Select(id => id.Id).ToList()
                        );

                        foreach (var removedSet in setsToRemove)
                        {
                            setBlocRecord.Sets.Remove(removedSet);
                        }
                        foreach (var set in setBloc.Sets)
                        {
                            //handle existing set
                            if (set.Id is not null)
                            {
                                var setRecord = setBlocRecord.Sets.Find(s => s.Id == set.Id);
                                setRecord!.Description = set.Description;
                                setRecord.DisplayOrder = set.DisplayOrder;
                                setRecord.Modified = DateTime.Now;
                                setRecord.RepFloor = set.RepFloor;
                                setRecord.RepCeiling = set.RepCeiling;
                            }
                            else
                            {
                                var setToAdd = AddSet(set, setBlocRecord);
                                createdSetsIdMap[setToAdd] = set.TempId;
                                setBlocRecord.Sets.Add(setToAdd);
                            }
                        }
                    }
                    //if setBloc does not exist
                    else
                    {
                        var blocToAdd = await AddBloc(setBloc, sessionRecord);
                        var setsToAdd = setBloc.Sets.Select(set => AddSet(set, blocToAdd)).ToList();
                        blocToAdd.Sets = setsToAdd;
                        sessionRecord.SetBlocs.Add(blocToAdd);
                    }
                }
            }
            //if session does not exist
            else
            {
                var sessionToAdd = AddSession(session, programme);
                createdSessionsIdMap[sessionToAdd] = session.TempId;

                foreach (var setBloc in session.SetBlocs)
                {
                    var blocToAdd = await AddBloc(setBloc, sessionToAdd);
                    createdSetBlocsIdMap[blocToAdd] = setBloc.TempId;

                    foreach (var set in setBloc.Sets)
                    {
                        var setToAdd = AddSet(set, blocToAdd);
                        blocToAdd.Sets.Add(setToAdd);
                        createdSetsIdMap[setToAdd] = set.TempId;
                    }

                    sessionToAdd.SetBlocs.Add(blocToAdd);
                }

                programme.Sessions.Add(sessionToAdd);
            }
        }

        await Context.SaveChangesAsync();
        var dto = WorkoutProgrammeToDto(programme);
        var sessionsIdMap = createdSessionsIdMap.ToDictionary(x => x.Key.Id, x => x.Value);
        var setBlocsIdMap = createdSetBlocsIdMap.ToDictionary(x => x.Key.Id, x => x.Value);
        var setsIdMap = createdSetsIdMap.ToDictionary(x => x.Key.Id, x => x.Value);
        foreach (var session in dto.Sessions)
        {
            if (sessionsIdMap.TryGetValue(session.Id, out var sessionValue))
            {
                session.TempId = sessionValue;
            }

            foreach (var bloc in session.SetBlocs)
            {
                if (setBlocsIdMap.TryGetValue(bloc.Id, out var blocValue))
                {
                    bloc.TempId = blocValue;
                }

                foreach (var set in bloc.Sets)
                {
                    if (setsIdMap.TryGetValue(set.Id, out var setValue))
                    {
                        set.TempId = setValue;
                    }
                }
            }
        }

        return dto;
    }

    private Session AddSession(ExerciseSessionCreateRequest sessionReq, WorkoutProgramme programme)
    {
        var session = new Session()
        {
            Name = sessionReq.Name,
            WorkoutProgramme = programme,
            DisplayOrder = sessionReq.DisplayOrder,
            Created = DateTime.Now,
            CreatedBy = CurrentUserId,
        };
        return session;
    }

    private async Task<SetBloc> AddBloc(ExerciseSetBlocCreateRequest blocReq, Session session)
    {
        var blocToAdd = new SetBloc()
        {
            Name = blocReq.Name,
            Session = session,
            Exercise = await Context.Exercises.FindAsync(blocReq.ExerciseId),
            ExerciseType = await Context.ExerciseTypes.FindAsync(blocReq.ExerciseTypeId),
            DisplayOrder = blocReq.DisplayOrder,
            Created = DateTime.Now,
            CreatedBy = CurrentUserId,
        };

        return blocToAdd;
    }

    private Set AddSet(ExerciseSetCreateRequest setReq, SetBloc setBloc)
    {
        return new Set
        {
            SetBloc = setBloc,
            Description = setReq.Description,
            RepFloor = setReq.RepFloor,
            RepCeiling = setReq.RepCeiling,
            DisplayOrder = setReq.DisplayOrder,
            Created = DateTime.Now,
            CreatedBy = CurrentUserId,
        };
    }

    public async Task<WorkoutProgrammeReturnDto?> CreateProgrammeFromTemplate(int templateId)
    {
        var template = await Context
            .WorkoutProgrammeTemplates.Include(x => x.SessionTemplates)
                .ThenInclude(x => x.SetBlocTemplates)
                    .ThenInclude(x => x.SetTemplates)
            .Include(workoutProgrammeTemplate => workoutProgrammeTemplate.SessionTemplates)
                .ThenInclude(sessionTemplate => sessionTemplate.SetBlocTemplates)
                    .ThenInclude(setBlocTemplate => setBlocTemplate.Exercise)
            .FirstOrDefaultAsync(x => x.Id == templateId);

        if (template is null)
            return null;

        var programme = new WorkoutProgramme
        {
            WorkoutProgrammeTemplateID = template.Id,
            Name = template.Name,
            Description = template.Description,
            Created = DateTime.Now,
            CreatedBy = CurrentUserId,
            Sessions = template
                .SessionTemplates.Select(s => new Session
                {
                    Name = s.Name,
                    DisplayOrder = s.DisplayOrder,
                    Created = DateTime.Now,
                    CreatedBy = CurrentUserId,
                    SessionTemplateId = s.Id,
                    SetBlocs = s
                        .SetBlocTemplates.Select(sb => new SetBloc
                        {
                            Name = sb.Name,
                            DisplayOrder = sb.DisplayOrder,
                            ExerciseId = sb.Exercise.Id,
                            SetBlocTemplateId = sb.Id,
                            Sets = sb
                                .SetTemplates.Select(set => new Set
                                {
                                    SetTemplateId = set.Id,
                                    DisplayOrder = set.DisplayOrder,
                                    Created = DateTime.Now,
                                    Description = set.Description,
                                    RepFloor = set.RepFloor,
                                    RepCeiling = set.RepCeiling,
                                })
                                .ToList(),
                        })
                        .ToList(),
                })
                .ToList(),
        };
        await Context.WorkoutProgrammes.AddAsync(programme);
        await Context.SaveChangesAsync();
        return WorkoutProgrammeToDto(programme);
    }

    public async Task<List<WorkoutProgrammeReturnDto>> GetAllByUserIdAsync(string userId)
    {
        var workoutProgrammes = await Context
            .WorkoutProgrammes.Include(x => x.Sessions)
                .ThenInclude(x => x.SetBlocs)
                    .ThenInclude(x => x.Sets)
            .Include(x => x.Sessions)
                .ThenInclude(x => x.SetBlocs)
                    .ThenInclude(x => x.ExerciseType)
            .Where(x => x.CreatedBy == userId)
            .ToListAsync();
        return workoutProgrammes.Select(WorkoutProgrammeToDto).ToList();
    }
}
