namespace Menu.Application.DTO.Comida
{
    public class UpdateComidaDto
    {
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Porcion { get; set; } = null!;
        public int CuantasPersonasComen { get; set; }
        public int TipoComidaId { get; set; }
        
        // Opcional: Actualizar ingredientes asignados
        public List<int>? IngredientesIds { get; set; }
    }
}
