import { Component, OnInit } from '@angular/core';
import { residencias } from '../app.component';
import { subastas } from '../app.component';
import { Subasta } from '../subasta';
import { Residencia } from '../residencia';

@Component({
  selector: 'app-crear-subasta',
  templateUrl: './crear-subasta.component.html',
  styleUrls: ['./crear-subasta.component.css']
})

export class CrearSubastaComponent implements OnInit {

  changed = false;
  showForm = true;
  showResidence = false;
  titleRes = '';
  residencia: Residencia;

  constructor() { }

  ngOnInit() {
  }

  hasChanged() {
    this.changed = true;
    this.titleRes = (<HTMLInputElement>document.getElementById("title")).value
    if (this.titleRes == '') {
      this.changed = false;
    }
    else {
       for (let i in residencias) {
         if (residencias[i].title == this.titleRes) {
           this.showResidence = true;
           this.residencia = residencias[i];
           break;
         }
         else this.showResidence = false;
       }
    }
  }

  getInputs() {
    var valor = +(<HTMLInputElement> document.getElementById("valor")).value;
    var semana = (<HTMLInputElement> document.getElementById("semana")).value;
    subastas.push(new Subasta(this.residencia, valor, new Date(semana)));
    this.showForm = false;
  }

}
