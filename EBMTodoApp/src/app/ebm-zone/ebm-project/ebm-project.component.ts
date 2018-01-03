import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';
import { tr } from 'ngx-bootstrap/bs-moment/i18n/tr';
import { filter } from 'rxjs/operator/filter';
declare var $: any;
@Component({
  selector: 'app-ebm-project',
  templateUrl: './ebm-project.component.html',
  styleUrls: ['./ebm-project.component.scss']
})
export class EbmProjectComponent implements OnInit, AfterViewInit {
  Columns = [
    { name: "專案名稱", prop: "ProjectName" },
    { name: "專案號", prop: "ProjectNo" },
    { name: "建立時間", prop: "CreateDateTime" },
    { name: "專案人員", prop: "ProjectMembers" },
  ]
  Source = [];
  Data = [];
  Filters = {};
  OrderBy = "CreateDateTime";
  PendingData = [];
  constructor(private api: DataStoreService) { }

  ngOnInit() {
    let model = {
      Length: 9999,
      OrderBy: "CreateDateTime",
      Reverse: true
    }
    this.api.projectData(model).subscribe(
      (data) => {
        this.Source = data.Data;
        this.filter();
        console.log(this.Data);
      },
      (err) => {
        console.log(err);
      }
    )
  }
  updateFilters(event, column) {
    this.Filters[column.prop] = event.target.value;
    console.log(this.Filters);
    this.filter();
  }
  filter() {
    let temp = Object.assign([], this.Source);

    Object.keys(this.Filters).forEach((key) => {
      temp = temp.filter((value) => {
        if (value[key]) {
          return value[key].toString().toLowerCase().indexOf(this.Filters[key].toString().toLowerCase()) !== -1;
        }
        else {
          return false;
        }
      })
    });
    this.Data = Object.assign([], temp);
  }
  add() {
    this.PendingData.unshift({ CreateDateTime: new Date() });
  }
  delete(project) {
    let index = this.Source.findIndex(item => item.PID == project.PID);
    this.Source.splice(index, 1);
    this.Source = Object.assign([], this.Source);
    this.filter();
  }
  deletePending(index) {
    this.PendingData.splice(index, 1);
    this.PendingData = Object.assign([], this.PendingData);
  }
  changeState(project, index) {
    this.deletePending(index);
    this.Source.unshift(project);
    this.filter();
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
