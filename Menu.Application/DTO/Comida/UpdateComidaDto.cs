using System.ComponentModel.DataAnnotations;

namespace Menu.Application.DTO.Comida
{
    public class UpdateComidaDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 200 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 99999.99, ErrorMessage = "El precio debe estar entre 0.01 y 99999.99")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La porción es obligatoria")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "La porción debe tener entre 1 y 50 caracteres")]
        public string Porcion { get; set; } = null!;

        [Range(1, 100, ErrorMessage = "La cantidad de personas debe estar entre 1 y 100")]
        public int CuantasPersonasComen { get; set; }

        [Required(ErrorMessage = "El tipo de comida es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de comida válido")]
        public int TipoComidaId { get; set; }

        public List<int>? IngredientesIds { get; set; }
    }
}