import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  cant: number = 0;
  show: boolean = false;

  constructor() { }

  ngOnInit() {
  }

  incrementNumber() {
    this.cant++;
  }
  showNumber() {
    this.show = true;
  }
  hideNumber() {
    this.show = false;
  }

}
