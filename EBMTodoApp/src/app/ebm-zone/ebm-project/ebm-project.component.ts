import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';
import { Project_Operation } from '../components/project-table-row/project-table-row.component';
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
    { name: "建立時間", prop: "CreateDateTime" }
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
  dispatchAction(event, index) {
    let type = <Project_Operation>event.type;
    let project = event.data;
    console.log(type);
    switch (type) {
      case Project_Operation.Insert:
        this.Source.unshift(project);
        this.deletePending(index);
        this.filter();
        break;
      case Project_Operation.Update:
        break;
      case Project_Operation.Delete:
        let i = this.Source.findIndex(item => item.Id == project.Id);
        this.Source.splice(i, 1);
        this.Source = Object.assign([], this.Source);
        this.filter();
        break;
      case Project_Operation.DeletePending:
        this.deletePending(index);
        break;
    }
  }
  add() {
    this.PendingData.unshift({ CreateDateTime: new Date() });
  }
  deletePending(index) {
    this.PendingData.splice(index, 1);
    this.PendingData = Object.assign([], this.PendingData);
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
