using HomeSwitchHome.ViewModels;
using System.Collections.Generic;

namespace HomeSwitchHome.Services
{
    public interface IPujaService
    {
        void RegistrarPuja(int idSubasta, int idCliente, decimal monto);
        
        List<PujaViewModel> ObtenerPujas(int idSubasta);

        void RemoverMaximaPuja(int idSubasta);
    }
}
