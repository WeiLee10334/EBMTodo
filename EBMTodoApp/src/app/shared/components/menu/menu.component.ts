import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
declare var $: any;
@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit, AfterViewInit {
  constructor() { }
  menuOpen = false;
  ngOnInit() {

  }
  ngAfterViewInit() {

  }
  openMenu() {
    document.getElementById("mySidenav").style.width = "250px";
    document.getElementById("main").style.marginLeft = "250px";
  }
  closeMenu() {
    document.getElementById("mySidenav").style.width = "0";
  }
}
