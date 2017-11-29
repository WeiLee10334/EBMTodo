import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-groupbytime',
  templateUrl: './groupbytime.component.html',
  styleUrls: ['./groupbytime.component.scss']
})
export class GroupbytimeComponent implements OnInit {
  @Input('Data') set _Data(value) {
    this.Data = value;
  }
  Data;
  constructor() { }

  ngOnInit() {
  }

}
