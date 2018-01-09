import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DataStoreService } from '../../../shared/services';

@Component({
  selector: '[app-user-table-row]',
  templateUrl: './user-table-row.component.html',
  styleUrls: ['./user-table-row.component.scss']
})
export class UserTableRowComponent implements OnInit {
  @Input() set _User(value) {
    this.User = value;
    if (!value.Id) {
      this.Editable = true;
    }
  }
  @Output() change = new EventEmitter<any>();
  Editable = false;
  User: any;

  constructor(private api: DataStoreService) { }

  ngOnInit() {
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
          this._User = data;
          this.change.emit({
            type: User_Operation.Insert,
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
      this.api.userUpdate(this.User).subscribe(
        (data) => {
          this._User = data;
          this.Editable = false;
          this.change.emit({
            type: User_Operation.Update
          })
        },
        (err) => {
          console.log(err);
        }
      )
    }

  }
  Delete() {
    if (!this.User.Id) {
      this.change.emit({
        type: User_Operation.DeletePending,
        data: this.User
      })
    }
    else {
      if (confirm("確定刪除?")) {
        this.api.userDelete(this.User).subscribe(
          (data) => {
            this.change.emit({
              type: User_Operation.Delete,
              data: this.User
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
export enum User_Operation {
  Insert,
  Update,
  Delete,
  DeletePending
}