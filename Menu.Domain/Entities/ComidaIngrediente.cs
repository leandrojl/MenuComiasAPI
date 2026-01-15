using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Domain.Entities
{
    public class ComidaIngrediente
    {
        public int Id { get; set; }

        // FKs
        public int ComidaId { get; set; }
        public int IngredienteId { get; set; }

        public string? Descripcion { get; set; }

        // Navegación
        public Comida Comida { get; set; } = null!;
        public Ingrediente Ingrediente { get; set; } = null!;
    }
}
