using Menu.Application.DTO.Comida;

namespace Menu.Application.DTO.TipoComida
{
    /// <summary>
    /// DTO para TipoComida que incluye sus comidas asociadas
    /// </summary>
    public class TipoComidaWithComidasDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int CantidadComidas { get; set; }
        
        /// <summary>
        /// Lista de comidas pertenecientes a este tipo
        /// </summary>
        public List<ComidaSimpleDto> Comidas { get; set; } = new List<ComidaSimpleDto>();
    }
}
