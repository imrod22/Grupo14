var switchHomeApp = angular.module('homeswitchhome', []);

switchHomeApp.controller('subastasController', function ($scope, $http) {

    $scope.subastasList;
    $http.get("/Home/GetSubastas").sucess(function (result) {
        $scope.subastasList = result.data;
    });

    $scope.aceptar = function () {

        $http.post("/Home/AddSubasta", {
            'idPropiedad': $scope.idPropiedad,
            'valorMinimo': $scope.valorMinimo,
            'fechaComienzo': $scope.fechaComienzo

        }).success(function (data) {
            alert("Se ha creado la subasta para la propiedad: " + data.propiedad.Nombre + " con éxito.")
        }).error(function (error) {
            alert("No se ha podido crear la subasta con los campos ingresados.")
        });
    }
})