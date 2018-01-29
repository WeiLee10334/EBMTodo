import { Directive, Input, ElementRef, HostListener, OnInit, forwardRef, Renderer2 } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { AfterViewInit } from '@angular/core/src/metadata/lifecycle_hooks';
declare var $: any;
@Directive({
  selector: '[appContentEditable]',
  providers:
    [
      { provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => ContentEditableDirective), multi: true },
      DatePipe
    ]
})
export class ContentEditableDirective implements ControlValueAccessor, OnInit, AfterViewInit {
  private innerValue = '';
  @Input() propValueAccessor: string = 'innerText';
  @Input() type: string = 'text';
  @Input() dateformat: string = 'yyyy-MM-dd';
  private calendar;
  /**
   * This property is deprecated, use `propValueAccessor` instead.
   * 
   * See [#7](https://github.com/KostyaTretyak/ng-contenteditable/issues/7);
   * 
   * @deprecated
   */
  @Input() propValueAccesor: string;

  private onChange: (value: string) => void;
  private onTouched: () => void;
  private removeDisabledState: () => void;

  constructor(private elementRef: ElementRef, private renderer: Renderer2, private datepipe: DatePipe) { }
  ngAfterViewInit() {

  }
  ngOnInit() {
    this.propValueAccessor = this.propValueAccesor || this.propValueAccessor;
  }

  @HostListener('focus', ['$event'])
  callOnFocus(event) {
    if (this.type === 'date') {
      this.renderer.appendChild(this.elementRef.nativeElement, this.calendar);
    }
  }

  @HostListener('keyup', ['$event'])
  callOnChange(event) {
    if (typeof this.onChange == 'function') {
      switch (this.type) {
        default:
          this.onChange(this.elementRef.nativeElement[this.propValueAccessor]);
          break;
      }

    }
  }

  @HostListener('blur')
  callOnTouched() {
    if (typeof this.onTouched == 'function') {
      if (this.type === 'date') {
        try {
          this.renderer.removeChild(this.elementRef.nativeElement, this.calendar);
        }
        catch (e) {
          console.log(e);
        }
      }
      this.onTouched();
    }

  }

  /**
   * Writes a new value to the element.
   * This method will be called by the forms API to write
   * to the view when programmatic (model -> view) changes are requested.
   * 
   * See: [ControlValueAccessor](https://angular.io/api/forms/ControlValueAccessor#members)
   */
  writeValue(value: any): void {
    switch (this.type) {
      case 'date':
        this.innerValue = this.datepipe.transform(value, this.dateformat);
        break;
      default:
        this.innerValue = value;
        break;
    }

    this.renderer.setProperty(this.elementRef.nativeElement, this.propValueAccessor, this.innerValue || '');
  }

  /**
   * Registers a callback function that should be called when
   * the control's value changes in the UI.
   * 
   * This is called by the forms API on initialization so it can update
   * the form model when values propagate from the view (view -> model).
   */
  registerOnChange(fn: () => void): void {
    if (this.type === 'date') {
      this.calendar = this.renderer.createElement('div');
      this.renderer.setStyle(this.calendar, 'position', 'absolute');
      this.renderer.setStyle(this.calendar, 'top', $(this.elementRef.nativeElement).height() + 'px');
      this.renderer.setStyle(this.calendar, 'z-index', 1024);
      $(this.calendar).datepicker({
        dateFormat: "yy-mm-dd",
        onSelect: (e) => {
          console.log(e)
          this.renderer.removeChild(this.elementRef.nativeElement, this.calendar);
          this.innerValue = this.datepipe.transform(e, this.dateformat);
          this.renderer.setProperty(this.elementRef.nativeElement, this.propValueAccessor, this.innerValue || '');
          this.onChange(this.elementRef.nativeElement[this.propValueAccessor]);
        }
      });
    }
    this.onChange = fn;
  }

  /**
   * Registers a callback function that should be called when the control receives a blur event.
   * This is called by the forms API on initialization so it can update the form model on blur.
   */
  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  /**
   * This function is called by the forms API when the control status changes to or from "DISABLED".
   * Depending on the value, it should enable or disable the appropriate DOM element.
   */
  setDisabledState(isDisabled: boolean): void {
    console.log('set disabled', isDisabled);
    if (isDisabled) {
      this.renderer.setAttribute(this.elementRef.nativeElement, 'disabled', 'true');
      this.removeDisabledState = this.renderer.listen(this.elementRef.nativeElement, 'keydown', this.listenerDisabledState);
    }
    else {
      if (this.removeDisabledState) {
        this.renderer.removeAttribute(this.elementRef.nativeElement, 'disabled');
        this.removeDisabledState();
      }
    }
  }

  private listenerDisabledState(e: KeyboardEvent) {
    e.preventDefault();
  }
}