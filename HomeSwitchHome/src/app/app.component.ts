import { Component } from '@angular/core';
import * as $ from 'jquery';
import { Residencia } from './residencia';
import { Subasta } from './subasta';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'HomeSwitchHome';
}

export var residencias = [
  new Residencia("La casa de Tax", "Gonnet", "Una de las mejores residencias del planeta."),
  new Residencia("La choza de Robert", "Olmos", "Si te gusta lo rústico."),  
  new Residencia("El remanso", "Corrientes", "El río te va a encantar."),
  new Residencia("SPA & Resort 'The dude'", "Miami Beach", "It's amazing, you're gonna wan't to come back.")
]

export var subastas = [
  new Subasta(residencias[0], 500000, (new Date(2019, 4, 19)))
]


