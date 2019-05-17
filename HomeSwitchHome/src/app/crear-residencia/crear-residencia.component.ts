import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { residencias } from '../app.component';
import { Residencia } from '../residencia';

@Component({
  selector: 'app-crear-residencia',
  templateUrl: './crear-residencia.component.html',
  styleUrls: ['./crear-residencia.component.css']
})
export class CrearResidenciaComponent implements OnInit {

  title: string = null;
  ubication: string = null;
  description: string = null;
  showForm: boolean = true;
  
  constructor() { }

  ngOnInit() {
  }

  getInputs() {
    this.title = (<HTMLInputElement> document.getElementById("title")).value;
    this.ubication = (<HTMLInputElement> document.getElementById("ubication")).value;
    this.description = (<HTMLInputElement> document.getElementById("description")).value;
    residencias.push(new Residencia(this.title, this.ubication, this.description));
    this.showForm = false;
  }

}
