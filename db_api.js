(function () {
  'use strict';

  angular
      .module('app')
      .factory('dbApiService', dbApiService);

function dbApiService(){

  var Connection = require('tedious').Connection;
  var Request = require('tedious').Request;
  var TYPES = require('tedious').TYPES;

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

var service = {};

  service.GetData = GetData;
  service.AddUser = AddUser;
  service.AddPropiedad = AddPropiedad;
  service.AddSubasta = AddSubasta;
  service.PujarSubasta = PujarSubasta;

  return service;

function GetData(aQuery){

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
      
      request = new Request(aQuery, function (err, rowCount) {
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

function AddUser(aQuery){

  connection = new Connection(config);
  connection.on('connect', function(err) 
  {  
    if (err) {
      console.log(err);
    } 
    else {
      console.log(sprintf('Starting POST Query'));

      request = new Request(aQuery, function(err) 
      {
        if (err) {  
          console.log(err);
        }
        connection.close();
      }); 
    
      request.addParameter('UserName', TYPES.NVarChar);  
      request.addParameter('Password', TYPES.NVarChar); 
        
      request.on('row', function(columns) {  
      
        columns.forEach(function(column) {
            if (column.value === null) {  
                console.log('NULL');  
              } 
            else {  
                console.log("COLUMN VALUE:" + column.value);  
              }  
        });  
      });

      connection.execSql(request);
    }    
  });
}

function AddPropiedad(aQuery){
  connection = new Connection(config);
  connection.on('connect', function(err) 
  {  
    if (err) {
      console.log(err);
    } 
    else {
      console.log('Starting POST Query');

      request = new Request(aQuery, function(err) 
      {
        if (err) {  
          console.log(err);
        }
        connection.close();
      }); 
    
      request.addParameter('Nombre', TYPES.NVarChar);  
      request.addParameter('Descripcion', TYPES.NVarChar);
      request.addParameter('Direccion', TYPES.NVarChar);
      request.addParameter('Pais', TYPES.NVarChar);
        
      request.on('row', function(columns) {  
      
        columns.forEach(function(column) {
            if (column.value === null) {  
                console.log('NULL');  
              } 
            else {  
                console.log("COLUMN VALUE:" + column.value);  
              }  
        });  
      });

      connection.execSql(request);
    }    
  });
}

function AddSubasta(aQuery){
  connection = new Connection(config);
  connection.on('connect', function(err) 
  {  
    if (err) {
      console.log(err);
    } 
    else {
      console.log('Starting POST Query');

      request = new Request(aQuery, function(err) 
      {
        if (err) {  
          console.log(err);
        }
        connection.close();
      }); 

      request.addParameter('IdPropiedad', TYPES.Int);  
      request.addParameter('FechaComienzo', TYPES.Date);
      request.addParameter('ValorMinimo', TYPES.Decimal);
      request.addParameter('ValorActual', TYPES.Decimal);
        
      request.on('row', function(columns) {  
      
        columns.forEach(function(column) {
            if (column.value === null) {  
                console.log('NULL');  
              } 
            else {  
                console.log("COLUMN VALUE:" + column.value);  
              }  
        });  
      });

      connection.execSql(request);
    }    
  });
}

function PujarSubasta(aQuery){
  connection = new Connection(config);
  connection.on('connect', function(err) 
  {  
    if (err) {
      console.log(err);
    } 
    else {
      console.log('Starting POST Query');

      request = new Request(aQuery, function(err) 
      {
        if (err) {  
          console.log(err);
        }
        connection.close();
      }); 

      request.addParameter('ValorActual', TYPES.Decimal);  
      request.addParameter('IdCliente', TYPES.Int);
      request.addParameter('IdSubasta', TYPES.Int);
        
      request.on('row', function(columns) {  
      
        columns.forEach(function(column) {
            if (column.value === null) {  
                console.log('NULL');  
              } 
            else {  
                console.log("COLUMN VALUE:" + column.value);  
              }  
        });  
      });

      connection.execSql(request);
    }    
  });
}

}
});

