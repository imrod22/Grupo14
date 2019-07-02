using System.Collections.Generic;

namespace HomeSwitchHome.Services
{
    public interface IPujaService
    {
        void RegistrarPuja(int idSubasta, int idCliente, decimal monto);

        List<PUJA> ObtenerUltimaPuja();
    }
}
