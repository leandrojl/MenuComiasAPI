namespace Menu.Application.DTO.ComidaIngrediente
{
    public class ComidaIngredienteDto
    {
        public int Id { get; set; }
        public int ComidaId { get; set; }
        public string ComidaNombre { get; set; } = null!;
        public int IngredienteId { get; set; }
        public string IngredienteNombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}
