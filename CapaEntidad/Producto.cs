using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public Categoria oCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public string CostoTexto { get; set; } //recibimos el precio como texto para convertirlo a decimal de acuerdo a la region (Colombia)
        public string FechaCompra { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }
        public string RutaImagen { get; set; }
        public string NombreImagen { get; set; }
    }
}
