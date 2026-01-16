namespace Menu.Application.DTO.Ingrediente
{
    public class IngredienteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        
        // Opcional: Cuántas comidas usan este ingrediente
        public int CantidadComidas { get; set; }
    }
}
