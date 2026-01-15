using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Menu.Domain.Entities
{
    public class Comida
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Porcion { get; set; } = null!;
        public int CuantasPersonasComen { get; set; }

        // FK
        public int TipoComidaId { get; set; }

        // Navegación
        public TipoComida TipoComida { get; set; } = null!;

        //la coleccion es una relacion muchos a muchos con ComidaIngrediente
        public ICollection<ComidaIngrediente> ComidaIngredientes { get; set; } = new List<ComidaIngrediente>();
    }
}
