using TrainComponentApi.DTOs;

namespace TrainComponentApi.Services.Interfaces
{
    public interface IComponentService
    {
        Task<IEnumerable<ComponentDto>> GetComponentsAsync(string? search);
        Task<ComponentDto?> GetComponentByIdAsync(int id);
        Task<ComponentDto> CreateComponentAsync(CreateComponentDto dto);
        Task<bool> UpdateQuantityAsync(int id, int quantity);
    }
}
