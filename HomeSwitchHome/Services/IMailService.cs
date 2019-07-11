using HomeSwitchHome.ViewModels;
using System.Collections.Generic;

namespace HomeSwitchHome.Services
{
    public interface IMailService
    {
        bool EnviarMailConRecuperacionContrasenia(ClienteViewModel clienteModel);

        void EnviarMailReservaHotSale();

        bool EnviarMailGanoSubasta(SubastaViewModel subastaModel);

        bool EnviarNotificacionNuevaSubasta(List<string> listaMails, SubastaViewModel subastaModel);

        bool EnviarNotificacionNuevoHotSale(List<ClienteViewModel> listaClientes, SubastaViewModel subastaModel);

        bool EnviarUsuarioAceptadoMail(ClienteViewModel cliente);

        bool EnviarPremiumAceptadoMail(ClienteViewModel cliente);
    }
}
