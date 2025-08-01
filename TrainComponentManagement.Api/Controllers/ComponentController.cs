using Microsoft.AspNetCore.Mvc;
using TrainComponentManagement.Application.DTOs;
using TrainComponentManagement.Application.Interfaces;
using TrainComponentManagement.Application.Responses;
using TrainComponentManagement.Domain.Interfaces;

namespace TrainComponentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentsController : ControllerBase
    {
        private readonly IComponentService _service;
        private readonly ILogger<ComponentsController> _logger;

        public ComponentsController(IComponentService service, ILogger<ComponentsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ComponentDto>>>> GetComponents([FromQuery] string? search)
        {
            _logger.LogInformation("Fetching components with search term: {Search}", search);
            var components = await _service.GetAllAsync(search);
            return Ok(ApiResponse<IEnumerable<ComponentDto>>.Ok(components));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ComponentDto>>> GetComponent(int id)
        {
            var component = await _service.GetByIdAsync(id);

            if (component == null)
            {
                _logger.LogWarning("Component with id {Id} not found", id);
                return NotFound(ApiResponse<ComponentDto>.Fail($"Component with id {id} not found"));
            }

            return Ok(ApiResponse<ComponentDto>.Ok(component));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ComponentDto>>> CreateComponent(CreateComponentDto dto)
        {
            var createdComponent = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetComponent), new { id = createdComponent.Id },
                ApiResponse<ComponentDto>.Ok(createdComponent, "Component created successfully"));
        }

        [HttpPatch("{id}/quantity")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateQuantity(int id, UpdateQuantityDto dto)
        {
            if (dto.Quantity <= 0)
                return BadRequest(ApiResponse<string>.Fail("Quantity must be a positive integer."));

            var result = await _service.UpdateQuantityAsync(id, dto.Quantity);

            if (!result)
                return BadRequest(ApiResponse<string>.Fail("Failed to update quantity. Check ID or component rules."));

            return Ok(ApiResponse<string>.Ok("Quantity updated successfully"));
        }
    }
}
