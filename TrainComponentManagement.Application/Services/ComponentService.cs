using TrainComponentManagement.Application.DTOs;
using TrainComponentManagement.Application.Interfaces;
using TrainComponentManagement.Application.Mappers;
using TrainComponentManagement.Domain.Interfaces;

namespace TrainComponentManagement.Application.Services
{
    public class ComponentService : IComponentService
    {
        private readonly IComponentRepository _repository;

        public ComponentService(IComponentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ComponentDto>> GetAllAsync(string? search)
        {
            var components = await _repository.GetAllAsync(search);
            return components.Select(ComponentMapper.ToDto);
        }

        public async Task<ComponentDto?> GetByIdAsync(int id)
        {
            var component = await _repository.GetByIdAsync(id);
            return component == null ? null : ComponentMapper.ToDto(component);
        }

        public async Task<ComponentDto> CreateAsync(CreateComponentDto dto)
        {
            if (await _repository.ExistsByUniqueNumberAsync(dto.UniqueNumber))
                throw new InvalidOperationException("Component with this UniqueNumber already exists.");

            var entity = ComponentMapper.ToEntity(dto);
            var created = await _repository.AddAsync(entity);
            return ComponentMapper.ToDto(created);
        }

        public async Task<bool> UpdateQuantityAsync(int id, int quantity)
        {
            return await _repository.UpdateQuantityAsync(id, quantity);
        }
    }
}
