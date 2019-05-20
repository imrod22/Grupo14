import { Component, OnInit } from '@angular/core';
import { residencias } from '../app.component';
import { Residencia } from '../residencia';
import { ubicaciones } from '../app.component';

@Component({
  selector: 'app-listar-residencias',
  templateUrl: './listar-residencias.component.html',
  styleUrls: ['./listar-residencias.component.css'],
})
export class ListarResidenciasComponent implements OnInit {

  arrayRes: Residencia[] = [];
  arrayResToShow: Residencia[] = [];
  ubicaciones: string[] = ubicaciones;
  residenciasFiltro: boolean[] = [];
  filtrar: boolean = false;

  constructor() { }

  ngOnInit() {
    this.inicializarResidencias();
    this.getInputs();
  }

  inicializarResidencias() {
    for (let i in residencias) {
      this.arrayRes[i] = residencias[i];
    }
  }

  getInputs() {
    var u = (<HTMLSelectElement>document.getElementById("ubicacion")).selectedOptions[0].text;
    if (!(u == "Elija la ubicación" || u == "Sin filtro")) {
      this.arrayResToShow = [];
      for (let i = 0; i < this.arrayRes.length; i++) {
        if (this.arrayRes[i].ubication == u) {
          this.arrayResToShow.push(this.arrayRes[i]);
        }
      }
    }
    else {
      this.arrayResToShow = this.arrayRes;
    }
  }
}
