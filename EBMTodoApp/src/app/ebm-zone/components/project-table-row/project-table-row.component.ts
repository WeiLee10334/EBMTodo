import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DataStoreService } from '../../../shared/services';

@Component({
  selector: '[app-project-table-row]',
  templateUrl: './project-table-row.component.html',
  styleUrls: ['./project-table-row.component.scss']
})
export class ProjectTableRowComponent implements OnInit {
  @Input() Project: any;
  @Output() stateChanged = new EventEmitter<any>();
  Editable = false;

  constructor(private api: DataStoreService) { }

  ngOnInit() {
    if (this.Project && !this.Project.PID) {
      this.Editable = true;
    }
  }
  Edit() {
    this.Editable = true;
  }
  Save() {
    if (!this.Project.PID) {
      this.api.projectCreate(this.Project).subscribe(
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
      this.api.projectUpdate(this.Project).subscribe(
        (data) => {
          this.stateChanged.emit(data);
          this.Editable = false;
        },
        (err) => {
          console.log(err);
        }
      )
    }

  }
  Delete() {
    if (!this.Project.PID) {
      this.stateChanged.emit(null)
    }
    else {
      if (confirm("確定刪除?")) {
        this.api.projectDelete(this.Project).subscribe(
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

