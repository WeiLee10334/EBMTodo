import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: '[app-project-working-table-row]',
  templateUrl: './project-working-table-row.component.html',
  styleUrls: ['./project-working-table-row.component.scss']
})
export class ProjectWorkingTableRowComponent implements OnInit {
  @Input() ProjectWorking: any;
  constructor() { }

  ngOnInit() {
  }

}
