using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Domain.Entities
{
    public class Ingrediente : IEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        // Navegación
        public ICollection<ComidaIngrediente> ComidaIngredientes { get; set; } = new List<ComidaIngrediente>();
    }
}
