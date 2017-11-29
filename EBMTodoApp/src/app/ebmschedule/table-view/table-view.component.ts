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
    { name: "行程", prop: "Title" },
    { name: "行程地點", prop: "Target" },
    { name: "專案", prop: "ProjectName" },
    { name: "人員", prop: "WorkerName" },
    { name: "時間", prop: "ScheduleDateTime" },
    { name: "內容", prop: "Description" },
    { name: "時數", prop: "WokingHour" },
    { name: "類型", prop: "scheduleType" }
  ]
  // public Guid PSID { set; get; }

  // public DateTime ScheduleDateTime { set; get; }

  // public string Target { set; get; }

  // public string Description { set; get; }

  // public decimal WokingHour { set; get; }

  // public string scheduleType { set; get; }

  // public DateTime FinishDateTime { set; get; }

  // public string LineUID { set; get; }

  // public string workerName { set; get; }

  // public string Title { set; get; }

  // public bool ProgressingFlag { set; get; }

  // public Guid PID { set; get; }

  // public string projectName { set; get; }
  constructor() { }

  ngOnInit() {
  }

}
