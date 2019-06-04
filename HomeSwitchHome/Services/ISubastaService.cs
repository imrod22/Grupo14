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
        List<SubastaViewModel> ObtenerSubastas();

        bool CrearSubasta(SUBASTA nuevaSubasta);

        bool PujarSubasta(SUBASTA subastaPujada, int idSubasta);

        bool ActualizarSubasta(SUBASTA subastaActualizada, int idSubasta);

        bool RemoverSubasta(int idSubasta);
    }
}
