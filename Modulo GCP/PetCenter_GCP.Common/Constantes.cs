using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.Common
{
    public class Constantes
    {
        public class Strings
        {
            public const string Null = "null";
            public const string Vacio = "";
        }

        public class Opciones
        {
            public const string RolNulo = "";
            public const int RolPadre = 0;
        }

        public class Resoluciones
        {
            public const string R1920X1080 = "1920X1080";
            public const string R1366X768 = "1366X768";
            public const string R1024X768 = "1024X768";
        }

        public class MenuOpciones
        {
            public const string MANTNOTIFICARCLIENTE = "MANTNOTIFICARCLIENTE";
            public const string COPIAINDICADOR = "COPIAINDICADOR";
            public const string CLONADOR = "CLONADOR";
        }

        public class MensajeOpciones
        {
            public const string ERRORSISTEMA = "Ocurrió un problema interno al procesar su acción, por favor vuelva a intentarlo.";
        }

        public class GenericProperties
        {
            public const string SuccessCode = "200";
            public const string Zero = "0";
            public const string Uno = "1";
        }

        public class EstadoRegistro
        {
            public const string Activo = "A";
            public const string Inactivo = "I";
        }

        public class EstadoOrden
        {
            public const string Generado = "GE";
            public const string Atendido = "AT";
            public const string Anulado = "AN";
        }

        public class Operaciones
        {
            public const string Delete = "ELIMINACION";
            public const string Update = "ACTUALIZACION";
            public const string Insert = "INSERCION";
            public const string Init = "INICIALIZACION";

            public const string ShortDelete = "DEL";
            public const string ShortUpdate = "UPD";
            public const string ShortInsert = "INS";
        }

        public class InputColors
        {
            public const string Obligatorio = "#FDE2E2";
            public const string Normal = "#FFFFFF";
            public const string Disabled = "#F0F0F0";
        }

        public class Parametro
        {
            public const int FechaLimitePeriodo = 1;
            public const int FechaLimiteIngNota = 2;
            public const int TiempoGraciaActuNota = 3;
            public const int FechaLimiteIngReg = 4;
        }

        public class KeysCode
        {
            public const int F9 = 120;
            public const int Enter = 13;
            public const int Escape = 27;
            public const int F2 = 113;
        }

        public class TipoCliente
        {
            public const string Natural = "1";
            public const string Juridico = "2";
        }

        public class TipoDocumento
        {
            public const string DNI = "DNI";
            public const string RUC = "RUC";
        }

        public class TipoParametro
        {
            public const string EMAIL = "EMAIL";
            public const string SMS = "SMS";
        }

        public class FileUploadOptions
        {
            public static string SizeLimitBytes
            {
                get
                {
                    return System.Configuration.ConfigurationManager.AppSettings["SIZE_LIMIT"];
                }
            }
        }

        public class Rutas
        {
            public static string RutaFilesTemp
            {
                get
                {
                    return System.Configuration.ConfigurationManager.AppSettings["rutaFilesTemp"].ToString();
                }
            }
        }

        public class ModalKeys
        {
            public const string CONSULTA = "VER";
        }

        public class TipoNotificacion
        {
            public const string Email = "Email";
            public const string Sms = "SMS";
        }
    }
}