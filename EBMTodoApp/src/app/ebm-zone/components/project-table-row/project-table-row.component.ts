import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DataStoreService } from '../../../shared/services';

@Component({
  selector: '[app-project-table-row]',
  templateUrl: './project-table-row.component.html',
  styleUrls: ['./project-table-row.component.scss']
})
export class ProjectTableRowComponent implements OnInit {
  @Input() set _Project(value) {
    this.Project = value;
    if (!value.PID) {
      this.Editable = true;
    }
  }
  @Output() change = new EventEmitter<any>();
  Editable = false;
  Project: any;

  constructor(private api: DataStoreService) { }

  ngOnInit() {
  }
  Edit() {
    this.Editable = true;
  }
  Save() {
    if (!this.Project.PID) {
      this.api.projectCreate(this.Project).subscribe(
        (data) => {
          this._Project = data;
          this.change.emit({
            type: Project_Operation.Insert,
            data: data
          })
          this.Editable = false;
        },
        (err) => {
          console.log(err);
        }
      )
    }
    else {
      this.api.projectUpdate(this.Project).subscribe(
        (data) => {
          this._Project = data;
          this.Editable = false;
          this.change.emit({
            type: Project_Operation.Update,
            data: data
          })
        },
        (err) => {
          console.log(err);
        }
      )
    }

  }
  Delete() {
    if (!this.Project.PID) {
      this.change.emit({
        type: Project_Operation.DeletePending,
        data: this.Project
      })
    }
    else {
      if (confirm("確定刪除?")) {
        this.api.projectDelete(this.Project).subscribe(
          (data) => {
            this.change.emit({
              type: Project_Operation.Delete,
              data: this.Project
            })
          },
          (err) => {
            console.log(err);
          }
        )
      }

    }

  }
}
export enum Project_Operation {
  Insert,
  Update,
  Delete,
  DeletePending
}
