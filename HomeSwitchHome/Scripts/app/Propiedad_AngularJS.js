var switchHomeApp = angular.module('homeswitchhome', []);

switchHomeApp.controller('propiedadesController', function ($scope, $http) {
    
    function corroborarInputs() {
        return (
            ($("#nombrePropiedad").val().length >= 8)
            && ($("#descripcionPropiedad").val().length >= 20))
    }

    $http.get("/Propiedad/Propiedad/Propiedades").then(function (result) {

        $scope.propiedadesList = result.data;
        $scope.propiedadesListFiltradas = result.data;
    });

    // filtra las propiedades
    $scope.filtroNombre = '';
    $scope.filtroPais = '';

    $scope.filtrar = function () {
        $scope.propiedadesListFiltradas = $scope.propiedadesList.filter(filtroPropiedades);
    }
    function filtroPropiedades(prop) {
        return prop.Nombre.toUpperCase().includes($scope.filtroNombre.toUpperCase()) && prop.Pais.includes($scope.filtroPais.toUpperCase());
    }

    $scope.aceptar = function () {

        if (corroborarInputs()) {
            $http.post("/Propiedad/Propiedad/CrearPropiedad", {
                'nombre': $scope.nombre,
                'descripcion': $scope.descripcion,
                'pais': $scope.pais
            }).then(function successCallback(response) {

                if (response.data == "") {
                    alert("No se ha podido crear la residencia con los campos ingresados.");
                }
                else {                    
                    alert("Se ha creado la residencia con éxito.");
                    $scope.propiedadesList = response.data;
                }

                $('#addEditPropiedadModal').modal('hide');

            }, function errorCallback() {
                $('#addEditPropiedadModal').modal('hide');
                alert("No se ha podido crear la residencia con los campos ingresados.");
            });
        } else {

            alert("No se han ingresado todos los datos correctamente!");
        }
    }

    $scope.modificar = function () {
        if (corroborarInputs()) {
            $http.post("/Propiedad/Propiedad/ModificarPropiedad", {

                'idpropiedad': $("#identificadorPropiedad").val(),
                'descripcion': $("#descripcionPropiedad").val(),
                'pais': $("#paisPropiedad").val()

            }).then(function successCallback(response) {

                if (response.data == "") {
                    alert("No se ha podido actualizar la residencia con los campos ingresados.");
                    
                }
                else {
                    alert("Se ha actualizado la residencia con éxito.");
                    $scope.propiedadesList = response.data;

                }

                $('#addEditPropiedadModal').modal('hide');

            }, function errorCallback() {
                $('#addEditPropiedadModal').modal('hide');
                alert("No se ha podido actualizar la residencia con los campos ingresados.");
            });
        }
        else {

            alert("No se han ingresado todos los datos correctamente!");
        }
    }

    $scope.detallar = function (element) {

        $scope.detallenombre = "";
        $scope.detalledescripcion = "";
        $scope.detallepais = "";
        $scope.detalleimagenes = [];

        var idPropiedad = element;

        $http.post("/Propiedad/Propiedad/ObtenerInformacionPropiedad",
        {
            'idPropiedad': idPropiedad
        }

        ).then(function successCallback(response) {

            $scope.intervaloimagen = 5000;

            $scope.detallenombre = response.data.Nombre;
            $scope.detalledescripcion = response.data.Descripcion;
            $scope.detallepais = response.data.Pais;

            $scope.detalleimagenes = response.data.Imagenes;
            
        }, function errorCallback() {

            alert("Se ha producido un error en el servidor.");
        });
    }

    $scope.borrar = function (element) {
        var idPropiedad = element;

        $http.post("/Propiedad/Propiedad/BorrarPropiedad",
        {
                'idPropiedad': idPropiedad
        }

        ).then(function successCallback(response) {

            if (response.data == "") {
                alert("No se ha podido eliminar la residencia, tiene subastas asociadas o reservas futuras!");
            }

            else {
                alert("La residencia seleccionada ha sido eliminada.");
                $scope.propiedadesList = response.data;
            }        

        }, function errorCallback() {
            alert("No se ha podido eliminar la residencia, tiene subastas asociadas o reservas futuras!");
        });
    }

    $scope.reservar = function (element) {

        var idPropiedad = element;
        //var idUsuario 0 ?;

        $http.post("/Propiedad/Propiedad/ReservarPropiedad",
            {
                'idPropiedad': idPropiedad
            }

        ).then(function successCallback(response) {

        }, function errorCallback() {

        });
    }

    $scope.solicitarNovedad = function (element) {

        var idPropiedad = element;
        //var idUsuario 0 ?;

        $http.post("/Propiedad/Propiedad/SolicitarNovedadPropiedad",
            {
                'idPropiedad': idPropiedad
            }

        ).then(function successCallback(response) {

        }, function errorCallback() {

        });
    }

});