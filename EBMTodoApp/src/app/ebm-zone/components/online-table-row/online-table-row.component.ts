import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DataStoreService } from '../../../shared/services';

@Component({
  selector: '[app-online-table-row]',
  templateUrl: './online-table-row.component.html',
  styleUrls: ['./online-table-row.component.scss']
})
export class OnlineTableRowComponent implements OnInit {
  @Input() set _Online(value) {
    this.Online = value;
    if (!value.POID) {
      this.Editable = true;
    }
  }
  @Output() deleted = new EventEmitter<any>();
  @Output() added = new EventEmitter<any>();
  Editable = false;
  Online: any;

  constructor(private api: DataStoreService) { }

  ngOnInit() {
  }
  Edit() {
    this.Editable = true;
  }
  Save() {
    if (!this.Online.POID) {
      this.api.onlineInsert(this.Online).subscribe(
        (data) => {
          this._Online = data;
          this.Editable = false;
          this.added.emit(this.Online);
        },
        (err) => {
          console.log(err);
        }
      )
    }
    else {
      this.api.onlineUpdate(this.Online).subscribe(
        (data) => {
          this._Online = data;
          this.Editable = false;
        },
        (err) => {
          console.log(err);
        }
      )
    }

  }
  Delete() {
    this.api.onlineDelete(this.Online).subscribe(
      (data) => {
        this.deleted.emit(this);
      },
      (err) => {
        console.log(err);
      }
    )

  }
}
