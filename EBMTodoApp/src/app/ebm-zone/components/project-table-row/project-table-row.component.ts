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
  @Output() deleted = new EventEmitter<any>();
  @Output() added = new EventEmitter<any>();
  Editable = false;
  Project: any;

  constructor(private api: DataStoreService) { }

  ngOnInit() {
  }
  Edit() {
    this.Editable = true;
  }
  Save() {
    if (!this.Project.POID) {
      this.api.projectCreate(this.Project).subscribe(
        (data) => {
          this._Project = data;
          this.Editable = false;
          this.added.emit(this.Project);
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
        },
        (err) => {
          console.log(err);
        }
      )
    }

  }
  Delete() {
    this.api.projectDelete(this.Project).subscribe(
      (data) => {
        this.deleted.emit(this);
      },
      (err) => {
        console.log(err);
      }
    )

  }
}
