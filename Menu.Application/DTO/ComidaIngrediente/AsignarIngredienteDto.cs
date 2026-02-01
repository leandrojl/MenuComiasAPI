using System.ComponentModel.DataAnnotations;

namespace Menu.Application.DTO.ComidaIngrediente
{
    public class AsignarIngredienteDto
    {
        [Required(ErrorMessage = "El ID de la comida es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una comida válida")]
        public int ComidaId { get; set; }

        [Required(ErrorMessage = "El ID del ingrediente es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un ingrediente válido")]
        public int IngredienteId { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string? Descripcion { get; set; }
    }
}