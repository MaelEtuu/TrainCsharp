using Microsoft.EntityFrameworkCore;
using Train.Models.Entity;
using Train.Models.Repository;
using Train.Models.Repository.Interfaces;

namespace Train.Models.Repository.Managers;

public class TrainManager : BaseManager<Train.Models.Entity.Train, string>, ITrainRepository
{
    public TrainManager(TrainDbContext context) : base(context)
    {
    }

    private IQueryable<Train.Models.Entity.Train> ApplyIncludes()
    {
        return context.Set<Train.Models.Entity.Train>()
            .Include(t => t.CompagnieNav)
            .Include(t => t.Voyages);
    }

    public override async Task<IEnumerable<Train.Models.Entity.Train>> GetAllAsync()
    {
        return await ApplyIncludes()
            .OrderBy(t => t.Nom)
            .ToListAsync();
    }

    public override async Task<Train.Models.Entity.Train?> GetByIdAsync(int id)
    {
        return await ApplyIncludes()
            .FirstOrDefaultAsync(t => t.IdTrain == id);
    }

    public override async Task<Train.Models.Entity.Train?> GetByNameAsync(string name)
    {
        return await ApplyIncludes()
            .FirstOrDefaultAsync(t => t.Nom.ToLower() == name.ToLower());
    }

    public async Task<IEnumerable<Train.Models.Entity.Train>> GetTrainsByCompagnieAsync(int idCompagnie)
    {
        return await ApplyIncludes()
            .Where(t => t.IdCompagnie == idCompagnie)
            .OrderBy(t => t.Nom)
            .ToListAsync();
    }

    public async Task<IEnumerable<Train.Models.Entity.Train>> GetTrainsActifsAsync()
    {
        return await ApplyIncludes()
            .Where(t => t.EstActif)
            .OrderBy(t => t.Nom)
            .ToListAsync();
    }
}