namespace Menu.Application.DTO.Comida
{
    public class CreateComidaDto
    {
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Porcion { get; set; } = null!;
        public int CuantasPersonasComen { get; set; }
        public int TipoComidaId { get; set; }
        
        // Opcional: Lista de IDs de ingredientes para asignar al crear
        public List<int>? IngredientesIds { get; set; }
    }
}
