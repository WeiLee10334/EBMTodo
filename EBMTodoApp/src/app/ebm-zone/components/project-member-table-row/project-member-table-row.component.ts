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
    this.User = {
      Id: value.Id,
      UserName: value.UserName
    }
    if (!value.PMID) {
      this.Editable = true;
    }
  }
  @Output() change = new EventEmitter<any>();
  Editable = false;
  ProjectMember: any;
  User: any;
  constructor(private api: DataStoreService) { }

  ngOnInit() {
  }
  changeUser(event) {
    this.ProjectMember['Id'] = event['Id'];
    this.ProjectMember['UserName'] = event['UserName'];
  }
  Edit() {
    this.Editable = true;
  }
  Save() {
    console.log(this.ProjectMember);
    if (!this.ProjectMember.PMID) {
      this.api.projectMemberCreate(this.ProjectMember).subscribe(
        (data) => {
          this._ProjectMember = data;
          this.change.emit({
            type: ProjectMember_Operation.Insert,
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
      this.api.projectMemberUpdate(this.ProjectMember).subscribe(
        (data) => {
          this._ProjectMember = data;
          this.Editable = false;
          this.change.emit({
            type: ProjectMember_Operation.Update,
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
    if (!this.ProjectMember.PMID) {
      this.change.emit({
        type: ProjectMember_Operation.DeletePending,
        data: this.ProjectMember
      })
    }
    else {
      if (confirm("確定刪除?")) {
        this.api.projectMemberDelete(this.ProjectMember).subscribe(
          (data) => {
            this.change.emit({
              type: ProjectMember_Operation.Delete,
              data: this.ProjectMember
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
export enum ProjectMember_Operation {
  Insert,
  Update,
  Delete,
  DeletePending
}
