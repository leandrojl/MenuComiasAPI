using Menu.Application.DTO.Comida;

namespace Menu.Application.DTO.Ingrediente
{
    /// <summary>
    /// DTO para Ingrediente que incluye las comidas que lo usan
    /// </summary>
    public class IngredienteWithComidasDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int CantidadComidas { get; set; }
        
        /// <summary>
        /// Lista de comidas que usan este ingrediente
        /// </summary>
        public List<ComidaSimpleDto> Comidas { get; set; } = new List<ComidaSimpleDto>();
    }
}
