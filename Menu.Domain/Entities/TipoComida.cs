using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Domain.Entities
{
    public class TipoComida : IEntity
    {
            public int Id { get; set; }
            public string Nombre { get; set; } = null!;

            // Navegación
            public ICollection<Comida> Comidas { get; set; } = new List<Comida>();
        
    }
}
