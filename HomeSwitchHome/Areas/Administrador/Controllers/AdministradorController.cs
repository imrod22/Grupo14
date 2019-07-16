using HomeSwitchHome.Services;
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
        readonly IPujaService servicioPuja;
        readonly IMailService servicioMail;
        readonly ICreditoService servicioCredito;
        readonly IHotsaleService servicioHotSale;

        public AdministradorController(IPropiedadService propiedadService, ISubastaService subastaService, IUsuarioService usuarioService, IReservaService reservaService, IPujaService pujaService, IMailService mailService, ICreditoService creditoService, IHotsaleService hotsaleService)
        {
            this.servicioPropiedad = propiedadService;
            this.servicioSubasta = subastaService;
            this.servicioUsuario = usuarioService;
            this.servicioReserva = reservaService;
            this.servicioPuja = pujaService;
            this.servicioMail = mailService;
            this.servicioCredito = creditoService;
            this.servicioHotSale = hotsaleService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Propiedades()
        {
            return Json(this.servicioPropiedad.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerTodasLasPropiedades()
        {
            return Json(this.servicioPropiedad.ObtenerTodasLasPropiedades().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubastasSinEmpezar()
        {
            var subastasFuturas = this.servicioSubasta.ObtenerSubastasFuturas();

            return Json(subastasFuturas.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubastasCerradas()
        {
            var subastasFinalizadas = this.servicioSubasta.ObtenerSubastasFinalizadas();

            return Json(subastasFinalizadas.ToArray(), JsonRequestBehavior.AllowGet);
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

        public JsonResult ActualizarEstatusPropiedad(string idpropiedad)
        {
            if (this.servicioPropiedad.CambiarEstatusPropiedad(Int32.Parse(idpropiedad)))
                return Json(this.servicioPropiedad.ObtenerTodasLasPropiedades().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult EliminarPropiedad(string idpropiedad)
        {
            if (this.servicioPropiedad.BorrarPropiedad(Int32.Parse(idpropiedad)))
                return Json(this.servicioPropiedad.ObtenerTodasLasPropiedades().ToArray(), JsonRequestBehavior.AllowGet);

            return null;

        }

        public JsonResult CrearPropiedad(string nombre, string ciudad, string descripcion, string pais)
        {
            PROPIEDAD nuevaResidencia = new PROPIEDAD();
            nuevaResidencia.Nombre = nombre;
            nuevaResidencia.Pais = pais;
            nuevaResidencia.Ciudad = ciudad;
            nuevaResidencia.Descripcion = descripcion;
            nuevaResidencia.Activa = true;

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

        public JsonResult CrearSubasta(string propiedad, string valorMinimo, string fechaComienzo, string fechaReserva)
        {
            SUBASTA nuevaSubasta = new SUBASTA();
            nuevaSubasta.IdPropiedad = Int32.Parse(propiedad);
            nuevaSubasta.FechaComienzo = DateTime.Parse(fechaComienzo);
            nuevaSubasta.FechaReserva = DateTime.Parse(fechaReserva);
            nuevaSubasta.ValorMinimo = Convert.ToDecimal(valorMinimo);
            nuevaSubasta.Estado = "NUEVO";

            var mensaje = this.servicioSubasta.CrearSubasta(nuevaSubasta);

            if (mensaje != "OK")
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }

            var clientesNotificar = this.servicioPropiedad.ObtenerNotificaciones(nuevaSubasta.IdPropiedad);
            List<string> clientes = new List<string>();
            var subastaModel = new SubastaViewModel().ToViewModel(nuevaSubasta);
            subastaModel.Propiedad = this.servicioPropiedad.ObtenerPropiedades().Where(t => t.IdPropiedad == subastaModel.IdPropiedad).SingleOrDefault();

            foreach (var notificacion in clientesNotificar)
                clientes.Add(notificacion.CLIENTE.Email);


            this.servicioMail.EnviarNotificacionNuevaSubasta(clientes, subastaModel);
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
            var mensaje = this.servicioUsuario.ConfirmarPremium(idCliente);

            if (mensaje != "OK")
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var nuevoPremium = this.servicioUsuario.ObtenerInformacionCliente(idCliente);
                this.servicioMail.EnviarPremiumAceptadoMail(nuevoPremium);

                return Json(this.servicioUsuario.ObtenerSolicitudesPremium().ToArray(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AceptarNuevoUsuario(int idCliente)
        {
            var mensaje = this.servicioUsuario.ConfirmarNuevoCliente(idCliente);

            if (mensaje != "OK")
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var nuevoUsuario = this.servicioUsuario.ObtenerInformacionCliente(idCliente);
                this.servicioMail.EnviarUsuarioAceptadoMail(nuevoUsuario);

                return Json(this.servicioUsuario.ObtenerNuevosClientes().ToArray(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ObtenerListadoReservas()
        {
            var reservas = this.servicioReserva.ObtenerReservas();

            return Json(reservas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerReservasOrdenadasPorFecha() {

            var reservasCache = this.servicioReserva.ObtenerReservas();

            return Json(this.FormatearFechaDeReservas(reservasCache), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfirmarReservaDeSubasta(int idSubasta)
        {
            var subastaAceptada = this.servicioSubasta.ObtenerSubastasFinalizadas().SingleOrDefault(t => t.IdSubasta == idSubasta);
            var reservaSubasta = new ReservaViewModel();
            reservaSubasta.IdCliente = Convert.ToInt32(subastaAceptada.IdCliente);
            reservaSubasta.IdPropiedad = subastaAceptada.IdPropiedad;
            reservaSubasta.FechaReserva = subastaAceptada.FechaReserva;
            reservaSubasta.Credito = true;

            var mensaje = this.servicioReserva.AgregarReservaDesdeSubasta(reservaSubasta);

            if (mensaje != "OK") {
                return this.CambiarGanadorPuja(idSubasta);
            }                
            else {

                this.servicioSubasta.ConfirmarSubasta(idSubasta);

                this.servicioCredito.DescontarCreditoCliente(reservaSubasta.IdCliente, DateTime.Parse(reservaSubasta.FechaReserva).Year);

                var subastaActual = this.servicioSubasta.ObtenerSubasta(idSubasta);
                this.servicioMail.EnviarMailGanoSubasta(subastaActual);

                return this.SubastasCerradas();
            }
        }

        public JsonResult CancelarSubasta(int idSubasta)
        {
            if (this.servicioSubasta.RemoverSubasta(idSubasta))
                return this.SubastasCerradas();

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return null;
        }

        public JsonResult CancelarReservaDeCliente(int idReserva)
        {
            var reserva = this.servicioReserva.ObtenerReservas().Where(t => t.IdReserva == idReserva).SingleOrDefault();

            if (this.servicioReserva.CancelarReserva(idReserva))
            {   
                if (reserva.Credito)
                {
                    var anioReserva = DateTime.Parse(reserva.FechaReserva).Year;

                    this.servicioCredito.DevolverCreditoCliente(reserva.IdCliente, anioReserva);
                    return Json(string.Format("Se ha cancelado la reserva y se ha devuelto el credito al cliente para el año {0}.", anioReserva), JsonRequestBehavior.AllowGet);
                }

                return Json(string.Format("Se ha cancelado la reserva de HOT SALE."), JsonRequestBehavior.AllowGet);

            }              

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return null;
        }

        public JsonResult FiltrarSubastasPorFecha(string comienzo, string fin)
        {
            var subastas = this.servicioSubasta.ObtenerSubastasDesdeHoy();

            var desdeSubasta = DateTime.ParseExact(comienzo, "M/d/yyyy", CultureInfo.InvariantCulture);
            var hastaSubasta = DateTime.ParseExact(fin, "M/d/yyyy", CultureInfo.InvariantCulture);
                       
            if (hastaSubasta.CompareTo(desdeSubasta.AddMonths(2)) != -1)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(string.Format("La fecha 'Hasta' : {0} es mayor en dos meses a la fecha 'Desde' : {1}. La busqueda no es posible.", fin, comienzo), JsonRequestBehavior.AllowGet);
            }

            return Json(subastas.Where(t => (Convert.ToDateTime(t.FechaComienzo).CompareTo(desdeSubasta) > -1 
                                            && Convert.ToDateTime(t.FechaComienzo).CompareTo(hastaSubasta) < 1) ||
                                            (Convert.ToDateTime(t.FechaReserva).CompareTo(desdeSubasta) > -1)
                                            && Convert.ToDateTime(t.FechaReserva).CompareTo(hastaSubasta) < 1).ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult FiltrarReservasPorFecha(string comienzo, string fin)
        {
            var reservas = this.servicioReserva.ObtenerReservas();

            var desdeReserva = DateTime.ParseExact(comienzo, "M/d/yyyy", CultureInfo.InvariantCulture);
            var hastaReserva = DateTime.ParseExact(fin, "M/d/yyyy", CultureInfo.InvariantCulture);

            if (hastaReserva.CompareTo(desdeReserva.AddMonths(2)) != -1)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(string.Format("La fecha 'Hasta' : {0} es mayor en dos meses a la fecha 'Desde' : {1}. La busqueda no es posible.", fin, comienzo), JsonRequestBehavior.AllowGet);
            }

            reservas = reservas.Where(t => (Convert.ToDateTime(t.FechaReserva).CompareTo(desdeReserva) > 0 && Convert.ToDateTime(t.FechaReserva).CompareTo(hastaReserva) < 0) 
            || (Convert.ToDateTime(t.FechaReserva).AddDays(7).CompareTo(desdeReserva) > 0 && Convert.ToDateTime(t.FechaReserva).AddDays(7).CompareTo(hastaReserva) < 0)).ToList();
            return Json(this.FormatearFechaDeReservas(reservas).ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelarNuevoUsuario(int idCliente) {

            var mensaje = this.servicioUsuario.RechazarSolicitudNuevoCliente(idCliente);

            if (mensaje != "OK")
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }
                
            else
                return Json(this.servicioUsuario.ObtenerNuevosClientes().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelarPremium(int idCliente) {

            var mensaje = this.servicioUsuario.RechazarSolicitudPremium(idCliente);

            if (mensaje != "OK")
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }
            
            else
            {
                return Json(this.servicioUsuario.ObtenerSolicitudesPremium().ToArray(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ObtenerHistoricoHotSales()
        {
            var hotsales = this.servicioHotSale.ObtenerHotSalesHistoricos();
            return Json(hotsales.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerProximosHotSales()
        {
            var hotsales = this.servicioHotSale.ObtenerHotSalesFuturos();
            return Json(hotsales.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoverHotSale(int idHotSale)
        {
            var borro = this.servicioHotSale.BorrarSemanaHotSale(idHotSale);
            string mensaje;

            if (!borro)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                mensaje = "Ha ocurrido un error en el servidor y no se hay podido eliminar la semana de HOT SALE.";
            }
            else
            {
                mensaje = "Se ha borrado satisfactoriamente la semana definida como HOT SALE.";
            }

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarHotSale(int idHotSale, decimal valor)
        {
            string mensaje;

            var hotsaleModificar = this.servicioHotSale.ObtenerHotSalesFuturos().Where(t => t.IdHotSale == idHotSale).SingleOrDefault();

            if (valor <= 0 || hotsaleModificar.Precio == valor)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                mensaje = "El valor ingresado no es valido. No se ha podido actualizar el HOT SALE.";
            }
            else
            {
                this.servicioHotSale.ModificarSemanaHotSale(idHotSale, valor);

                mensaje = "Se ha actualizado satisfactoriamente el HOT SALE.";
            }

            return Json(mensaje, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CrearHotSale(decimal valor, int idPropiedad, string fecha)
        {
            var reservasPropiedad = this.servicioReserva.ObtenerReservasPropiedad(idPropiedad);
            var propiedad = this.servicioPropiedad.ObtenerPropiedades().Where(t => t.IdPropiedad == idPropiedad).SingleOrDefault();
            string mensaje;

            if (reservasPropiedad.Where(t => (DateTime.Parse(fecha) <= DateTime.Parse(t.FechaReserva).AddDays(7)) && DateTime.Parse(t.FechaReserva) <= DateTime.Parse(fecha)
                                    || (DateTime.Parse(fecha).AddDays(7) <= DateTime.Parse(t.FechaReserva).AddDays(7)  && DateTime.Parse(t.FechaReserva) <= DateTime.Parse(fecha).AddDays(7))).Any())
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                mensaje = string.Format("La residencia {0} no esta disponible durante la fecha seleccionada como HOT SALE.", propiedad.Nombre);
            }

            if (valor <= 0)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                mensaje = "El valor ingresado no es valido. No se ha podido generar la semana HOT SALE.";
            }

            HotSaleViewModel nuevoHotSale = new HotSaleViewModel();
            nuevoHotSale.FechaDisponible = fecha;
            nuevoHotSale.Precio = valor;
            nuevoHotSale.IdPropiedad = idPropiedad;
            nuevoHotSale.Estado = true;

           if(!this.servicioHotSale.CrearSemanaHotSale(nuevoHotSale))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                mensaje = string.Format("Ya hay definida una semana HOT SALE para la propiedad {0} dentro de la fecha elegida.", propiedad.Nombre);
            }
           else
            mensaje = string.Format("Se ha creado la semana HOT SALE para la propiedad {0}.", propiedad.Nombre);

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerInformacionHotSale(int idHotSale)
        {
            var hotsalesActuales = this.servicioHotSale.ObtenerHotSalesFuturos();
            var hotSaleActual = hotsalesActuales.Where(t => t.IdHotSale == idHotSale).SingleOrDefault();
            return Json(hotSaleActual, JsonRequestBehavior.AllowGet);
        }

        public void GuardarImagen()
        {
        }

        public void BorrarImagen()
        {

        }

        public JsonResult ObtenerImagenesDePropiedad(int idPropiedad)
        {
            var propiedad = this.servicioPropiedad.ObtenerTodasLasPropiedades().Where(t => t.IdPropiedad == idPropiedad).SingleOrDefault();
            return Json(propiedad.Imagenes.ToArray(), JsonRequestBehavior.AllowGet);
        }

        private JsonResult CambiarGanadorPuja(int idSubasta)
        {
            var subastaActual = this.servicioSubasta.ObtenerSubastasFinalizadas().Where(t => t.IdSubasta == idSubasta).FirstOrDefault();
            var usuarioSubasta = subastaActual.Cliente;

            this.servicioPuja.RemoverMaximaPuja(idSubasta);
            var ultimasPuja = this.servicioPuja.ObtenerPujas(idSubasta);

            if (!ultimasPuja.Any()) {

                this.servicioSubasta.RemoverSubasta(idSubasta);

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(string.Format("El sistema no puede confirmar la reserva para la propiedad {0} del cliente {1},{2} ya que no posee creditos. La subasta no posee pujas de menor valor y ha sido cancelada.", subastaActual.Propiedad.Nombre, usuarioSubasta.Apellido, usuarioSubasta.Nombre), JsonRequestBehavior.AllowGet);
            }

            var maximoPujante = ultimasPuja.FirstOrDefault();
            var nuevoClienteMaximo = this.servicioUsuario.ObtenerInformacionCliente(maximoPujante.IdCliente);

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(string.Format("El sistema no puede confirmar la reserva para la propiedad {0} del cliente {1},{2} ya que no posee creditos. La subasta se le ha adjudicado al cliente {3}, {4} con un monto de {5}.", subastaActual.Propiedad.Nombre, usuarioSubasta.Apellido, usuarioSubasta.Nombre, nuevoClienteMaximo.Apellido, nuevoClienteMaximo.Nombre, maximoPujante.Monto), JsonRequestBehavior.AllowGet);

        }

        private List<ReservaViewModel> FormatearFechaDeReservas(List<ReservaViewModel> reservasSinFormato)
        {
            List<ReservaViewModel> reservasOrdenadas = new List<ReservaViewModel>();

            foreach (var reservaEnCache in reservasSinFormato)
            {
                ReservaViewModel reservaAjustada = new ReservaViewModel();
                reservaAjustada.IdReserva = reservaEnCache.IdReserva;
                reservaAjustada.IdPropiedad = reservaEnCache.IdPropiedad;
                reservaAjustada.Propiedad = reservaEnCache.Propiedad;
                reservaAjustada.IdCliente = reservaEnCache.IdCliente;
                reservaAjustada.Cliente = reservaEnCache.Cliente;
                reservaAjustada.Credito = reservaEnCache.Credito;

                reservaAjustada.FechaReserva = string.Format("{0}-{1}-{2}", Convert.ToDateTime(reservaEnCache.FechaReserva).Day, Convert.ToDateTime(reservaEnCache.FechaReserva).Month, Convert.ToDateTime(reservaEnCache.FechaReserva).Year);

                reservasOrdenadas.Add(reservaAjustada);
            }

            return reservasOrdenadas;
        }

    }
}