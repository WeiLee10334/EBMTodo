import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-groupbyname',
  templateUrl: './groupbyname.component.html',
  styleUrls: ['./groupbyname.component.scss']
})
export class GroupbynameComponent implements OnInit {
  @Input('Data') set _Data(value) {
    this.Data = value;
  }
  Data;
  constructor() { }

  ngOnInit() {
  }


}
