using HomeSwitchHome.ViewModels;
using System.Collections.Generic;

namespace HomeSwitchHome.Services
{
    public interface IPropiedadService
    {
        List<PropiedadViewModel> ObtenerPropiedades();

        bool CrearPropiedad(PROPIEDAD nuevaPropiedad);

        bool ActualizarPropiedad(PROPIEDAD datosPrioridad, int idPropiedad);

    }
}
