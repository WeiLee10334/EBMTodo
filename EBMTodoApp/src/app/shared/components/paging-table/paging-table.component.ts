import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-paging-table',
  templateUrl: './paging-table.component.html',
  styleUrls: ['./paging-table.component.scss']
})
export class PagingTableComponent implements OnInit {
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

}
