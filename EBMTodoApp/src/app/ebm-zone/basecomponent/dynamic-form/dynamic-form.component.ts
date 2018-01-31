import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';
import { Component, OnInit, forwardRef, Input } from '@angular/core';

export const DY_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => DynamicFormComponent),
  multi: true
};

@Component({
  selector: 'app-dynamic-form',
  templateUrl: './dynamic-form.component.html',
  styleUrls: ['./dynamic-form.component.scss'],
  providers: [DY_VALUE_ACCESSOR]
})
export class DynamicFormComponent implements ControlValueAccessor {
  protected innerValue: any;
  set value(v) {
    if (v !== this.innerValue) {
      this.innerValue = v;
      this.onChange(v);
    }
  }
  get value() {
    return this.innerValue;
  }

  @Input() disabled: boolean;
  onChange: (value) => {};
  onTouched: () => {};

  writeValue(obj: any): void {
    if (obj !== this.innerValue) {
      this.innerValue = obj;
    }
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
}
