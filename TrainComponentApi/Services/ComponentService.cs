using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainComponentApi.Data;
using TrainComponentApi.DTOs;
using TrainComponentApi.Models;
using TrainComponentApi.Services.Interfaces;

namespace TrainComponentApi.Services
{
    public class ComponentService : IComponentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ComponentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComponentDto>> GetComponentsAsync(string? search)
        {
            var query = _context.Components.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(c => c.Name.Contains(search) || c.UniqueNumber.Contains(search));

            return _mapper.Map<IEnumerable<ComponentDto>>(await query.ToListAsync());
        }

        public async Task<ComponentDto?> GetComponentByIdAsync(int id)
        {
            var component = await _context.Components.FindAsync(id);
            return component == null ? null : _mapper.Map<ComponentDto>(component);
        }

        public async Task<ComponentDto> CreateComponentAsync(CreateComponentDto dto)
        {
            var component = _mapper.Map<Component>(dto);
            _context.Components.Add(component);
            await _context.SaveChangesAsync();
            return _mapper.Map<ComponentDto>(component);
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
    }
}
