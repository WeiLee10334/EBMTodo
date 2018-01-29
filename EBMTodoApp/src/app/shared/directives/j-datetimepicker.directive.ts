import { Directive, ElementRef, AfterViewInit, HostListener } from '@angular/core';
import { NgModel } from '@angular/forms';
import * as moment from 'moment';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
declare var $: any;
@Directive({
  selector: '[appJDatetimepicker]'
})
export class JDatetimepickerDirective implements AfterViewInit {
  ngAfterViewInit(): void {

    $(this.el.nativeElement).datepicker({
      dateFormat: "yy-mm-dd",
      onSelect: (e) => {
        this.model.control.setValue(e);
      }
    });

    // setInterval(() => {
    //   console.log(this.model.control.value);
    // }, 1000)
  }


  constructor(private el: ElementRef, private model: NgModel) {

  }
}
