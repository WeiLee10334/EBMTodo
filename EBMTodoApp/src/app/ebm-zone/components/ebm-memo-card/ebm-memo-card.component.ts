import { Component, OnInit, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { DynamicFormComponent } from '../../basecomponent/dynamic-form/dynamic-form.component';

@Component({
  selector: 'app-ebm-memo-card',
  templateUrl: './ebm-memo-card.component.html',
  styleUrls: ['./ebm-memo-card.component.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => EbmMemoCardComponent),
    multi: true
  }]
})
export class EbmMemoCardComponent extends DynamicFormComponent {
  @Output() actionChanged = new EventEmitter<any>();
  emit(action: string) {
    this.disabled = true;
    this.actionChanged.emit(action);
  }
}