using HomeSwitchHome.ViewModels;
using System.Collections.Generic;

namespace HomeSwitchHome.Services
{
    public interface ICreditoService
    {
        bool GenerarCreditosCliente(ClienteViewModel cliente);
               
        bool DescontarCreditoCliente(int creditoClienteId);

        bool DevolverCreditoCliente(int clienteId, int anio);

        List<ClienteCreditoViewModel> ObtenerCreditosAnio(int anio);
    }
}