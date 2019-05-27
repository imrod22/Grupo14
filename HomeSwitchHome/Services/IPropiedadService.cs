using HomeSwitchHome.ViewModels;
using System.Collections.Generic;

namespace HomeSwitchHome.Services
{
    public interface IPropiedadService
    {
        List<PropiedadViewModel> ObtenerPropiedades();

        void CrearPropiedad(PROPIEDAD nuevaPropiedad);

        void ActualizarPropiedad(PROPIEDAD datosPrioridad);

    }
}
