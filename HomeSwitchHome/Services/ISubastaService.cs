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

        void CrearSubasta(SUBASTA nuevaSubasta);

        void PujarSubasta(SUBASTA subastaPujada);
    }
}
