import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-crear-residencia',
  templateUrl: './crear-residencia.component.html',
  styleUrls: ['./crear-residencia.component.css']
})
export class CrearResidenciaComponent implements OnInit {

  title: string = null;
  ubication: string = null;
  show: boolean = false;
  
  constructor() { }

  ngOnInit() {
  }

  getInputs() {
    this.title = (<HTMLInputElement> document.getElementById("title")).value;
    this.ubication = (<HTMLInputElement> document.getElementById("ubication")).value;
    this.show = true;

  }

}
