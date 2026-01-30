using Menu.Application.Interfaces;
using Menu.Application.DTO.ComidaIngrediente;
using Microsoft.AspNetCore.Mvc;

namespace MenuComidasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComidaIngredienteController : ControllerBase
    {
        private readonly IComidaIngredienteService _comidaIngredienteService;

        public ComidaIngredienteController(IComidaIngredienteService comidaIngredienteService)
        {
            _comidaIngredienteService = comidaIngredienteService;
        }

        // GET: api/ComidaIngrediente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComidaIngredienteDto>>> GetAll()
        {
            var relaciones = await _comidaIngredienteService.GetAllAsync();
            return Ok(relaciones);
        }

        // GET: api/ComidaIngrediente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComidaIngredienteDto>> GetById(int id)
        {
            var relacion = await _comidaIngredienteService.GetByIdAsync(id);
            
            if (relacion == null)
                return NotFound();
            
            return Ok(relacion);
        }

        // GET: api/ComidaIngrediente/comida/5
        [HttpGet("comida/{comidaId}")]
        public async Task<ActionResult<IEnumerable<ComidaIngredienteDto>>> GetByComidaId(int comidaId)
        {
            var relaciones = await _comidaIngredienteService.GetByComidaIdAsync(comidaId);
            return Ok(relaciones);
        }

        // GET: api/ComidaIngrediente/ingrediente/5
        [HttpGet("ingrediente/{ingredienteId}")]
        public async Task<ActionResult<IEnumerable<ComidaIngredienteDto>>> GetByIngredienteId(int ingredienteId)
        {
            var relaciones = await _comidaIngredienteService.GetByIngredienteIdAsync(ingredienteId);
            return Ok(relaciones);
        }

        // POST: api/ComidaIngrediente
        [HttpPost]
        public async Task<ActionResult<ComidaIngredienteDto>> AsignarIngrediente([FromBody] AsignarIngredienteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var relacion = await _comidaIngredienteService.AsignarIngredienteAsync(dto);
                
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = relacion.Id },
                    relacion
                );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/ComidaIngrediente/comida/5/ingredientes
        [HttpPost("comida/{comidaId}/ingredientes")]
        public async Task<ActionResult> AsignarMultiplesIngredientes(int comidaId, [FromBody] List<int> ingredienteIds)
        {
            if (ingredienteIds == null || !ingredienteIds.Any())
                return BadRequest("Debe proporcionar al menos un ingrediente.");
            
            try
            {
                await _comidaIngredienteService.AsignarMultiplesIngredientesAsync(comidaId, ingredienteIds);
                return Ok(new { message = $"Se asignaron {ingredienteIds.Count} ingredientes a la comida." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/ComidaIngrediente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateComidaIngredienteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var existe = await _comidaIngredienteService.ExistsAsync(id);
            if (!existe)
                return NotFound();
            
            await _comidaIngredienteService.UpdateAsync(id, dto);
            
            return NoContent();
        }

        // DELETE: api/ComidaIngrediente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existe = await _comidaIngredienteService.ExistsAsync(id);
            if (!existe)
                return NotFound();
            
            await _comidaIngredienteService.DeleteAsync(id);
            
            return NoContent();
        }

        // DELETE: api/ComidaIngrediente/comida/5/ingrediente/3
        [HttpDelete("comida/{comidaId}/ingrediente/{ingredienteId}")]
        public async Task<IActionResult> DesasignarIngrediente(int comidaId, int ingredienteId)
        {
            var existe = await _comidaIngredienteService.ExisteRelacionAsync(comidaId, ingredienteId);
            if (!existe)
                return NotFound("La relación entre comida e ingrediente no existe.");
            
            await _comidaIngredienteService.DesasignarIngredienteAsync(comidaId, ingredienteId);
            
            return NoContent();
        }

        // DELETE: api/ComidaIngrediente/comida/5/ingredientes
        [HttpDelete("comida/{comidaId}/ingredientes")]
        public async Task<IActionResult> DesasignarTodosIngredientes(int comidaId)
        {
            await _comidaIngredienteService.DesasignarTodosIngredientesAsync(comidaId);
            return NoContent();
        }
    }
}
