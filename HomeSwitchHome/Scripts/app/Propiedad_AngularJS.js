var switchHomeApp = angular.module('homeswitchhome', []);

currentApp.controller('propiedadesController', function ($scope, $http) {

    $scope.propiedadesList;
    $http.get("/Home/GetPropiedades").then(function (result) {

        $scope.propiedadesList = result.data;
    });

    $scope.aceptar = function () {

        $http.post("/Home/AddPropiedad", {
            'nombre': $scope.nombre,
            'domicilio': $scope.domicilio,
            'descripcion': $scope.descripcion,
            'pais': $scope.pais
        }).success(function (response) {
            console.log("Se ha creado la residencia "+ response.Nombre +" con éxito.")
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