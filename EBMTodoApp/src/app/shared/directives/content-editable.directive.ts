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
export class ContentEditableDirective implements ControlValueAccessor {
  private innerValue = '';
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
  @Input() disabled: boolean;
  private onChange: (value: string) => void;
  private onTouched: () => void;
  private removeDisabledState: () => void;

  constructor(private elementRef: ElementRef, private renderer: Renderer2, private datepipe: DatePipe) { }


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
          this.onChange(this.elementRef.nativeElement['innerText']);
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

        }
      }
      this.onTouched();
    }
  }

  writeValue(value: any): void {
    switch (this.type) {
      case 'date':
        this.innerValue = this.datepipe.transform(value, this.dateformat);
        break;
      default:
        this.innerValue = value;
        break;
    }
    this.renderer.setProperty(this.elementRef.nativeElement, 'innerText', this.innerValue || '');
    this.renderer.setAttribute(this.elementRef.nativeElement, 'contenteditable', this.disabled ? 'false' : 'true');
  }

  registerOnChange(fn: () => void): void {
    if (this.type === 'date') {
      this.calendar = this.renderer.createElement('div');
      this.renderer.setStyle(this.calendar, 'position', 'absolute');
      this.renderer.setStyle(this.calendar, 'top', $(this.elementRef.nativeElement).height() + 'px');
      this.renderer.setStyle(this.calendar, 'z-index', 1024);
      $(this.calendar).datepicker({
        dateFormat: "yy-mm-dd",
        onSelect: (e) => {
          this.renderer.removeChild(this.elementRef.nativeElement, this.calendar);
          this.innerValue = this.datepipe.transform(e, this.dateformat);
          this.renderer.setProperty(this.elementRef.nativeElement, 'innerText', this.innerValue || '');
          this.onChange(this.elementRef.nativeElement['innerText']);
        }
      });
    }
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
    this.renderer.setAttribute(this.elementRef.nativeElement, 'contenteditable', this.disabled ? 'false' : 'true');
  }
}