using Microsoft.EntityFrameworkCore;
using TrainComponentManagement.Domain.Interfaces;
using TrainComponentManagement.Domain.Entities;
using TrainComponentManagement.Infrastructure.Data;

namespace TrainComponentManagement.Infrastructure.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        private readonly AppDbContext _context;

        public ComponentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Component>> GetAllAsync(string? search)
        {
            var query = _context.Components.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(c => c.Name.Contains(search) || c.UniqueNumber.Contains(search));

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<Component?> GetByIdAsync(int id)
        {
            return await _context.Components.FindAsync(id);
        }

        public async Task<Component> AddAsync(Component component)
        {
            _context.Components.Add(component);
            await _context.SaveChangesAsync();
            return component;
        }

        public async Task<bool> UpdateQuantityAsync(int id, int quantity)
        {
            var component = await _context.Components.FindAsync(id);
            if (component == null || !component.CanAssignQuantity || quantity <= 0)
                return false;

            component.Quantity = quantity;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsByUniqueNumberAsync(string uniqueNumber)
        {
            return await _context.Components.AnyAsync(c => c.UniqueNumber == uniqueNumber);
        }
    }
}
