import { Component, OnInit, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { BaseTableRowComponent } from '../../basecomponent/base-table-row/base-table-row.component';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: '[app-user-table-row]',
  templateUrl: './user-table-row.component.html',
  styleUrls: ['./user-table-row.component.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => UserTableRowComponent),
    multi: true
  }]
})
export class UserTableRowComponent extends BaseTableRowComponent {

}
