﻿using HomeSwitchHome.Services;
using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HomeSwitchHome.Areas.Administrador.Controllers
{
    public class AdministradorController : Controller
    {
        readonly IUsuarioService servicioUsuario;
        readonly ISubastaService servicioSubasta;
        readonly IPropiedadService servicioPropiedad;
        readonly IReservaService servicioReserva;

        public AdministradorController(IPropiedadService propiedadService, ISubastaService subastaService, IUsuarioService usuarioService, IReservaService reservaService)
        {
            this.servicioPropiedad = propiedadService;
            this.servicioSubasta = subastaService;
            this.servicioUsuario = usuarioService;
            this.servicioReserva = reservaService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Propiedades()
        {
            return Json(this.servicioPropiedad.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubastasSinEmpezar()
        {
            return Json(this.servicioSubasta.ObtenerSubastasFuturas().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubastasCerradas()
        {
            return Json(this.servicioSubasta.ObtenerSubastasFinalizadas().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult NuevosClientes()
        {
            return Json(this.servicioUsuario.ObtenerNuevosClientes().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClientesAPremium()
        {
            return Json(this.servicioUsuario.ObtenerSolicitudesPremium().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarPropiedad(string idpropiedad, string descripcion, string pais)
        {
            PROPIEDAD nuevaResidencia = new PROPIEDAD();

            nuevaResidencia.Descripcion = descripcion;
            nuevaResidencia.Pais = pais;

            if (this.servicioPropiedad.ActualizarPropiedad(nuevaResidencia, Int32.Parse(idpropiedad)))
                return Json(this.servicioPropiedad.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult BorrarPropiedad(string idpropiedad)
        {
            if (this.servicioPropiedad.RemoverPropiedad(Int32.Parse(idpropiedad)))
                return Json(this.servicioPropiedad.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);

            return null;

        }

        public JsonResult CrearPropiedad(string nombre, string domicilio, string descripcion, string pais)
        {
            PROPIEDAD nuevaResidencia = new PROPIEDAD();
            nuevaResidencia.Nombre = nombre;
            nuevaResidencia.Pais = pais;
            nuevaResidencia.Descripcion = descripcion;

            if (this.servicioPropiedad.CrearPropiedad(nuevaResidencia))
                return Json(this.servicioPropiedad.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult ObtenerInformacionPropiedad(int idPropiedad)
        {
            var propiedadesActuales = this.servicioPropiedad.ObtenerPropiedades();
            var currentPropiedad = propiedadesActuales.Where(t => t.IdPropiedad == idPropiedad).SingleOrDefault();
            return Json(currentPropiedad, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CrearSubasta(string propiedad, string fechaComienzo, string valorMinimo)
        {
            SUBASTA nuevaSubasta = new SUBASTA();
            nuevaSubasta.IdPropiedad = Int32.Parse(propiedad);
            nuevaSubasta.FechaComienzo = DateTime.Parse(fechaComienzo);
            nuevaSubasta.ValorMinimo = Convert.ToDecimal(valorMinimo);
            nuevaSubasta.Estado = "NUEVO";

            var mensaje = this.servicioSubasta.CrearSubasta(nuevaSubasta);

            if (mensaje != "OK")
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }

            return this.SubastasSinEmpezar();
            
        }

        public JsonResult ModificarSubasta(string idSubasta, string valorMinimo)
        {
            SUBASTA subastaActualizada = new SUBASTA();
            subastaActualizada.ValorMinimo = Convert.ToDecimal(valorMinimo);

            if (this.servicioSubasta.ActualizarSubasta(subastaActualizada, Int32.Parse(idSubasta)))
                return Json(this.servicioSubasta.ObtenerSubastasFuturas().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult BorrarSubasta(string idSubasta)
        {
            if (this.servicioSubasta.RemoverSubasta(Int32.Parse(idSubasta)))
                return Json(this.servicioSubasta.ObtenerSubastasFuturas().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult ObtenerInformacionSubasta(int idSubasta)
        {
            var subastasActuales = this.servicioSubasta.ObtenerSubastasFuturas();

            var currentSubasta = subastasActuales.Where(t => t.IdSubasta == idSubasta).SingleOrDefault();

            return Json(currentSubasta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AceptarPremium(int idCliente)
        {
            if (this.servicioUsuario.ConfirmarPremium(idCliente))
                return Json(this.servicioUsuario.ObtenerSolicitudesPremium().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult AceptarNuevoUsuario(int idCliente)
        {
            if (this.servicioUsuario.ConfirmarNuevoCliente(idCliente))
                return Json(this.servicioUsuario.ObtenerNuevosClientes().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult ObtenerListadoReservas()
        {
            var reservas = this.servicioReserva.ObtenerReservas();

            return Json(reservas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerReservasOrdenadasPorFecha() {

            List<ReservaViewModel> reservasOrdenadas = new List<ReservaViewModel>();

            var reservasCache = this.servicioReserva.ObtenerReservas();

            foreach (var reservaEnCache in reservasCache)
            {
                ReservaViewModel reservaAjustada = new ReservaViewModel();
                reservaAjustada.IdPropiedad = reservaEnCache.IdPropiedad;
                reservaAjustada.Propiedad = reservaEnCache.Propiedad;
                reservaAjustada.IdCliente = reservaEnCache.IdCliente;
                reservaAjustada.Cliente = reservaEnCache.Cliente;

                reservaAjustada.FechaReserva = string.Format("{0}-{1}-{2}", Convert.ToDateTime(reservaEnCache.FechaReserva).Day, Convert.ToDateTime(reservaEnCache.FechaReserva).Month, Convert.ToDateTime(reservaEnCache.FechaReserva).Year);

                reservasOrdenadas.Add(reservaAjustada);
            }

            return Json(reservasOrdenadas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerFechasOcupadasDePropiedad(int idPropiedad)
        {
            var reservasDePropiedad = this.servicioReserva.ObtenerReservasPropiedad(idPropiedad);

            var fechasReserva = reservasDePropiedad.Where(t => t.IdPropiedad == idPropiedad).Select(t => Convert.ToDateTime(t.FechaReserva).Date).ToArray();

            List<string> resultadoRangos = new List<string>();


            foreach (var comienzoReserva in fechasReserva)
            {
                for (int i = 0; i < 3; i++)
                {
                    var fechaActual = comienzoReserva.AddDays(i);
                    resultadoRangos.Add(string.Format("{0}-{1}-{2}", fechaActual.Year, fechaActual.Month, fechaActual.Day));
                }
            }

            return Json(resultadoRangos.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfirmarReservaDeSubasta(int idSubasta)
        {
            var subastaAceptada = this.servicioSubasta.ObtenerSubastasFinalizadas().Where(t => t.IdSubasta == idSubasta).SingleOrDefault();
            var reservaSubasta = new ReservaViewModel();
            reservaSubasta.IdCliente = Convert.ToInt32(subastaAceptada.IdCliente);
            reservaSubasta.IdPropiedad = subastaAceptada.IdPropiedad;
            reservaSubasta.FechaReserva = subastaAceptada.FechaComienzo;

            var mensaje = this.servicioReserva.AgregarReserva(reservaSubasta);

            if (mensaje != "OK") {
                return this.CancelarSubasta(idSubasta);
            }                
            else {
                this.servicioSubasta.ConfirmarSubasta(idSubasta);
                return this.ObtenerListadoReservas();
            }
        }

        public JsonResult CancelarSubasta(int idSubasta)
        {
            if (this.servicioSubasta.RemoverSubasta(idSubasta))
                return this.ObtenerListadoReservas();

            return null;
        }

        public JsonResult CancelarReservaDeCliente(int idReserva)
        {
            if (this.servicioReserva.CancelarReservaAdministrador(idReserva))
                return this.ObtenerListadoReservas();

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return null;
        }

        public JsonResult FiltrarSubastasPorFecha(string comienzo, string fin)
        {
            var subastas = this.servicioSubasta.ObtenerSubastasFuturas();

            var fechaComienzo = DateTime.ParseExact(comienzo, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var fechaFin = DateTime.ParseExact(fin, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);            

            return Json(subastas.Where(t => Convert.ToDateTime(t.FechaComienzo).CompareTo(fechaComienzo) > 0 && Convert.ToDateTime(t.FechaComienzo).CompareTo(fechaFin) < 0).ToArray(), JsonRequestBehavior.AllowGet);
        }

    }
}