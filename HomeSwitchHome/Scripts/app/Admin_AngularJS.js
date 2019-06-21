var adminsection = angular.module('adminsection', []);

adminsection.controller('admincontroller', function ($scope, $http) {

    $scope.subastasoff;
    $scope.subastasfin;
    $scope.parapremiums;
    $scope.nuevosclientes;
    $scope.propiedades;
    $scope.reservas;

    var datenow = moment().add(6, 'months');
    var datelimit = moment().add(12, 'months');
    
    $http.get("/Administrador/Administrador/SubastasSinEmpezar").then(function (result) {
        $scope.subastasoff = result.data;
    });

    $http.get("/Administrador/Administrador/SubastasCerradas").then(function (result) {
        $scope.subastasfin = result.data;
    });
    
    $http.get("/Administrador/Administrador/Propiedades").then(function (result) {
        $scope.propiedades = result.data;
    });

    $http.get("/Administrador/Administrador/ClientesAPremium").then(function (result) {
        $scope.parapremiums = result.data;
    });

    $http.get("/Administrador/Administrador/NuevosClientes").then(function (result) {
        $scope.nuevosclientes = result.data;
    });

    $http.get("/Administrador/Administrador/ObtenerReservasOrdenadasPorFecha").then(function (result) {
        $scope.reservas = result.data;
    });

    $('#fromsubasta').pignoseCalendar({
        minDate: datenow,
        date: datenow,
        maxDate: datelimit,
        multiple: true,
        buttons: true,
        theme: 'dark',
        apply: function (dates, context) {

            $http.post("/Administrador/Administrador/FiltrarSubastasPorFecha", {
                'comienzo': dates[0].format('l'),
                'fin': dates[1].format('l')

            }).then(function (result) {
                $scope.subastasoff = result.data;

            }, function errorCallback(jqXHR) {
                    $('#fromreserva').prop('value', "");
                    swal("Home Switch Home", jqXHR.data, "error");
            });            
        }
    });

    $('#fromreserva').pignoseCalendar({
        minDate: datenow,
        date: datenow,
        maxDate: datelimit,
        multiple: true,
        buttons: true,
        theme: 'dark',
        apply: function (dates, context) {

            $http.post("/Administrador/Administrador/FiltrarReservasPorFecha", {
                'comienzo': dates[0].format('l'),
                'fin': dates[1].format('l')

            }).then(function (result) {
                $scope.reservas = result.data;

            }, function errorCallback(jqXHR) {
                    $('#fromreserva').prop('value', "");
                    swal("Home Switch Home", jqXHR.data, "error");
            });
        }
    });

    function datosDePropiedadCorrectos() {
        return (
            (($("#nombrePropiedad").val() == null)
            && ($("#descripcionPropiedad").val() == null)
            && ($("#nombrePropiedad").val().length >= 8)
            && ($("#descripcionPropiedad").val().length >= 20)
            && ($("#paisPropiedad").val().length == null)))
    };

    $scope.crearpropiedad = function () {

        if (datosDePropiedadCorrectos()) {
            $http.post("/Administrador/Administrador/CrearPropiedad", {
                'nombre': $scope.nombre,
                'descripcion': $scope.descripcion,
                'pais': $scope.pais
            }).then(function successCallback(response) {

                if (response.data == "") {
                    swal("Home Switch Home", "No se ha podido crear la residencia con los campos ingresados. Ya existe una residencia con el titulo ingresado.", "error");

                }
                else {
                    $scope.propiedades = response.data;
                    swal("Home Switch Home", "Se ha creado la residencia con éxito.", "success");                   
                }

                $('#addEditPropiedadModal').modal('hide');

            }, function errorCallback() {
                swal("Home Switch Home", "No se ha podido crear la residencia. Se ha producido un error en el servidor.", "error");
                $('#addEditPropiedadModal').modal('hide');
            });
        }
        else {
            swal("Home Switch Home", "No se han ingresado los datos correctamente.", "error");            
        }
    }

    $scope.modificarpropiedad = function () {
        if (datosDePropiedadCorrectos()) {
            $http.post("/Administrador/Administrador/ModificarPropiedad", {

                'idpropiedad': $("#identificadorPropiedad").val(),
                'descripcion': $("#descripcionPropiedad").val(),
                'pais': $("#paisPropiedad").val()

            }).then(function successCallback(response) {

                if (response.data == "") {
                    swal("Home Switch Home", "No se ha podido actualizar la residencia con los campos ingresados.", "error");

                }
                else {
                    swal("Home Switch Home", "Se ha actualizado la residencia con éxito.", "success");
                    $scope.propiedades = response.data;

                }

                $('#addEditPropiedadModal').modal('hide');

            }, function errorCallback() {
                swal("Home Switch Home", "Ha ocurrido un error en el servidor.", "error");
                $('#addEditPropiedadModal').modal('hide');
            });
        }
        else {
            swal("Home Switch Home", "No se han ingresado todos los datos correctamente.", "warning");
        }
    }

    $scope.borrarpropiedad = function (element) {
        var idPropiedad = element;

        $http.post("/Administrador/Administrador/BorrarPropiedad",
            {
                'idPropiedad': idPropiedad
            }

        ).then(function successCallback(response) {

            if (response.data == "") {
                swal("Home Switch Home", "No se ha podido eliminar la residencia, tiene subastas asociadas o reservas futuras.", "warning");
            }

            else {
                swal("Home Switch Home", "La residencia seleccionada ha sido eliminada.", "success");
                $scope.propiedades = response.data;
            }

        }, function errorCallback() {
            swal("Home Switch Home", "No se puede eliminar la residencia. Ha ocurrido un error en el servidor", "error");
           
        });
    }

    $scope.crearsubasta = function () {

        if (valoresDeSubastaAceptados()) {

            var idPropiedad = $('#propiedad_select option:selected').attr('id');

            $http.post("/Administrador/Administrador/CrearSubasta", {
                'propiedad': idPropiedad,
                'valorMinimo': $scope.valorMinimo,
                'fechaComienzo': $('input[name="fechasubasta"]').val(),
                'fechaReserva': $('input[name="fechacomienzoreserva"]').val()

            }).then(function successCallback(response) {

                swal("Home Switch Home", "Se ha creado la subasta con éxito.", "success");
                $scope.subastasoff = response.data;
                $('#addEditSubastaModal').modal('hide');

            }, function errorCallback(jqXHR) {
                swal("Home Switch Home", jqXHR.data, "error");

            });

        }

        else {
            swal("Home Switch Home", "Los valores ingresados no son correctos.", "warning");            
        }        
    }

    $scope.modificarsubasta = function () {
        if (valoresDeSubastaAceptados()) {
            $http.post("/Administrador/Administrador/ModificarSubasta", {

                'idSubasta': $("#identificadorSubasta").val(),
                'valorMinimo': $("#valorMinimo").val()

            }).then(function successCallback(response) {

                if (response.data == "") {
                    swal("Home Switch Home", "El monto de la subasta ingresado no es valido.", "error");
                    
                }
                else {
                    swal("Home Switch Home", "Se ha actualizado el monto de la subasta con éxito.", "success");
                    $scope.subastasoff = response.data;

                }

                $('#addEditSubastaModal').modal('hide');

            }, function errorCallback() {
                    $('#addEditSubastaModal').modal('hide');
                    swal("Home Switch Home", "No se ha podido actualizar la subasta. Ha ocurrido un error en el servidor.", "error");
            });
        }
        else {
            swal("Home Switch Home", "No se han ingresado todos los datos correctamente", "warning");
        }
    }

    $scope.borrarsubasta = function (element) {
        var idSubasta = element;

        $http.post("/Administrador/Administrador/BorrarSubasta", {
                'idSubasta': idSubasta
            }

        ).then(function successCallback(result) {

            if (result.data == null) {
                swal("Home Switch Home", "No se ha podido eliminar la subasta seleccionada.", "error");
            }
            else {
                swal("Home Switch Home", "La subasta seleccionada ha sido eliminada.", "success");
                $scope.subastasoff = result.data;
            }            

        }, function errorCallback() {
            swal("Home Switch Home", "No se ha podido eliminar la subasta. Ha ocurrido un error en el servidor.", "error");
            
        });
    }

    $scope.aceptarnuevousuario = function (element) {

        var idCliente = element;

        $http.post("/Administrador/Administrador/AceptarNuevoUsuario",
            {
                'idCliente': idCliente
            }

        ).then(function successCallback(result) {

                swal("Home Switch Home", "Se ha procesado el registro para el nuevo usuario.", "success");
                $scope.nuevosclientes = result.data;        

        }, function errorCallback(jqXHR) {
            swal("Home Switch Home", jqXHR.data, "error");

        });
    }

    $scope.confirmarpremium = function (element) {

        var idCliente = element;

        $http.post("/Administrador/Administrador/AceptarPremium",
            {
                'idCliente': idCliente
            }

        ).then(function successCallback(result) {

                swal("Home Switch Home", "Se ha procesado la solicitud premium del usuario seleccionado.", "success");
                $scope.parapremiums = result.data;

        }, function errorCallback(jqXHR) {
            swal("Home Switch Home", jqXHR.data, "error");

        });
    }

    $scope.confirmarsubasta = function (element) {
        var idSubasta = element;

        $http.post("/Administrador/Administrador/ConfirmarReservaDeSubasta",
            {
                'idSubasta': idSubasta
            }

        ).then(function successCallback(result) {

            swal("Home Switch Home", "Se han actualizado las listas de subastas cerradas y reservas efectuadas.", "success");
            $scope.subastasfin = result.data;

            $http.get("/Administrador/Administrador/ObtenerReservasOrdenadasPorFecha").then(function (result) {
                $scope.reservas = result.data;
            });                                

        }, function errorCallback() {
            swal("Home Switch Home", "No se ha podido confirmar la subasta. Ha ocurrido un error en el servidor.", "error");
        });
    }

    $scope.rechazarsubasta = function (element) {
        var idSubasta = element;

        $http.post("/Administrador/Administrador/CancelarSubasta",
            {
                'idSubasta': idSubasta
            }

        ).then(function successCallback(result) {

                swal("Home Switch Home", "Se ha cancelado la subasta.", "success");
                $scope.subastasfin = result.data;

        }, function errorCallback() {
            swal("Home Switch Home", "No se ha podido cancelar la subasta. Ha ocurrido un error en el servidor.", "error");

        });
    }

    $scope.cancelarreserva = function (element) {
        var idReserva = element;

        $http.post("/Administrador/Administrador/CancelarReservaDeCliente",
            {
                'idReserva': idReserva
            }

        ).then(function successCallback(result) {            
                swal("Home Switch Home", "Se ha cancelado la reserva.", "success");
                $scope.reservas = result.data;
            

        }, function errorCallback() {
            swal("Home Switch Home", "No se ha podido cancelar la reserva. Ha ocurrido un error en el servidor.", "error");

        });
    }

    $scope.rechazarnuevousuario = function (element) {
        var idCliente = element;

        $http.post("/Administrador/Administrador/CancelarNuevoUsuario",
            {
                'idCliente': idCliente
            }

        ).then(function successCallback(result) {

            swal("Home Switch Home", "Se ha rechazado la solicitud de nuevo usuario.", "success");
            $scope.nuevosclientes = result.data;

        }, function errorCallback(jqXHR) {
            swal("Home Switch Home", jqXHR.data, "error");

        });
    }

    $scope.rechazarpremium = function (element) {
        var idCliente = element;

        $http.post("/Administrador/Administrador/CancelarPremium",
            {
                'idCliente': idCliente
            }

        ).then(function successCallback(result) {
                swal("Home Switch Home", "Se ha rechazado la solicitud premium del usuario seleccionado.", "success");
                $scope.parapremiums = result.data;

        }, function errorCallback(jqXHR) {
            swal("Home Switch Home", jqXHR.data, "error");

        });
    }

    function valoresDeSubastaAceptados()
    {
        return ($.isNumeric($scope.valorMinimo)
            && ($("#propiedad_select option:selected").text() != "")
            && ($('#fechaSubasta').prop('value') != "")
            && ($('#fechaReservaSubasta').prop('value') != "")
        );
    }
});