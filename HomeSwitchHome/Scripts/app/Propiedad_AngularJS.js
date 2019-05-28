var switchHomeApp = angular.module('homeswitchhome', []);

switchHomeApp.controller('propiedadesController', function ($scope, $http) {

    $scope.propiedadesList;
    $http.get("/Propiedad/Propiedad/Propiedades").then(function (result) {

        $scope.propiedadesList = result.data;
    });

    $scope.aceptar = function () {

        $http.post("/Propiedad/CrearPropiedad", {
            'nombre': $scope.nombre,
            'domicilio': $scope.domicilio,
            'descripcion': $scope.descripcion,
            'pais': $scope.pais
        }).success(function (response) {
            console.log("Se ha creado la residencia "+ response.data +" con éxito.")
        }).error(function (error) {
            alert("No se ha podido crear la residencia con los campos ingresados.")
        });
    };

    function corroborarInputs() {
        return (
            ($scope.nombre.length < 8)
            || ($scope.descripcion.length < 20)
            || ($scope.ubicacion == ''))
    }

    $scope.modificar = function () {
        if (!corroborarInputs()) {
            $http.post("/Home/ModificarPropiedad", {
                'nombre': $scope.nombre,
                'domicilio': $scope.domicilio,
                'descripcion': $scope.descripcion,
                'pais': $scope.pais
            }).then(function (response) {
                console.log("Se ha creado la residencia con éxito.");
                alert("Se ha creado la residencia con éxito.");
            })
        }
    }
});