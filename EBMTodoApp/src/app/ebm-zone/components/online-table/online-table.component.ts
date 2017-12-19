import { Component, OnInit, Input, AfterViewInit } from '@angular/core';
import { OnlineTableRowComponent } from '../online-table-row/online-table-row.component';
import { DataStoreService } from '../../../shared/services';
declare var $: any;

@Component({
  selector: 'app-online-table',
  templateUrl: './online-table.component.html',
  styleUrls: ['./online-table.component.scss']
})
export class OnlineTableComponent implements OnInit {
  @Input() set _Data(value) {
    this.Data = value;
    this.Source = value;
  };
  @Input() set _Columns(value) {
    this.Columns = value;
  }
  Source = [];
  Data = [];
  PendingData = [];
  Columns = [];
  constructor(private api: DataStoreService) { }

  ngOnInit() {

  }
  ngAfterViewInit() {
    $("table th").resizable({
      handles: "e",
      minWidth: 0,
      resize: function (event, ui) {
        $(event.target).width(ui.size.width);
      }
    });
  }
  add() {
    this.PendingData.unshift({});
  }
  filter(event, column) {
    this.Data = Object.assign([], this.Source.filter((value) => {
      if (value[column.prop]) {
        return value[column.prop].toString().toLowerCase().indexOf(event.target.value) !== -1;
      }
      else {
        return false;
      }
    }))
  }
  delete(el: OnlineTableRowComponent, index) {
    var d = this.Data.splice(index, 1);
    this.Source.splice(this.Source.findIndex((value) => {
      if (value.POID == d[0].POID)
        return true;
    }), 1);
    this.Source = Object.assign([], this.Source);
    this.Data = Object.assign([], this.Data);
  }
  deletePending(el: OnlineTableRowComponent, index) {
    var d = this.PendingData.splice(index, 1);
    this.PendingData = Object.assign([], this.PendingData);
  }
  changeState(online, index) {
    var d = this.PendingData.splice(index, 1);
    this.PendingData = Object.assign([], this.PendingData);
    this.Source.unshift(online);
    this.Data.unshift(online);
    this.Source = Object.assign([], this.Source);
    this.Data = Object.assign([], this.Data);
  }
}
