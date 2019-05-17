import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-crear-residencia',
  templateUrl: './crear-residencia.component.html',
  styleUrls: ['./crear-residencia.component.css']
})
export class CrearResidenciaComponent implements OnInit {

  title: string = null;
  ubication: string = null;
  showForm: boolean = true;
  
  constructor() { }

  ngOnInit() {
  }

  getInputs() {
    this.title = (<HTMLInputElement> document.getElementById("title")).value;
    this.ubication = (<HTMLInputElement> document.getElementById("ubication")).value;
    this.showForm = false;
  }

}
