﻿using HomeSwitchHome.ViewModels;
using System.Collections.Generic;

namespace HomeSwitchHome.Services
{
    public interface IPropiedadService
    {
        List<PropiedadViewModel> ObtenerPropiedades();

        List<PropiedadViewModel> ObtenerTodasLasPropiedades();

        bool CrearPropiedad(PROPIEDAD nuevaPropiedad);

        bool ActualizarPropiedad(PROPIEDAD datosPrioridad, int idPropiedad);

        bool BorrarPropiedad(int idPropiedad);

        bool CambiarEstatusPropiedad(int idPropiedad);

        bool RegistrarNotificacionesDePropiedad(NovedadViewModel nuevaNovedad);

        List<NOVEDAD_PROPIEDAD> ObtenerNotificaciones(int idPropiedad);

        bool AgregarImagen(int idPropiedad, string nombreImagen);

        bool EliminarImagen(int idImagen);
    }
}
