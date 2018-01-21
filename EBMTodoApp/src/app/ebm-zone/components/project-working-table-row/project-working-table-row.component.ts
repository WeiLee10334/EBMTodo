import { Component, OnInit, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { BaseTableRowComponent } from '../../basecomponent/base-table-row/base-table-row.component';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
@Component({
  selector: '[app-project-working-table-row]',
  templateUrl: './project-working-table-row.component.html',
  styleUrls: ['./project-working-table-row.component.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ProjectWorkingTableRowComponent),
    multi: true
  }]
})
export class ProjectWorkingTableRowComponent extends BaseTableRowComponent {
  Editable = false;
}
