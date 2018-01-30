import { Component, OnInit, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { BaseTableRowComponent } from '../../basecomponent/base-table-row/base-table-row.component';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

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
export class EbmMemoCardComponent extends BaseTableRowComponent {
  isCollapsed = true;
  @Output() actionChanged = new EventEmitter<any>();
  emit(action: string) {
    this.Editable = false;
    this.actionChanged.emit(action);
  }

}