import { Component, OnInit, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { BaseTableRowComponent } from '../../basecomponent/base-table-row/base-table-row.component';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-project-todolist-card',
  templateUrl: './project-todolist-card.component.html',
  styleUrls: ['./project-todolist-card.component.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ProjectTodolistCardComponent),
    multi: true
  }]
})
export class ProjectTodolistCardComponent extends BaseTableRowComponent {
  isCollapsed = true;
  @Output() actionChanged = new EventEmitter<any>();
  emit(action: string) {
    this.actionChanged.emit(action);
  }

}

