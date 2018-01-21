import { Component, OnInit, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { BaseTableRowComponent } from '../../basecomponent/base-table-row/base-table-row.component';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: '[app-project-member-table-row]',
  templateUrl: './project-member-table-row.component.html',
  styleUrls: ['./project-member-table-row.component.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ProjectMemberTableRowComponent),
    multi: true
  }]
})
export class ProjectMemberTableRowComponent extends BaseTableRowComponent {

}