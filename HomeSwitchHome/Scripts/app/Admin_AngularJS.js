var adminsection = angular.module('adminsection', []);

adminsection.controller('admincontroller', function ($scope, $http) {

    $scope.subastasoff;
    $scope.subastasfin;
    $scope.parapremiums;
    $scope.nuevosclientes;
    $scope.propiedades;
    $scope.reservas;
    $scope.hotsalehistorico;
    $scope.hotsalefuturos;
    $scope.imagenes;
    $scope.pathnuevafoto;

    var datecurrent = moment();
    var datenow = moment().add(6, 'months');
    var datelimit = moment().add(12, 'months');
    
    $http.get("/Administrador/Administrador/SubastasHistorial").then(function (result) {
        $scope.subastasoff = result.data;
    });

    $http.get("/Administrador/Administrador/SubastasCerradas").then(function (result) {
        $scope.subastasfin = result.data;
    });
    
    $http.get("/Administrador/Administrador/ObtenerTodasLasPropiedades").then(function (result) {
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

    $http.get("/Administrador/Administrador/ObtenerHistoricoHotSales").then(function (result) {
        $scope.hotsalehistorico = result.data;
    });

    $http.get("/Administrador/Administrador/ObtenerProximosHotSales").then(function (result) {
        $scope.hotsalefuturos = result.data;
    });

    $('#fromsubasta').pignoseCalendar({
        date: datecurrent,
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
        date: datecurrent,
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
            (($("#nombrePropiedad").val() != null)
            && ($("#descripcionPropiedad").val() != null)
            && ($("#nombrePropiedad").val().length >= 8)
            && ($("#descripcionPropiedad").val().length >= 20)
            && ($("#paisPropiedad").val().length != null)
            && ($("#ciudadPropiedad").val().length != null)))
    };

    function valoresDeSubastaAceptados() {
        return ($.isNumeric($scope.valorMinimo)
            && ($("#propiedad_select option:selected").text() != "")
            && ($('#fechaSubasta').prop('value') != "")
            && ($('#fechaReservaSubasta').prop('value') != "")
        );
    }

    $scope.crearpropiedad = function () {

        if (datosDePropiedadCorrectos()) {
            $http.post("/Administrador/Administrador/CrearPropiedad", {
                'nombre': $scope.nombre,
                'ciudad': $scope.ciudad,
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

        $http.post("/Administrador/Administrador/EliminarPropiedad",
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

    $scope.estadopropiedad = function (element) {
        var idPropiedad = element;

        $http.post("/Administrador/Administrador/ActualizarEstatusPropiedad",
            {
                'idPropiedad': idPropiedad
            }

        ).then(function successCallback(response) {

            if (response.data == "") {
                swal("Home Switch Home", "No se puede eliminar la residencia. Ha ocurrido un error en el servidor.", "error");
            }

            else {
                swal("Home Switch Home", "La residencia seleccionada ha actualizado su disponiblidad.", "success");
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


            $http.get("/Administrador/Administrador/SubastasCerradas").then(function (subastas) {
                $scope.subastasfin = subastas.data;
            });

            $http.get("/Administrador/Administrador/ObtenerReservasOrdenadasPorFecha").then(function (reservas) {
                $scope.reservas = reservas.data;
            });                                

            swal("Home Switch Home", result.data, "success");

        }, function errorCallback(jqXHR) {
                swal("Home Switch Home", jqXHR.data, "error");
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

            swal("Home Switch Home", result.data, "success");

            $http.get("/Administrador/Administrador/ObtenerReservasOrdenadasPorFecha").then(function (result) {
                $scope.reservas = result.data;
            });            

        }, function errorCallback(jqXHR) {
            swal("Home Switch Home", jqXHR.data, "error");

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

    $scope.rechazarhotsale = function (element) {
        var idHotSale = element;

        $http.post("/Administrador/Administrador/RemoverHotSale",
            {
                'idHotSale': idHotSale
            }

        ).then(function successCallback(result) {

            swal("Home Switch Home", result.data, "success");
            $http.get("/Administrador/Administrador/ObtenerProximosHotSales").then(function (result) {
                $scope.hotsalefuturos = result.data;
            });

        }, function errorCallback(jqXHR) {
            swal("Home Switch Home", jqXHR.data, "error");

        });
    }

    $scope.modificarhotsale = function () {
        $http.post("/Administrador/Administrador/ModificarHotSale", {

            'idHotSale': $("#identificadorHotSale").val(),
            'valor': $("#valorhotsale").val()

        }).then(function successCallback(response) {

            $('#addEditHotSaleModal').modal('hide');
            swal("Home Switch Home", response.data, "success");

            $http.get("/Administrador/Administrador/ObtenerProximosHotSales").then(function (result) {
                $scope.hotsalefuturos = result.data;
            });

        }, function errorCallback(jqXHR) {

            swal("Home Switch Home", jqXHR.data, "error");

        });
    }

    $scope.altahotsale = function () {
        var idPropiedad = $('#propiedad_select option:selected').attr('id');

        $http.post("/Administrador/Administrador/CrearHotSale", {

            'valor': $("#valorhotsale").val(),
            'idPropiedad': idPropiedad,
            'fecha': $('input[name="fechahotsale"]').val(),

        }).then(function successCallback(response) {
            $('#addEditHotSaleModal').modal('hide');
            swal("Home Switch Home", response.data, "success");

            $http.get("/Administrador/Administrador/ObtenerProximosHotSales").then(function (result) {
                

                $scope.hotsalefuturos = result.data;
            });

        }, function errorCallback(jqXHR) {
                
            swal("Home Switch Home", jqXHR.data, "error");

        });
    }

    $scope.obtenerimagenes = function (element) {

        var idPropiedad = element;

        $http.post("/Administrador/Administrador/ObtenerImagenesDePropiedad",
            {
                'idPropiedad': idPropiedad
            }

        ).then(function successCallback(response) {
                       
            $scope.imagenes = response.data;

        })
    }

    $scope.refrescarsubastas = function () {
        $('#fromsubasta').val('');

        $http.get("/Administrador/Administrador/SubastasHistorial").then(function (result) {
            $scope.subastasoff = result.data;
        });
    }

    $scope.refrescarreservas = function () {
        $('#fromreserva').val('');

        $http.get("/Administrador/Administrador/ObtenerReservasOrdenadasPorFecha").then(function (result) {
            $scope.reservas = result.data;
        });
    }

    $scope.eliminarimagen = function (element) {

        $http.post("/Administrador/Administrador/EliminarImagen",
            {
                'idImagen': element
            }

        ).then(function successCallback(result) {

            swal("Home Switch Home", result.data, "success");

            var idPropiedad = $("#identificadorPropImagenes").val();

            $http.post("/Administrador/Administrador/ObtenerImagenesDePropiedad",
                {
                    'idPropiedad': idPropiedad
                }

            ).then(function successCallback(response) {

                $scope.imagenes = response.data;
            })

        }, function errorCallback(jqXHR) {
            swal("Home Switch Home", jqXHR.data, "error");

        });
    }
});

adminsection.controller("fileUploadCtrl", ['$scope', function ($scope) {

}]);

adminsection.directive("imgUpload", function ($http, $compile) {
    return {
        restrict: 'AE',
        scope: {
            url: "@",
            method: "@"
        },
        template: '<input class="fileUpload" type="file" multiple />' +
            '<div class="dropzone">' +
            '<p class="msg">Click or Drag and Drop files to upload</p>' +
            '</div>' +
            '<div class="preview clearfix">' +
            '<div class="previewData clearfix" ng-repeat="data in previewData track by $index">' +
            '<img src={{data.src}}></img>' +
            '<div class="previewDetails">' +
            '<div class="detail"><b>Name : </b>{{data.name}}</div>' +
            '<div class="detail"><b>Type : </b>{{data.type}}</div>' +
            '<div class="detail"><b>Size : </b> {{data.size}}</div>' +
            '</div>' +
            '<div class="previewControls">' +
            '<span ng-click="upload(data)" class="circle upload">' +
            '<i class="fa fa-check"></i>' +
            '</span>' +
            '<span ng-click="remove(data)" class="circle remove">' +
            '<i class="fa fa-close"></i>' +
            '</span>' +
            '</div>' +
            '</div>' +
            '</div>',
        link: function (scope, elem, attrs) {
            var formData = new FormData();
            scope.previewData = [];

            function previewFile(file) {
                var reader = new FileReader();
                var obj = new FormData().append('file', file);
                reader.onload = function (data) {
                    var src = data.target.result;
                    var size = ((file.size / (1024 * 1024)) > 1) ? (file.size / (1024 * 1024)) + ' mB' : (file.size / 1024) + ' kB';
                    scope.$apply(function () {
                        scope.previewData.push({
                            'name': file.name, 'size': size, 'type': file.type,
                            'src': src, 'data': obj
                        });
                    });
                    console.log(scope.previewData);
                }
                reader.readAsDataURL(file);
            }

            function uploadFile(e, type) {
                e.preventDefault();
                var files = "";
                if (type == "formControl") {
                    files = e.target.files;
                } else if (type === "drop") {
                    files = e.originalEvent.dataTransfer.files;
                }
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    if (file.type.indexOf("image") !== -1) {
                        previewFile(file);
                    } else {
                        alert(file.name + " is not supported");
                    }
                }
            }
            elem.find('.fileUpload').bind('change', function (e) {
                uploadFile(e, 'formControl');
            });

            elem.find('.dropzone').bind("click", function (e) {
                $compile(elem.find('.fileUpload'))(scope).trigger('click');
            });

            elem.find('.dropzone').bind("dragover", function (e) {
                e.preventDefault();
            });

            elem.find('.dropzone').bind("drop", function (e) {
                uploadFile(e, 'drop');
            });

            scope.upload = function (obj) {

                var propiedadActual = $("#identificadorPropImagenes").val();

                $http.post("/Administrador/Administrador/GuardarImagen",
                    {
                        'idPropiedad': propiedadActual,
                        'nombre': obj.name
                    }

                ).then(function successCallback(result) {

                    var propiedadActual = $("#identificadorPropImagenes").val();

                    swal("Home Switch Home", result.data, "success");

                    $http.post("/Administrador/Administrador/ObtenerImagenesDePropiedad",
                        {
                            'idPropiedad': propiedadActual
                        }

                    ).then(function successCallback(response) {

                        $scope.imagenes = response.data;

                    })

                }, function errorCallback(jqXHR) {
                    swal("Home Switch Home", jqXHR.data, "error");

                });
            }

            scope.remove = function (data) {
                var index = scope.previewData.indexOf(data);
                scope.previewData.splice(index, 1);
            }
        }
    }
});