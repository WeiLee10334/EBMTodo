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
    { name: "註記", prop: "memo" },
    { name: "人員", prop: "WorkerName" },
    { name: "時間", prop: "CreateDateTime" },
    { name: "內容", prop: "Content" },
    { name: "標籤", prop: "Tag" },
    { name: "類型", prop: "memoType" }
  ]
// public DateTime CreateDateTime { set; get; }

// public string Tag { set; get; }

// public string Content { set; get; }

// public string memoType { set; get; }

// public string LineUID { set; get; } 

// public string WorkerName { set; get; }

// public string memo { set; get; }

// public bool ProgressingFlag { set; get; }
  constructor() { }

  ngOnInit() {
  }

}
