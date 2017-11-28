import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-table-view',
  templateUrl: './table-view.component.html',
  styleUrls: ['./table-view.component.scss']
})
export class TableViewComponent implements OnInit {
  @Input('list') set list(value) {
    this.Data = value;
  };
  Data;
  Columns = [
    { name: "專案", prop: "ProjectName" },
    { name: "人員", prop: "WorkerName" },
    { name: "時間", prop: "RecordDateTime" },
    { name: "內容", prop: "Description" },
    { name: "時數", prop: "WokingHour" },
    { name: "類型", prop: "workingType" }
  ]
  // public Guid PWID { set; get; }

  // public string PID { set; get; }

  // public string ProjectName { set; get; }

  // public string Target { set; get; }

  // public string Description { set; get; }

  // public decimal WokingHour { set; get; }

  // public string workingType { set; get; }

  // public DateTime RecordDateTime { set; get; }

  // public string LineUID { set; get; }

  // public string WorkerName { set; get; }
  constructor() { }

  ngOnInit() {
  }

}
