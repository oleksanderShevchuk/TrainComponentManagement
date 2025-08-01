using TrainComponentManagement.Application.DTOs;
using TrainComponentManagement.Domain.Entities;

namespace TrainComponentManagement.Application.Mappers
{
    public static class ComponentMapper
    {
        public static ComponentDto ToDto(Component entity)
        {
            return new ComponentDto
            {
                Id = entity.Id,
                Name = entity.Name,
                UniqueNumber = entity.UniqueNumber,
                CanAssignQuantity = entity.CanAssignQuantity,
                Quantity = entity.Quantity
            };
        }

        public static Component ToEntity(CreateComponentDto dto)
        {
            return new Component
            {
                Name = dto.Name,
                UniqueNumber = dto.UniqueNumber,
                CanAssignQuantity = dto.CanAssignQuantity,
                Quantity = dto.CanAssignQuantity ? 0 : null
            };
        }
    }
}
