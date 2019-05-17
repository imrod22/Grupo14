import { Component, OnInit } from '@angular/core';
import { residencias } from '../app.component';
import { Residencia } from '../residencia';

@Component({
  selector: 'app-listar-residencias',
  templateUrl: './listar-residencias.component.html',
  styleUrls: ['./listar-residencias.component.css'],
})
export class ListarResidenciasComponent implements OnInit {

  mostrar = false;
  arrayRes: Residencia[] = [];

  constructor() { }

  ngOnInit() {
    this.inicializarResidencias();
  }

  listarResidencias() {
    this.mostrar = true;
  }

  inicializarResidencias() {
    for (let i in residencias) {
      this.arrayRes[i] = residencias[i];
    }
  }

}
