import { Component, OnInit } from '@angular/core';
import { trigger, state, style, transition, animate, query } from '@angular/animations';
export const RoutingAnimation = trigger('routerAnimation', [
  transition('* <=> *', [
    // Initial state of new route
    query(':enter',
      style({
        position: 'fixed',
        width: '100%',
        opacity: '0',
        //transform: 'translateY(100%)'
      }),
      { optional: true }),
    query(':leave',
      animate('300ms ease-out',
        style({
          position: 'fixed',
          width: '100%',
          opacity: '0',
          //transform: 'translateY(-100%)'
        })
      ),
      { optional: true }),
    query(':enter',
      animate('500ms ease',
        style({
          opacity: 1
        })
      ),
      { optional: true }),
  ])
]);
@Component({
  selector: 'app-ebm-zone',
  templateUrl: './ebm-zone.component.html',
  styleUrls: ['./ebm-zone.component.scss'],
  animations: [
    RoutingAnimation
  ]
})
export class EbmZoneComponent implements OnInit {
  constructor() { }

  ngOnInit() {
  }
  getRouteAnimation(outlet) {
    return outlet.activated
  }
}
