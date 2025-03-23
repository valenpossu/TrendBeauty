using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Reporte
    {
        public string FechaVenta { get; set; }
        public string Producto { get; set; }
        public int CantidadVendida { get; set; }
        public decimal PrecioVenta { get; set; }
        public string Estado { get; set; }
        public int Descuento { get; set; }
       
    }
}
