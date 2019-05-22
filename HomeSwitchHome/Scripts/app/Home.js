var currentApp = angular.module('homeswitchhome', []);

currentApp.controller('propiedadesController', function ($scope, $http) {

    $scope.propiedadesList;
    $http.get("/Home/GetPropiedades").then(function (result) {
        console.log(result);
        $scope.propiedadesList = result.data;
    })
});

currentApp.controller('subastasController', function ($scope, $http) {

    $scope.propiedadesList;
    $http.get("/Home/GetSubastas").then(function (result) {
        console.log(result);

        subastas = result.data;

        $scope.propiedadesList = result.data;
    })
});