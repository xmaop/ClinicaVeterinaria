using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACI_Business;
using ACI_Entities;
using ACIWeb.ViewModels;
using System.Runtime.Remoting.Activation;

namespace ACIWeb.Controllers
{
    public class OrdenImplantacionController : Controller
    {
        //
        // GET: /OrdenImplantacion/

        public ActionResult Consulta()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registrar(FormCollection collection)
        {
            int idOrdenAtencion = Convert.ToInt32(collection["IdOrdenAtencion"]);
            BOrdenAtencion ordenAtencion = new BOrdenAtencion();
            BChip bChip = new BChip();
            OrdenAtencion orden = new OrdenAtencion();
            Chip chip = new Chip();

            orden = ordenAtencion.ListarTodo().Where(d => d.IdOrdenAtencion == idOrdenAtencion).FirstOrDefault();

            ////////validad edad paciente
            int meses = CalcularMesesDeDiferencia(orden.Paciente.Fecha_Nacimiento, DateTime.Now.Date);
            if (meses > 3 && orden.Paciente.Raza.Descripcion.ToUpper() == "PERRO") {
                orden.Estado = "Rechazado";
                orden.MotivoRechazo = "Edad mínima no corresponde";
                orden.Observacion = "No corresponde a la edad establecida";
            }
            else if (meses > 5 && orden.Paciente.Raza.Descripcion.ToUpper() == "GATO")
            {
                orden.Estado = "Rechazado";
                orden.MotivoRechazo = "Edad mínima no corresponde";
                orden.Observacion = "No corresponde a la edad establecida";
            }
            else {
                orden.Estado = collection["EstadoAtencionModificacion"];
                orden.MotivoRechazo = collection["Motivo"];
                orden.Observacion = collection["Observaciones"];
                orden.DescripciónMotivoRechazo = collection["DescripcionMotivoRechazo"];
            }


            ////////actualiza chip
            chip = bChip.ListarTodo().Where(f => f.Id_chip == orden.Paciente.Chip.Id_chip).FirstOrDefault();
            chip.Estado = "En uso";
            try
            {
                ordenAtencion.Modificar(orden);
                bChip.Modificar(chip);
                return View(GetDatosRegistrar(idOrdenAtencion));
            }
            catch (Exception)
            {
                throw;
            }

            //return Json(new { success = true, responseText = "OK" }, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Registrar(int numOrden)
        {
            try
            {
                return View(GetDatosRegistrar(numOrden));
            }
            catch (Exception)
            {
                throw;
            }

            //ViewBag.ListaFechaCita = new SelectList(listfecha, "id", "name", 0);            
        }


        private OrdenAtencionPacienteCliente GetDatosRegistrar(int numOrden)
        {
            BOrdenAtencion ordenAtencion = new BOrdenAtencion();
            OrdenAtencion orden = null;
            orden = ordenAtencion.ListarTodo().Where(d => d.IdOrdenAtencion == numOrden).FirstOrDefault();
            OrdenAtencionPacienteCliente item = new OrdenAtencionPacienteCliente();

            try
            {

                DateTime fechanacimiento = orden.Paciente.Fecha_Nacimiento;
                DateTime fechahoy = DateTime.Now.Date;
                TimeSpan tiempoTranscurrido = fechahoy.Subtract(fechanacimiento);                

                item.IdOrdenAtencion = orden.IdOrdenAtencion;
                item.NumeroChip = orden.Paciente.Chip.Codigo_Chip;
                item.FechaRegistro = orden.Fecha.ToString("dd/MM/yyyy");
                item.EstadoAtencion = orden.Estado;
                item.CodigoCliente = orden.Paciente.Cliente.IdCliente;
                item.TipoCliente = orden.Paciente.Cliente.Tipo_Cliente;
                //item.NombreCliente = orden.Paciente.Cliente.Nom_Cliente + " " + orden.Paciente.Cliente.ApePat_Cliente + " " + orden.Paciente.Cliente.ApeMat_Cliente;
                item.NombreCliente = (item.TipoDocumentoCliente == "DNI" ? orden.Paciente.Cliente.Nom_Cliente + " " + orden.Paciente.Cliente.ApePat_Cliente + " " + orden.Paciente.Cliente.ApeMat_Cliente : orden.Paciente.Cliente.Razon_Social);
                item.TipoDocumentoCliente = orden.Paciente.Cliente.TipoDocumento_Identidad;
                item.NumeroDocumentoCliente = orden.Paciente.Cliente.Documento_Identidad;
                item.NombreContacto = orden.Paciente.Cliente.Nombre_Contacto + " " + orden.Paciente.Cliente.ApePat_Contacto + " " + orden.Paciente.Cliente.ApeMat_Contacto;
                item.TipoDocumentoContacto = orden.Paciente.Cliente.TipoDocIdent_Contacto;
                item.NumeroDocumentoContacto = orden.Paciente.Cliente.NroDocIdent_Contacto;
                item.CodigoPaciente = orden.Paciente.Id_Mascota;
                item.NombrePaciente = orden.Paciente.Nombre;
                item.FechaNacimiento = orden.Paciente.Fecha_Nacimiento.ToString("dd/MM/yyyy");
                item.TipoPaciente = orden.Paciente.Especie.Descripcion;
                item.RazaPaciente = orden.Paciente.Raza.Descripcion;

                item.NombrePaciente = orden.Paciente.Nombre;
                item.EdadPaciente = Convert.ToInt32(tiempoTranscurrido.Days / 365);

                item.Observaciones = orden.Observacion;
                item.Motivo = orden.MotivoRechazo;
                item.DescripcionMotivoRechazo = orden.DescripciónMotivoRechazo;
                PopulateEstados(item.EstadoAtencion, (item.Motivo == string.Empty ? "0" : item.Motivo));
                return item;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetImplantacion(string numOrden)
        {
            int idorden = 0;
            idorden = (numOrden == string.Empty ? -1 : Convert.ToInt32(numOrden));
            BOrdenAtencion ordenAtencion = new BOrdenAtencion();
            List<OrdenAtencion> listOrdenAtencion = null;
            List<OrdenAtencionPacienteCliente> listOrdenAtencionPacienteCliente = new List<OrdenAtencionPacienteCliente>();

            try
            {
                listOrdenAtencion = ordenAtencion.ListarTodo().Where(d => d.IdOrdenAtencion == (idorden == -1 ? d.IdOrdenAtencion : idorden) && (d.Estado.Equals("Listo para implantación") || d.Estado.Equals("Iniciar implantación"))).ToList();

                foreach (OrdenAtencion orden in listOrdenAtencion)
                {
                    DateTime fechanacimiento = orden.Paciente.Fecha_Nacimiento;
                    DateTime fechahoy = DateTime.Now.Date;
                    TimeSpan tiempoTranscurrido = fechahoy.Subtract(fechanacimiento);
                    decimal tiempo = tiempoTranscurrido.Days / 365;

                    OrdenAtencionPacienteCliente item = new OrdenAtencionPacienteCliente();
                    item.IdOrdenAtencion = orden.IdOrdenAtencion;
                    item.NombrePaciente = orden.Paciente.Nombre;
                    item.EdadPaciente = Convert.ToInt32(Math.Round(tiempo, 0, MidpointRounding.ToEven));
                    item.TipoDocumentoCliente = orden.Paciente.Cliente.TipoDocumento_Identidad;
                    item.NumeroDocumentoCliente = orden.Paciente.Cliente.Documento_Identidad;
                    item.NombreCliente = (item.TipoDocumentoCliente == "DNI" ? orden.Paciente.Cliente.Nom_Cliente + " " + orden.Paciente.Cliente.ApePat_Cliente + " " + orden.Paciente.Cliente.ApeMat_Cliente : orden.Paciente.Cliente.Razon_Social);
                    item.EstadoAtencion = orden.Estado;
                    listOrdenAtencionPacienteCliente.Add(item);
                }
                return Json(listOrdenAtencionPacienteCliente, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void PopulateEstados(string estado, string motivo)
        {
            try
            {
                List<SelectListItem> estados = new List<SelectListItem>();
                estados.Add(new SelectListItem() { Text = "Listo para implantación", Value = "Listo para implantación" });
                estados.Add(new SelectListItem() { Text = "Iniciar implantación", Value = "Iniciar implantación" });
                estados.Add(new SelectListItem() { Text = "Implantado", Value = "Implantado" });
                estados.Add(new SelectListItem() { Text = "Rechazado", Value = "Rechazado" });

                ViewBag.ListaEstados = new SelectList(estados, "Value", "Text", estado);

                List<SelectListItem> motivos = new List<SelectListItem>();
                motivos.Add(new SelectListItem() { Text = "Edad mínima no corresponde", Value = "Edad mínima no corresponde" });
                motivos.Add(new SelectListItem() { Text = "Problemas físicos", Value = "Problemas físicos" });
                motivos.Add(new SelectListItem() { Text = "Dueño no autoriza", Value = "Dueño no autoriza" });
                motivos.Add(new SelectListItem() { Text = "Otros", Value = "Otros" });

                ViewBag.ListaMotivos = new SelectList(motivos, "Value", "Text", motivo);


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
        {
            return Math.Abs((fechaDesde.Month - fechaHasta.Month) + 12 * (fechaDesde.Year - fechaHasta.Year));
        }

    }
}
