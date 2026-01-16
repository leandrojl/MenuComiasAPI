using Menu.Application.DTO.Ingrediente;

namespace Menu.Application.DTO.Comida
{
    public class ComidaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Porcion { get; set; }
        public int CuantasPersonasComen { get; set; }
        public int TipoComidaId { get; set; }
        public string TipoComidaNombre { get; set; }
        public List<IngredienteDto> Ingredientes { get; set; }
    }
}
