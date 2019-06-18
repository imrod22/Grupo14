var switchHomeApp = angular.module('homeswitchhome', []);

switchHomeApp.controller('subastasController', function ($scope, $http) {

    $scope.subastasList = [];
    $scope.subastasListFiltradas = [];
    $scope.date = new Date();

    $http.get("/Subasta/Subasta/Subastas").then(function (result) {
        $scope.subastasList = result.data;
        $scope.subastasListFiltradas = result.data;
    });

    $scope.filtroNombre = '';
    $scope.filtroPais = '';

    $scope.filtrar = function () {
        $scope.subastasListFiltradas = $scope.subastasList.filter(filtroSubastas);
    }
    function filtroSubastas(sub) {
        return sub.Propiedad.Nombre.toUpperCase().includes($scope.filtroNombre.toUpperCase()) && sub.Propiedad.Pais.includes($scope.filtroPais.toUpperCase());
    }

    $scope.pujar = function (element) {

        $http.post("/Subasta/Subasta/PujarEnSubasta",
            {
                'idSubasta': $("#identificadorSubasta").val(),
                'valorPujado': $("#valorPujado").val()
            }

        ).then(function successCallback(result) {

            if (result.data == "") {
                alert("El valor ingresado es menor al valor actual! o La subasta ya ha terminado/finalizado.");
            }

            else {
                alert("Ha pujado satisfactoriamente en la subasta!");
                $scope.subastasList = result.data;
                $scope.subastasListFiltradas = result.data;
            }

            $('#pujaSubastaModal').modal('hide');


        }, function errorCallback() {

            $('#pujaSubastaModal').modal('hide');
            alert("Se ha producido un error en el servidor.");
        });
    }

    $scope.subastasIsEmpty = function () {
        return $scope.subastasListFiltradas.length == 0;
    }

    $scope.finalizaEn = function (sub) {
        console.log(sub.FechaComienzo);
        var fechaFin = addDays(sub.FechaComienzo, 3);
        var hours = Math.abs(fechaFin - new Date());
        hours = hours / (1000 * 60 * 60);
        return hours.toFixed(1);
    }

    function replaceAt(string, index, replace) {
        return string.substring(0, index) + replace + string.substring(index + 1);
    }

    function dateFromString(date) {
        var day0 = date.charAt(0);
        var day1 = date.charAt(1);
        date = replaceAt(date, 0, date.charAt(3));
        date = replaceAt(date, 1, date.charAt(4));
        date = replaceAt(date, 3, day0);
        date = replaceAt(date, 4, day1);
        date = date.slice(0, -5);
        date = new Date(date);
        return date;
    }

    function addDays(date, days) {
        var result = dateFromString(date);
        result.setDate(result.getDate() + days);
        return result;
    }

});

switchHomeApp.filter('dateRange', function () {
    return function (items, fromDate, toDate) {
        var filtered = [];
        console.log(fromDate, toDate);
        var from_date = Date.parse(fromDate);
        var to_date = Date.parse(toDate);
        if (!to_date || !from_date) {
            return items;
        }
        angular.forEach(items, function (item) {
            if (Date.parse(item.FechaComienzo) > from_date && Date.parse(item.FechaComienzo) < to_date) {
                filtered.push(item);
            }
        });
        return filtered;
    };
});