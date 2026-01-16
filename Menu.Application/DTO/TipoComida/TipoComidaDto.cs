namespace Menu.Application.DTO.TipoComida
{
    public class TipoComidaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        
        // Opcional: Cantidad de comidas de este tipo
        public int CantidadComidas { get; set; }
    }
}
