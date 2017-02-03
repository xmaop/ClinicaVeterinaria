using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class TareaBE
    {
        public int    id_Tarea  { get; set; }
	    public string CodTarea	{ get; set; }
	    public string Usuario	{ get; set; }
	    public string Modalidad	{ get; set; }
	    public string FechaHoraRegistro { get; set; }
	    public string FechaHoraInicio  { get; set; }
	    public string FechaHoraFin { get; set; }
	    public string FechaHoraProgramada { get; set; }
        public string Estado { get; set; }

    }
}
