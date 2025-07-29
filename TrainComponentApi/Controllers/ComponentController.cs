using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainComponentApi.Data;
using TrainComponentApi.DTOs;
using TrainComponentApi.Models;

namespace TrainComponentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ComponentController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComponentDto>>> GetComponents([FromQuery] string? search)
        {
            var query = _context.Components.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(c => c.Name.Contains(search) || c.UniqueNumber.Contains(search));

            var components = await query.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ComponentDto>>(components));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentDto>> GetComponent(int id)
        {
            var component = await _context.Components.FindAsync(id);
            if (component == null)
                return NotFound();

            return _mapper.Map<ComponentDto>(component);
        }

        [HttpPost]
        public async Task<ActionResult<ComponentDto>> CreateComponent(CreateComponentDto dto)
        {
            var component = _mapper.Map<Component>(dto);
            _context.Components.Add(component);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComponent), new { id = component.Id }, _mapper.Map<ComponentDto>(component));
        }

        [HttpPatch("{id}/quantity")]
        public async Task<IActionResult> UpdateQuantity(int id, UpdateQuantityDto dto)
        {
            var component = await _context.Components.FindAsync(id);
            if (component == null) return NotFound();

            if (!component.CanAssignQuantity)
                return BadRequest("This component cannot have a quantity assigned.");

            if (dto.Quantity <= 0)
                return BadRequest("Quantity must be a positive integer.");

            component.Quantity = dto.Quantity;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
