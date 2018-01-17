import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { DataStoreService } from '../../../shared/services/data-store.service';

@Component({
  selector: 'app-project-todolist-card',
  templateUrl: './project-todolist-card.component.html',
  styleUrls: ['./project-todolist-card.component.scss']
})
export class ProjectTodolistCardComponent implements OnInit {

  @Input() ProjectTodoList: any;
  @Output() stateChanged = new EventEmitter<any>();
  Editable = false;

  constructor(private api: DataStoreService) { }

  ngOnInit() {
    if (!this.ProjectTodoList.PTLID) {
      this.Editable = true;
    }
  }
  Edit() {
    this.Editable = true;
  }
  Save() {
    console.log(this.ProjectTodoList);
    if (!this.ProjectTodoList.PTLID) {
      this.api.projectTodoListCreate(this.ProjectTodoList).subscribe(
        (data) => {
          this.stateChanged.emit(data)
          this.Editable = false;
        },
        (err) => {
          console.log(err);
        }
      )
    }
    else {
      this.api.projectTodoListUpdate(this.ProjectTodoList).subscribe(
        (data) => {
          this.stateChanged.emit(data)
          this.Editable = false;
        },
        (err) => {
          console.log(err);
        }
      )
    }

  }
  Delete() {
    if (!this.ProjectTodoList.PTLID) {
      this.stateChanged.emit(null)
    }
    else {
      if (confirm("確定刪除?")) {
        this.api.projectTodoListDelete(this.ProjectTodoList).subscribe(
          (data) => {
            this.stateChanged.emit(null)
          },
          (err) => {
            console.log(err);
          }
        )

      }

    }
  }
}
