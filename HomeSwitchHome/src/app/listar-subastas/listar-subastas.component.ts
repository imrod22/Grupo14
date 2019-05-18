import { Component, OnInit } from '@angular/core';
import { subastas } from '../app.component';
import { Subasta } from '../subasta';
import { formatter } from '../app.component';

@Component({
  selector: 'app-listar-subastas',
  templateUrl: './listar-subastas.component.html',
  styleUrls: ['./listar-subastas.component.css']
})
export class ListarSubastasComponent implements OnInit {

  arraySub: Subasta[] = [];
  formatterAux = formatter; 
  canModify = false;

  constructor() { }

  ngOnInit() {
    this.inicializarSubastas();
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
}
