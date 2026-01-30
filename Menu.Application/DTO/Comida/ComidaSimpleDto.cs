namespace Menu.Application.DTO.Comida
{
    /// <summary>
    /// DTO simplificado de Comida (sin relaciones anidadas)
    /// Útil para evitar referencias circulares en respuestas JSON
    /// </summary>
    public class ComidaSimpleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Porcion { get; set; } = null!;
        public int CuantasPersonasComen { get; set; }
    }
}
