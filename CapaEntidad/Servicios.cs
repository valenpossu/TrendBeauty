using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Servicios
    {
        public int IdServicio { get; set; }
        public string NombreServicio { get; set; }
        public string Descripcion { get; set; }
        public decimal Valor { get; set; }
        public Categoria oCategoria { get; set; }
        public bool Activo { get; set; }
    }
}
