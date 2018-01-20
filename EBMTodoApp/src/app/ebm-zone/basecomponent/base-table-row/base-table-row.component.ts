import { Component, OnInit, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: '[app-base-table-row]',
  templateUrl: './base-table-row.component.html',
  styleUrls: ['./base-table-row.component.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => BaseTableRowComponent),
    multi: true
  }]
})
export class BaseTableRowComponent implements ControlValueAccessor {
  //The internal data model
  private innerValue: any = '';
  
  //Placeholders for the callbacks which are later provided
  //by the Control Value Accessor
  private onTouchedCallback: () => void = () => { };
  protected onChangeCallback: (_: any) => void = () => { };

  //get accessor
  get value(): any {
    return this.innerValue;
  };

  //set accessor including call the onchange callback
  set value(v: any) {
    if (v !== this.innerValue) {
      this.innerValue = v;
      this.onChangeCallback(v);
    }
  }

  //Set touched on blur
  onBlur() {
    this.onTouchedCallback();
  }

  //From ControlValueAccessor interface
  writeValue(value: any) {
    if (value !== this.innerValue) {
      this.innerValue = value;
    }
  }

  //From ControlValueAccessor interface
  registerOnChange(fn: any) {
    this.onChangeCallback = fn;
  }

  //From ControlValueAccessor interface
  registerOnTouched(fn: any) {
    this.onTouchedCallback = fn;
  }
  setDisabledState(isDisabled: boolean): void {
    console.log('setDisabled', isDisabled);
  }
  @Input() Columns: any[];
  @Input() Editable = true;
  modelChanged(event, prop) {
    this.innerValue[prop] = event;
    this.onChangeCallback(this.innerValue);
  }
}