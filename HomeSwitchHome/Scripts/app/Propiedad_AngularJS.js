var switchHomeApp = angular.module('homeswitchhome', []);

switchHomeApp.controller('propiedadesController', function ($scope, $http) {
    
    $http.get("/Propiedad/Propiedad/Propiedades").then(function (result) {

        $scope.propiedadesList = result.data;
    });

    $scope.aceptar = function () {

        $http.post("/Propiedad/Propiedad/CrearPropiedad", {
            'nombre': $scope.nombre,
            'domicilio': $scope.domicilio,
            'descripcion': $scope.descripcion,
            'pais': $scope.pais
        }).then(function successCallback(response) {
            alert("Se ha creado la residencia con éxito.");

            $('#addEditPropiedadModal').modal('hide');          

            console.log(response.data);

            $scope.propiedadesList = response.data;

        }, function errorCallback() {
            $('#addEditPropiedadModal').modal('hide');
            alert("No se ha podido crear la residencia con los campos ingresados.");
        });
    };

    function corroborarInputs() {
        return (
            ($("#nombrePropiedad").val().length < 8)
            || ($("#descripcionPropiedad").val().length < 20)
            || ($("#ubicacionPropiedad").val().length === 0))
    }

    $scope.modificar = function () {
        if (!corroborarInputs()) {
            $http.post("/Propiedad/Propiedad/ModificarPropiedad", {
                'idpropiedad': $("#identificadorPropiedad").val(),
                'nombre': $("#nombrePropiedad").val(),
                'domicilio': $("#ubicacionPropiedad").val(),
                'descripcion': $("#descripcionPropiedad").val(),
                'pais': $("#paisPropiedad").val()

            }).then(function (response) {
                $('#addEditPropiedadModal').modal('hide');
                alert("Se ha creado la residencia con éxito.");
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
});