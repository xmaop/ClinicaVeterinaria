using System.Runtime.Serialization;
namespace PETCenter.Entities.Seguridad
{
    [DataContract]
    public class Option
    {
        [DataMember]
        public int Codigo { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Ruta { get; set; }
        [DataMember]
        public int CodigoPadre { get; set; }
        [DataMember]
        public string Nivel { get; set; }
        [DataMember]
        public int TipoApertura { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string TipoRuta { get; set; }
        [DataMember]
        public string Abreviatura { get; set; }
    }
}
