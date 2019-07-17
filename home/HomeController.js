(function () {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$rootScope', 'PropiedadService'];

    function HomeController($rootScope, PropiedadService) {
        var model = this;   
        
        model.propiedades = null;

        initController();

        function initController() {
            LoadAllPropiedades();            
        }

        function LoadAllPropiedades() {
            PropiedadService.GetAll()
                .then(function (propiedades) {
                    model.propiedades = propiedades;
                });
        }
    }
})();