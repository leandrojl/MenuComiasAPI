using Menu.Application.Interfaces;
using Menu.Application.DTO.Ingrediente;
using Microsoft.AspNetCore.Mvc;

namespace MenuComidasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredienteController : ControllerBase
    {
        private readonly IIngredienteService _ingredienteService;

        public IngredienteController(IIngredienteService ingredienteService)
        {
            _ingredienteService = ingredienteService;
        }

        // GET: api/Ingrediente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredienteDto>>> GetAll()
        {
            var ingredientes = await _ingredienteService.GetAllAsync();
            return Ok(ingredientes);
        }

        // GET: api/Ingrediente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredienteDto>> GetById(int id)
        {
            var ingrediente = await _ingredienteService.GetByIdAsync(id);
            
            if (ingrediente == null)
                return NotFound();
            
            return Ok(ingrediente);
        }

        // GET: api/Ingrediente/5/comidas
        [HttpGet("{id}/comidas")]
        public async Task<ActionResult<IngredienteWithComidasDto>> GetByIdWithComidas(int id)
        {
            var ingrediente = await _ingredienteService.GetByIdWithComidasAsync(id);
            
            if (ingrediente == null)
                return NotFound();
            
            return Ok(ingrediente);
        }

        // GET: api/Ingrediente/buscar?nombre=tomate
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<IngredienteDto>>> SearchByNombre([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return BadRequest("El parámetro 'nombre' es requerido.");
            
            var ingredientes = await _ingredienteService.SearchByNombreAsync(nombre);
            return Ok(ingredientes);
        }

        // POST: api/Ingrediente
        [HttpPost]
        public async Task<ActionResult<IngredienteDto>> Create([FromBody] CreateIngredienteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var ingredienteCreado = await _ingredienteService.CreateAsync(dto);
            
            return CreatedAtAction(
                nameof(GetById),
                new { id = ingredienteCreado.Id },
                ingredienteCreado
            );
        }

        // PUT: api/Ingrediente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateIngredienteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var existe = await _ingredienteService.ExistsAsync(id);
            if (!existe)
                return NotFound();
            
            await _ingredienteService.UpdateAsync(id, dto);
            
            return NoContent();
        }

        // DELETE: api/Ingrediente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existe = await _ingredienteService.ExistsAsync(id);
            if (!existe)
                return NotFound();
            
            // Verificar si está siendo usado en alguna comida
            var tieneComidas = await _ingredienteService.TieneComidasAsync(id);
            if (tieneComidas)
                return BadRequest("No se puede eliminar el ingrediente porque está siendo usado en una o más comidas.");
            
            await _ingredienteService.DeleteAsync(id);
            
            return NoContent();
        }
    }
}
