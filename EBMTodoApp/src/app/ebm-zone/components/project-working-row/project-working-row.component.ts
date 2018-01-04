import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: '[app-project-working-row]',
  templateUrl: './project-working-row.component.html',
  styleUrls: ['./project-working-row.component.scss']
})
export class ProjectWorkingRowComponent implements OnInit {
  @Input() set _Working(value) {
    this.Working = value;
  }
  Working: any;
  constructor() { }

  ngOnInit() {
  }

}
