import { Component, OnInit, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { BaseTableRowComponent } from '../../basecomponent/base-table-row/base-table-row.component';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: '[app-project-table-row]',
  templateUrl: './project-table-row.component.html',
  styleUrls: ['./project-table-row.component.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ProjectTableRowComponent),
    multi: true
  }]
})
export class ProjectTableRowComponent extends BaseTableRowComponent {

}

