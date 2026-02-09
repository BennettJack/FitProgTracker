using System.Linq.Expressions;
using fpt_backend.Data;
using fpt_backend.Data.DTO.GeneralDTOs;
using fpt_backend.Data.Models;
using fpt_backend.Data.Models.GymModels;
using fpt_backend.DbRepositories.Interfaces;
using fpt_backend.Helper_classes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace fpt_backend.DbRepositories;

public class BaseService<T> : IBaseService<T> where T : BaseModel
{
    protected readonly FptDbContext Context;
    protected readonly DbSet<T> DbSet;
    
    public BaseService(FptDbContext context)
    {
        Context = context;
        DbSet = Context.Set<T>();
    }
    
    public virtual async Task<List<T>> GetAllAsync()
    {
        var items = await DbSet.ToListAsync();
        return items;
    }

    //TODO Fix null
    public virtual async Task<T> GetByIdAsync(int id)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity == null)
        {
            return null;
        }

        return entity;
    }

    public virtual async Task<List<T>> GetByIdAsync(List<int> ids)
    {
        var entities = await DbSet.Where(e => ids.Contains(e.Id)).ToListAsync();
        return entities;
    }

    //TODO Fix null
    public virtual async Task<T> UpdateAsync(T entity)
    {
        var entityToUpdate = await DbSet.FindAsync(entity.Id);
        if (entityToUpdate == null)
        {
            return null;
        }
        Context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await Context.SaveChangesAsync();
        return entityToUpdate;
    }
    
    //TODO fix null
    public virtual async Task<T> DeleteAsync(T entity)
    {
        var entityToDelete = await DbSet.FindAsync(entity);
        if (entityToDelete == null)
        {
            return null;
        }
        
        DbSet.Remove(entityToDelete);
        return entity;
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        return entity;
    }

    public virtual async Task<List<T>> AddMultipleAsync(List<T> entities)
    {
        foreach (var entity in entities)
        {
            await DbSet.AddAsync(entity);
        }

        return entities;
    }

    //TODO fix null
    public virtual async Task<T> FindAsync(T entity)
    {
        var entityToFind = await DbSet.FindAsync(entity);
        if (entityToFind == null)
        {
            return null;
        }
        
        return entityToFind;
    }

    //TODO fix null
    public virtual async Task<List<DropdownReturnDto>> GetListAsDropdownAsync()
    {
        var entities = await DbSet.ToListAsync();
        var dropdownDtoList = new List<DropdownReturnDto>();

        foreach (var entity in entities)
        {
            dropdownDtoList.Add(new()
            {
                Value = entity.Id,
                Label = "",
            });
        }
        return  dropdownDtoList;
    }
}