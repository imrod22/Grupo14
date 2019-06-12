using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSwitchHome.Services
{
    public interface ISubastaService
    {
        List<SubastaViewModel> ObtenerSubastasActivas();

        List<SubastaViewModel> ObtenerSubastasFinalizadas();

        List<SubastaViewModel> ObtenerSubastasFuturas();

        List<SubastaViewModel> ObtenerSubastasDePropiedad(int idPropiedad);

        string CrearSubasta(SUBASTA nuevaSubasta);

        bool PujarSubasta(SUBASTA subastaPujada, int idSubasta, int idCliente);

        bool ActualizarSubasta(SUBASTA subastaActualizada, int idSubasta);

        bool ConfirmarSubasta(int idSubasta);

        bool RemoverSubasta(int idSubasta);
    }
}
