using TrainComponentManagement.Application.DTOs;

namespace TrainComponentManagement.Application.Interfaces
{
    public interface IComponentService
    {
        /// <summary>
        /// Get all components, optionally filtered by search term (Name or UniqueNumber)
        /// </summary>
        Task<IEnumerable<ComponentDto>> GetAllAsync(string? search);

        /// <summary>
        /// Get single component by Id
        /// </summary>
        Task<ComponentDto?> GetByIdAsync(int id);

        /// <summary>
        /// Create a new component
        /// </summary>
        Task<ComponentDto> CreateAsync(CreateComponentDto dto);

        /// <summary>
        /// Update component quantity (returns false if invalid)
        /// </summary>
        Task<bool> UpdateQuantityAsync(int id, int quantity);
    }
}
