var switchHomeApp = angular.module('homeswitchhome', []);

switchHomeApp.controller('propiedadesController', function ($scope, $http) {
    
    $http.get("/Propiedad/Propiedad/Propiedades").then(function (result) {

        $scope.propiedadesList = result.data;
    });

    $scope.aceptar = function () {

        $http.post("/Propiedad/Propiedad/CrearPropiedad", {
            'nombre': $scope.nombre,
            'descripcion': $scope.descripcion,
            'pais': $scope.pais
        }).then(function successCallback(response) {
            alert("Se ha creado la residencia con éxito.");

            $('#addEditPropiedadModal').modal('hide');

            $scope.propiedadesList = response.data;

        }, function errorCallback() {
            $('#addEditPropiedadModal').modal('hide');
            alert("No se ha podido crear la residencia con los campos ingresados.");
        });
    };

    function corroborarInputs() {
        return (
            ($("#nombrePropiedad").val().length < 8)
            || ($("#descripcionPropiedad").val().length < 20))
    }

    $scope.modificar = function () {
        if (!corroborarInputs()) {
            $http.post("/Propiedad/Propiedad/ModificarPropiedad", {
                'idpropiedad': $("#identificadorPropiedad").val(),
                'descripcion': $("#descripcionPropiedad").val(),
                'pais': $("#paisPropiedad").val()

            }).then(function successCallback(response) {
                $('#addEditPropiedadModal').modal('hide');
                alert("Se ha actualizado la residencia con éxito.");
                $scope.propiedadesList = response.data;

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

        ).then(function successCallback(result) {

            $scope.intervaloimagen = 5000;


            $scope.detallenombre = result.data.Nombre;
            $scope.detalledescripcion = result.data.Descripcion;
            $scope.detallepais = result.data.Pais;

            $scope.detalleimagenes = result.data.Imagenes;
            
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

        ).then(function successCallback(result) {

            alert("La residencia seleccionada ha sido eliminada.");

            $scope.propiedadesList = result.data;



        }, function errorCallback() {

            alert("No se ha podido eliminar la residencia, tiene subastas asociadas o reservas futuras!");
        });
    }
});