(function () {
    'use strict';

    angular
        .module('app')
        .factory('PropiedadService', PropiedadService);

    PropiedadService.$inject = ['$filter', '$q'];

    function PropiedadService($filter, $q) {

        var service = {};
        var Connection = import('tedious').Connection;
        var Request = import('tedious').Request;
        var TYPES = import('tedious').TYPES;

        var config = {
            server: 'localhost',
            authentication: {
              type: 'default',
              options: {
                  userName: 'sa',
                  password: 'lobo22' 
                }
            },
            options: {
                database: 'HomeSwitchHome'
            }
        };

        service.GetAll = GetAll;
        service.GetById = GetById;
        service.GetByName = GetByName;

        return service;

        function GetAll() {
            var deferred = $q.defer();
            deferred.resolve(getPropiedades());
            return deferred.promise;
        }

        function GetById(id) {
            var deferred = $q.defer();
            var filtered = $filter('filter')(getPropiedades(), { id: id });
            var user = filtered.length ? filtered[0] : null;
            deferred.resolve(user);
            return deferred.promise;
        }

        function GetByName(prop) {
            var deferred = $q.defer();
            var filtered = $filter('filter')(getPropiedades(), { Nombre: nombre });
            var user = filtered.length ? filtered[0] : null;
            deferred.resolve(user);
            return deferred.promise;
        }

        function getPropiedades() {
           
            var rows = [];
            var row = {};
          
            connection = new Connection(config);
            connection.on('connect', function(err) 
            {  
              if (err) {
                console.log(err);
              } 
              else {
                console.log('CONNECT TO DATABASE...');
                
                request = new Request("SELECT * FROM PROPIEDADES", function (err, rowCount) {
                    if (err) {
                      console.log(err);
                    }
                    else {
                      console.log('RESPONSE OK.');
                    }
                    connection.close();
                });
          
                request.on('row', function(columns) {
          
                columns.forEach(function(column) {
                  row[column.metadata.colName] = column.value;
                });
          
                rows.push(row);
          
                console.log(rows);
          
                return rows;
                
              });
          
              connection.execSql(request);
            }
          });
          
          }

        function setUsers(users) {
            localStorage.users = JSON.stringify(users);          
        }
    }
})();