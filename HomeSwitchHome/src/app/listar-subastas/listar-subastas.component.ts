import { Component, OnInit } from '@angular/core';
import { subastas } from '../app.component';
import { Subasta } from '../subasta';
import { formatter } from '../app.component';
import { ubicaciones } from '../app.component';

@Component({
  selector: 'app-listar-subastas',
  templateUrl: './listar-subastas.component.html',
  styleUrls: ['./listar-subastas.component.css']
})
export class ListarSubastasComponent implements OnInit {

  arraySub: Subasta[] = [];
  arraySubToShow: Subasta[] = [];
  ubicaciones: string[] = ubicaciones;
  formatterAux = formatter; 
  canModify = false;

  constructor() { }

  ngOnInit() {
    this.inicializarSubastas();
    this.getInputs();
  }

  inicializarSubastas() {
    for (let i in subastas) {
      this.arraySub[i] = subastas[i];
    }
  }

  hoursLeft(sub: Subasta) {
    var one_day=1000*60*60*24;
    var difference_ms = sub.semana.getTime() - (new Date().getTime());
    return Math.round(difference_ms/one_day);
  }

  modify() {
    this.canModify = true;
  }
  modified(sub: Subasta) {
    this.canModify = false;
    sub.valor = +(<HTMLInputElement>document.getElementById("newValue")).value;
  }

  getInputs() {
    var u = (<HTMLSelectElement>document.getElementById("ubicacion")).value;
    if (!(u == "Elija la ubicación" || u == "Sin filtro")) {
      this.arraySubToShow = [];
      for (let i = 0; i < this.arraySub.length; i++) {
        if (this.arraySub[i].residencia.ubication == u) {
          this.arraySubToShow.push(this.arraySub[i]);
        }
      }
    }
    else {
      this.arraySubToShow = this.arraySub;
    }
  }

  cantSubWithUbication(u: string) {
    var cont = 0;
    for (let i = 0; i<this.arraySub.length; i++) {
      if (this.arraySub[i].residencia.ubication == u) cont++
    }
    return cont;
  }

}
