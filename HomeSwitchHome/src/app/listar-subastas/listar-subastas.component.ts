import { Component, OnInit } from '@angular/core';
import { subastas } from '../app.component';
import { Subasta } from '../subasta';

@Component({
  selector: 'app-listar-subastas',
  templateUrl: './listar-subastas.component.html',
  styleUrls: ['./listar-subastas.component.css']
})
export class ListarSubastasComponent implements OnInit {

  arraySub: Subasta[] = [];

  constructor() { }

  ngOnInit() {
    this.inicializarSubastas();
  }

  inicializarSubastas() {
    for (let i in subastas) {
      this.arraySub[i] = subastas[i];
    }
    console.log(this.hoursLeft(this.arraySub[0]));
  }

  hoursLeft(sub: Subasta) {
    var f = new Date(2020, 1, 1);
    console.log("value of:", (f.valueOf).toString());
    console.log("getDay: ", (f.getDay).toString());
    
    return (sub.semana.valueOf() - (new Date().valueOf())) / 24 / 30;
  }

}
