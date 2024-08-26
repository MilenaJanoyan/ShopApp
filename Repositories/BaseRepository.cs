using ShopApp.Data;
using ShopApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ShopApp.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly ShopDbContext _context;

    public BaseRepository(ShopDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            return await _context.Set<T>().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while fetching all entities: {ex.Message}", ex);
        }
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        try
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }
            return entity;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while fetching entity by ID: {ex.Message}", ex);
        }
    }

    public async Task<T> AddAsync(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        try
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while adding entity: {ex.Message}", ex);
        }
    }

    public async Task UpdateAsync(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        try
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while updating entity: {ex.Message}", ex);
        }
    }

    public async Task DeleteAsync(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        try
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while deleting entity: {ex.Message}", ex);
        }
    }
}
