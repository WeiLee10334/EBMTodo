import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DataStoreService } from '../../../shared/services';

@Component({
  selector: '[app-project-member-table-row]',
  templateUrl: './project-member-table-row.component.html',
  styleUrls: ['./project-member-table-row.component.scss']
})
export class ProjectMemberTableRowComponent implements OnInit {
  @Input() set _ProjectMember(value) {
    this.ProjectMember = value;
    if (!value.PID) {
      this.Editable = true;
    }
  }
  @Output() deleted = new EventEmitter<any>();
  @Output() added = new EventEmitter<any>();
  Editable = false;
  ProjectMember: any;

  constructor(private api: DataStoreService) { }

  ngOnInit() {
  }
  Edit() {
    this.Editable = true;
  }
  Save() {
    if (!this.ProjectMember.POID) {
      this.api.projectCreate(this.ProjectMember).subscribe(
        (data) => {
          this._ProjectMember = data;
          this.Editable = false;
          this.added.emit(this.ProjectMember);
        },
        (err) => {
          console.log(err);
        }
      )
    }
    else {
      this.api.projectUpdate(this.ProjectMember).subscribe(
        (data) => {
          this._ProjectMember = data;
          this.Editable = false;
        },
        (err) => {
          console.log(err);
        }
      )
    }

  }
  Delete() {
    this.api.projectDelete(this.ProjectMember).subscribe(
      (data) => {
        this.deleted.emit(this);
      },
      (err) => {
        console.log(err);
      }
    )

  }
}
