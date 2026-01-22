using fpt_backend.Controllers;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories;
using fpt_backend.Helper_classes;
using fpt_backend.Services.GymServices.Interfaces;

namespace fpt_backend.Services.GymServices;

public class WorkoutProgramService : BaseService<WorkoutProgram>, IWorkoutProgramService
{
    public WorkoutProgramService(
        FptDbContext context) : base(context){}
}