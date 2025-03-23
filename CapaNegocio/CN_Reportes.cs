using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Reportes
    {
        //instancia de la capa datos donde estan los metodos; asi podemos acceder a todos sus metodos
        private CD_Reportes objCapaDato = new CD_Reportes();

        public Dashboard VerDashboard()
        {
            return objCapaDato.VerDashboard();
        }


        public List<Reporte> Ventas(string FechaInicio, string FechaFin)
        {
            return objCapaDato.Ventas(FechaInicio, FechaFin);
        }
    }
}
