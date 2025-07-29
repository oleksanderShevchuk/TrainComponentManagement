using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainComponentApi.Data;
using TrainComponentApi.DTOs;
using TrainComponentApi.Models;
using TrainComponentApi.Responses;

namespace TrainComponentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ComponentController> _logger;

        public ComponentController(AppDbContext context, IMapper mapper, ILogger<ComponentController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ComponentDto>>>> GetComponents([FromQuery] string? search)
        {
            _logger.LogInformation("Fetching components with search term: {Search}", search);

            var query = _context.Components.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(c => c.Name.Contains(search) || c.UniqueNumber.Contains(search));

            var components = await query.ToListAsync();
            var result = _mapper.Map<IEnumerable<ComponentDto>>(components);

            return Ok(ApiResponse<IEnumerable<ComponentDto>>.Ok(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ComponentDto>>> GetComponent(int id)
        {
            var component = await _context.Components.FindAsync(id);
            if (component == null)
            {
                _logger.LogWarning("Component with id {Id} not found", id);
                return NotFound(ApiResponse<ComponentDto>.Fail($"Component with id {id} not found"));
            }

            return Ok(ApiResponse<ComponentDto>.Ok(_mapper.Map<ComponentDto>(component)));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ComponentDto>>> CreateComponent(CreateComponentDto dto)
        {
            if (await _context.Components.AnyAsync(c => c.UniqueNumber == dto.UniqueNumber))
                return BadRequest(ApiResponse<ComponentDto>.Fail("Component with this UniqueNumber already exists."));

            var component = _mapper.Map<Component>(dto);
            _context.Components.Add(component);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<ComponentDto>(component);

            return CreatedAtAction(nameof(GetComponent), new { id = component.Id },
                ApiResponse<ComponentDto>.Ok(resultDto, "Component created successfully"));
        }

        [HttpPatch("{id}/quantity")]
        public async Task<ActionResult<ApiResponse<string>>> UpdateQuantity(int id, UpdateQuantityDto dto)
        {
            var component = await _context.Components.FindAsync(id);
            if (component == null)
                return NotFound(ApiResponse<string>.Fail($"Component with id {id} not found"));

            if (!component.CanAssignQuantity)
                return BadRequest(ApiResponse<string>.Fail("This component cannot have a quantity assigned."));

            if (dto.Quantity <= 0)
                return BadRequest(ApiResponse<string>.Fail("Quantity must be a positive integer."));

            component.Quantity = dto.Quantity;
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<string>.Ok("Quantity updated successfully"));
        }
    }
}
