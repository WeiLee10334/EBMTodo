import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DataStoreService } from '../../../shared/services';

@Component({
  selector: '[app-user-table-row]',
  templateUrl: './user-table-row.component.html',
  styleUrls: ['./user-table-row.component.scss']
})
export class UserTableRowComponent implements OnInit {
  @Input() User: any;
  @Output() stateChanged = new EventEmitter<any>();
  Editable = false;

  constructor(private api: DataStoreService) { }

  ngOnInit() {
    if (this.User && !this.User.Id) {
      this.Editable = true;
    }
  }
  changeUser(event) {
    this.User['UID'] = event['UID'];
  }
  Edit() {
    this.Editable = true;
  }
  Save() {
    if (!this.User.Id) {
      this.api.userCreate(this.User).subscribe(
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
      this.api.userUpdate(this.User).subscribe(
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
    if (!this.User.Id) {
      this.stateChanged.emit(null)
    }
    else {
      if (confirm("確定刪除?")) {
        this.api.userDelete(this.User).subscribe(
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
