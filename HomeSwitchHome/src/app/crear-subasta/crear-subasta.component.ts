import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-crear-subasta',
  templateUrl: './crear-subasta.component.html',
  styleUrls: ['./crear-subasta.component.css']
})

export class CrearSubastaComponent implements OnInit {

  changed = false;

  constructor() { }

  ngOnInit() {
  }

  hasChanged() {
    this.changed = true;
    if ((<HTMLInputElement>document.getElementById("title")).value == '') {
      this.changed = false;
    }
  }

}
