import { Component, OnInit, Input, AfterViewInit } from '@angular/core';
declare var $: any;
@Component({
  selector: 'app-paging-table',
  templateUrl: './paging-table.component.html',
  styleUrls: ['./paging-table.component.scss']
})
export class PagingTableComponent implements OnInit, AfterViewInit {
  @Input() set _Data(value) {
    this.Data = value;
  };
  @Input() set _Columns(value) {
    this.Columns = value;
  }
  Data = [];
  Columns = [];
  constructor() { }

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
}
