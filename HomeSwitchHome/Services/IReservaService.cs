using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSwitchHome.Services
{
    public interface IReservaService
    {
        List<ReservaViewModel> ObtenerReservas();

        List<ReservaViewModel> ObtenerReservasCliente(int idCliente);

        List<ReservaViewModel> ObtenerReservasClientePorAnio(int idCliente, int anio);

        List<ReservaViewModel> ObtenerReservasPropiedad(int idPropiedad);        

        string AgregarReserva(ReservaViewModel reserva);

        bool CancelarSubastasDePropiedadReservada(ReservaViewModel reservaModelo);

        string AgregarReservaDesdeHotSale(ReservaViewModel reservaModelo);

        bool CancelarReserva(int idReserva);
    }
}
