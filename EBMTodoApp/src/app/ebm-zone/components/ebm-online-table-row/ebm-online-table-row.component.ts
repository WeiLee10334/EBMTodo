import { Component, OnInit, forwardRef } from '@angular/core';
import { BaseTableRowComponent } from '../../basecomponent/base-table-row/base-table-row.component';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: '[app-ebm-online-table-row]',
  templateUrl: './ebm-online-table-row.component.html',
  styleUrls: ['./ebm-online-table-row.component.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => EbmOnlineTableRowComponent),
    multi: true
  }]
})
export class EbmOnlineTableRowComponent extends BaseTableRowComponent {
  Editable = true;
}
