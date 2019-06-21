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

        List<ReservaViewModel> ObtenerReservasPropiedad(int idPropiedad);        

        string AgregarReserva(ReservaViewModel reserva);

        string CancelarReservaCliente(int idReserva);

        bool CancelarReservaAdministrador(int idReserva);
    }
}
