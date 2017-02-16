using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.Entity
{
    public class PacienteEntity
    {
        public int id_Paciente { get; set; }
        public string nombre { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string sexo { get; set; }
        public string rutaImagen { get; set; }
        public string id_Foto { get; set; }
        public decimal peso { get; set; }
        public int id_Cliente { get; set; }
        public string codigo { get; set; }
        public int id_Chip { get; set; }
        public int id_Raza { get; set; }
        public int id_Especie { get; set; }
        public string comentario { get; set; }
        public string estado { get; set; }

        //---
        public string descSexo { get; set; }
        public string nomRaza { get; set; }
        public string nomEspecie { get; set; }
        public string nomCliente { get; set; }
        public string codigoCliente { get; set; }
        public string rutaImagenTemp { get; set; }
        //---
    }
}