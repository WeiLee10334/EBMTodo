import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';
declare var $: any;
@Component({
  selector: 'app-ebm-project-member',
  templateUrl: './ebm-project-member.component.html',
  styleUrls: ['./ebm-project-member.component.scss']
})
export class EbmProjectMemberComponent implements OnInit, AfterViewInit {

  // public string PMID { set; get; }

  // public DateTime? CreateDateTime { set; get; }
  // [StringLength(100)]
  // public string title { set; get; }
  // [Required]
  // public string Id { set; get; }

  // public string UserName { set; get; }
  // [Required]
  // public string PID { set; get; }

  // public string ProjectName { set; get; }
  Columns = [
    { name: "負責人", prop: "UserName" },
    { name: "職稱", prop: "title" },
    { name: "專案名稱", prop: "ProjectName" },
    { name: "建立時間", prop: "CreateDateTime" },
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
    this.api.projectMemberData(model).subscribe(
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
