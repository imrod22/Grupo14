var switchHomeApp = angular.module('homeswitchhome', []);

switchHomeApp.controller('subastasController', function ($scope, $http) {

    $scope.subastasList = [];
    $scope.propiedadesList;
    $scope.subastasListFiltradas = [];

    $scope.date = new Date();

    $http.get("/Subasta/Subasta/Subastas").then(function (result) {
        $scope.subastasList = result.data;
        $scope.subastasListFiltradas = result.data;
    });

    $http.get("/Propiedad/Propiedad/Propiedades").then(function (result) {

        $scope.propiedadesList = result.data;
    });

    // filtra las propiedades
    $scope.filtroNombre = '';
    $scope.filtroPais = '';

    $scope.filtrar = function () {
        $scope.subastasListFiltradas = $scope.subastasList.filter(filtroSubastas);
    }
    function filtroSubastas(sub) {
        return sub.Propiedad.Nombre.toUpperCase().includes($scope.filtroNombre.toUpperCase()) && sub.Propiedad.Pais.includes($scope.filtroPais.toUpperCase());
    }


    $scope.aceptar = function () {

        var idPropiedad = $('#propiedadSelect option:selected').attr('id');

        $http.post("/Subasta/Subasta/CrearSubasta", {
            'propiedad': idPropiedad,
            'valorMinimo': $scope.valorMinimo,
            'fechaComienzo': $scope.fechaComienzo

        }).then(function successCallback(response) {
            alert("Se ha creado la subasta con éxito.");

            $('#addEditSubastaModal').modal('hide');

            $scope.subastasList = response.data;

        }, function errorCallback() {
            $('#addEditSubastaModal').modal('hide');
            alert("No se ha podido crear la subasta con los campos ingresados.");
        });
    }

    $scope.modificar = function () {
        if (!sonDatosInvalidos()) {
            $http.post("/Subasta/Subasta/ModificarSubasta", {
            
                'idSubasta': $("#identificadorSubasta").val(),
                'fechaComienzo': $("#fechaSubasta").val(),
                'valorMinimo': $("#valorMinimo").val()

            }).then(function successCallback(response) {

                if (result.data == "") {
                    alert("No se ha podido actualizar la subasta con los campos ingresados.");
                    
                }
                else {
                    alert("Se ha actualizado la subasta con éxito.");
                    $scope.subastasList = response.data;

                }

                $('#addEditSubastaModal').modal('hide');

            }, function errorCallback() {
                $('#addEditSubastaModal').modal('hide');
                    alert("No se ha podido actualizar la subasta con los campos ingresados.");
            });
        }
        else {

            alert("No se han ingresado todos los datos correctamente!");
        }
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

    $scope.borrar = function (element) {
        var idSubasta = element;

        $http.post("/Subasta/Subasta/BorrarSubasta",
            {
                'idPropiedad': idSubasta
            }

        ).then(function successCallback(result) {

            alert("La subasta seleccionada ha sido eliminada.");
            $scope.subastasList = result.data;

        }, function errorCallback() {

            alert("No se ha podido eliminar la subasta, ya ha comenzado!");
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

})