var switchHomeApp = angular.module('homeswitchhome', []);

switchHomeApp.controller('subastasController', function ($scope, $http) {

    $scope.subastasList;
    $scope.propiedadesList;

    $http.get("/Subasta/Subasta/Subastas").then(function (result) {
        $scope.subastasList = result.data;
    });

    $http.get("/Propiedad/Propiedad/Propiedades").then(function (result) {

        $scope.propiedadesList = result.data;
    });


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
                alert("El valor ingresado es menor al valor actual! o La subasta ya a terminado/finalizado.");
            }

            else {
                alert("Ha pujado satisfactoriamente en la subasta!");
                $scope.subastasList = result.data;
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

})