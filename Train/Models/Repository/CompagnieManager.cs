using Microsoft.EntityFrameworkCore;
using Train.Models.Entity;
using Train.Models.Repository;
using Train.Models.Repository.Interfaces;

namespace Train.Models.Repository.Managers;

public class CompagnieManager : BaseManager<Compagnie, string>, ICompagnieRepository
{
    public CompagnieManager(TrainDbContext context) : base(context)
    {
    }

    private IQueryable<Compagnie> ApplyIncludes()
    {
        return context.Set<Compagnie>()
            .Include(c => c.Trains);
    }

    public override async Task<IEnumerable<Compagnie>> GetAllAsync()
    {
        return await ApplyIncludes()
            .OrderBy(c => c.Nom)
            .ToListAsync();
    }

    public override async Task<Compagnie?> GetByIdAsync(int id)
    {
        return await ApplyIncludes()
            .FirstOrDefaultAsync(c => c.IdCompagnie == id);
    }

    public override async Task<Compagnie?> GetByNameAsync(string name)
    {
        return await ApplyIncludes()
            .FirstOrDefaultAsync(c => c.Nom.ToLower() == name.ToLower());
    }

    public async Task<Compagnie?> GetByEmailAsync(string email)
    {
        return await ApplyIncludes()
            .FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower());
    }
}