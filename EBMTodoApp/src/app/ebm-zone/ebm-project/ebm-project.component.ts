import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';
declare var $: any;
@Component({
  selector: 'app-ebm-project',
  templateUrl: './ebm-project.component.html',
  styleUrls: ['./ebm-project.component.scss']
})
export class EbmProjectComponent implements OnInit, AfterViewInit {
  // public string PID { set; get; }
  // [StringLength(100)]
  // public string ProjectName { set; get; }

  // public DateTime? CreateDateTime { set; get; }
  // [StringLength(100)]
  // public string ProjectNo { set; get; }

  // public bool IsHode { set; get; }
  Columns = [
    { name: "專案名稱", prop: "ProjectName" },
    { name: "專案號", prop: "ProjectNo" },
    { name: "建立時間", prop: "CreateDateTime" },
    { name: "專案人員", prop: "ProjectMembers" },
  ]
  Data = [];
  PendingData = [];
  constructor(private api: DataStoreService) { }
  add() {
    this.PendingData.unshift({ CreateDateTime: new Date() });
  }
  ngOnInit() {
    let model = {
      "Length": 9999,
      "OrderBy": "CreateDateTime",
      "Reverse": false
    }
    this.api.projectData(model).subscribe(
      (data) => {
        this.Data = data.Data;
        console.log(this.Data);
      },
      (err) => {
        console.log(err);
      }
    )
  }
  delete(el: any, index) {
    var d = this.Data.splice(index, 1);

    this.Data = Object.assign([], this.Data);
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
