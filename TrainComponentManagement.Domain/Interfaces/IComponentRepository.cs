using TrainComponentManagement.Domain.Models;

namespace TrainComponentManagement.Domain.Interfaces
{
    public interface IComponentRepository
    {
        Task<IEnumerable<Component>> GetAllAsync(string? search);
        Task<Component?> GetByIdAsync(int id);
        Task<Component> AddAsync(Component component);
        Task<bool> UpdateQuantityAsync(int id, int quantity);
        Task<bool> ExistsByUniqueNumberAsync(string uniqueNumber);
    }
}