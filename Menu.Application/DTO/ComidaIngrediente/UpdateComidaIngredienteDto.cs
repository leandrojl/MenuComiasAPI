using System.ComponentModel.DataAnnotations;

namespace Menu.Application.DTO.ComidaIngrediente
{
    public class UpdateComidaIngredienteDto
    {
        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string? Descripcion { get; set; }
    }
}