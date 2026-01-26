using Menu.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Menu.Application.DTO.Comida;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MenuComidasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComidaController : ControllerBase
    {
    
        private readonly IComidaService _comidaService;

        public ComidaController(IComidaService comidaService)
        {
            _comidaService = comidaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComidaDto>>> GetAll()
        {
            var comidas = await _comidaService.GetAllAsync();
            return Ok(comidas);
        }

        // GET: api/Comida/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComidaDto>> GetById(int id)
        {
            var comida = await _comidaService.GetByIdAsync(id);
            
            if (comida == null)
                return NotFound();  // 404 si no existe
            
            return Ok(comida);      // 200 con la comida
        }

        // POST: api/Comida
        [HttpPost]
        public async Task<ActionResult<ComidaDto>> Create([FromBody] CreateComidaDto dto)
        {
            // Validar que el modelo sea válido
            if (!ModelState.IsValid)
                return BadRequest(ModelState);  // 400 con errores de validación
            
            var comidaCreada = await _comidaService.CreateAsync(dto);
            
            // 201 Created con la ubicación del nuevo recurso
            return CreatedAtAction(
                nameof(GetById),           // Nombre del método para obtener el recurso
                new { id = comidaCreada.Id },  // Parámetros de la ruta
                comidaCreada               // El objeto creado
            );
        }

        // PUT: api/Comida/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateComidaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            // Verificar que existe
            var existe = await _comidaService.ExistsAsync(id);
            if (!existe)
                return NotFound();
            
            await _comidaService.UpdateAsync(id, dto);
            
            return NoContent();  // 204 No Content (éxito sin cuerpo)
        }

        // DELETE: api/Comida/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existe = await _comidaService.ExistsAsync(id);
            if (!existe)
                return NotFound();
            
            await _comidaService.DeleteAsync(id);
            
            return NoContent();  // 204 No Content
        }
    }
}
