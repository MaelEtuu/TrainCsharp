using Train.Models.Entity;

namespace Train.Models.Repository.Interfaces;

public interface ITrainRepository
{
    Task<IEnumerable<Train.Models.Entity.Train>> GetTrainsByCompagnieAsync(int idCompagnie);
    Task<IEnumerable<Train.Models.Entity.Train>> GetTrainsActifsAsync();
}

public interface ICompagnieRepository
{
    Task<Compagnie?> GetByEmailAsync(string email);
}