import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DataStoreService } from '../../../shared/services';

@Component({
  selector: '[app-project-member-table-row]',
  templateUrl: './project-member-table-row.component.html',
  styleUrls: ['./project-member-table-row.component.scss']
})
export class ProjectMemberTableRowComponent implements OnInit {
  @Input() ProjectMember: any;
  @Output() stateChanged = new EventEmitter<any>();
  Editable = false;

  User: any;
  constructor(private api: DataStoreService) { }

  ngOnInit() {
    if (!this.ProjectMember.PMID) {
      this.Editable = true;
    }
  }
  Edit() {
    this.Editable = true;
  }
  Save() {
    console.log(this.ProjectMember);
    if (!this.ProjectMember.PMID) {
      this.api.projectMemberCreate(this.ProjectMember).subscribe(
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
      this.api.projectMemberUpdate(this.ProjectMember).subscribe(
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
    if (!this.ProjectMember.PMID) {
      this.stateChanged.emit(null)
    }
    else {
      if (confirm("確定刪除?")) {
        this.api.projectMemberDelete(this.ProjectMember).subscribe(
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