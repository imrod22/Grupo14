using HomeSwitchHome.ViewModels;

namespace HomeSwitchHome.Services
{
    public interface IMailService
    {
        bool EnviarMailConRecuperacionContrasenia(ClienteViewModel clienteModel);

        void EnviarMailReservaPropiedad();

        void EnviarMailGanoSubasta();

        void EnviarNotificacionNuevaSubasta();

        void EnviarNotificacionNuevoHotSale();
    }
}
