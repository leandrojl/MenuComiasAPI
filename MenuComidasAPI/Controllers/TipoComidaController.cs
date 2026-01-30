using Menu.Application.Interfaces;
using Menu.Application.DTO.TipoComida;
using Microsoft.AspNetCore.Mvc;

namespace MenuComidasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoComidaController : ControllerBase
    {
        private readonly ITipoComidaService _tipoComidaService;

        public TipoComidaController(ITipoComidaService tipoComidaService)
        {
            _tipoComidaService = tipoComidaService;
        }

        // GET: api/TipoComida
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoComidaDto>>> GetAll()
        {
            var tiposComida = await _tipoComidaService.GetAllAsync();
            return Ok(tiposComida);
        }

        // GET: api/TipoComida/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoComidaDto>> GetById(int id)
        {
            var tipoComida = await _tipoComidaService.GetByIdAsync(id);
            
            if (tipoComida == null)
                return NotFound();
            
            return Ok(tipoComida);
        }

        // GET: api/TipoComida/5/comidas
        [HttpGet("{id}/comidas")]
        public async Task<ActionResult<TipoComidaDto>> GetByIdWithComidas(int id)
        {
            var tipoComida = await _tipoComidaService.GetByIdWithComidasAsync(id);
            
            if (tipoComida == null)
                return NotFound();
            
            return Ok(tipoComida);
        }

        // POST: api/TipoComida
        [HttpPost]
        public async Task<ActionResult<TipoComidaDto>> Create([FromBody] CreateTipoComidaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var tipoComidaCreado = await _tipoComidaService.CreateAsync(dto);
            
            return CreatedAtAction(
                nameof(GetById),
                new { id = tipoComidaCreado.Id },
                tipoComidaCreado
            );
        }

        // PUT: api/TipoComida/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTipoComidaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var existe = await _tipoComidaService.ExistsAsync(id);
            if (!existe)
                return NotFound();
            
            await _tipoComidaService.UpdateAsync(id, dto);
            
            return NoContent();
        }

        // DELETE: api/TipoComida/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existe = await _tipoComidaService.ExistsAsync(id);
            if (!existe)
                return NotFound();
            
            // Opcional: Verificar si tiene comidas asociadas antes de eliminar
            var tieneComidas = await _tipoComidaService.TieneComidasAsync(id);
            if (tieneComidas)
                return BadRequest("No se puede eliminar el tipo de comida porque tiene comidas asociadas.");
            
            await _tipoComidaService.DeleteAsync(id);
            
            return NoContent();
        }
    }
}
