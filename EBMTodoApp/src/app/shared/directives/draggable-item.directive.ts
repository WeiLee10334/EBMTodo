import { Directive, ElementRef, Renderer2, OnInit, HostListener } from '@angular/core';
import { AfterViewInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/concatAll';
declare var $: any;
@Directive({
  selector: '[appDraggableItem]'
})
export class DraggableItemDirective implements OnInit, AfterViewInit {
  ngOnInit(): void {
    //this.renderer.setAttribute(this.elementRef.nativeElement, 'draggable', 'true');
  }
  ngAfterViewInit(): void {
    const dragDOM = $(this.elementRef.nativeElement);
    const body = document.body;

    const mouseDown = Observable.fromEvent(dragDOM, 'mousedown');
    const mouseUp = Observable.fromEvent(body, 'mouseup');
    const mouseMove = Observable.fromEvent(body, 'mousemove');
    mouseDown
      .map(event => mouseMove.takeUntil(mouseUp))
      .concatAll()
      .map((event: any) => ({ x: event.clientX, y: event.clientY + window.scrollY }))
      .subscribe(pos => {
        console.log(pos, dragDOM.offset());
        this.renderer.setStyle(this.elementRef.nativeElement, 'left', pos.x + 'px');
        this.renderer.setStyle(this.elementRef.nativeElement, 'top', pos.y + 'px');
      })
  }


  constructor(private elementRef: ElementRef, private renderer: Renderer2) { }

}
