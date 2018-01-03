import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
declare var $: any;
@Component({
  selector: 'app-ebm-project-member',
  templateUrl: './ebm-project-member.component.html',
  styleUrls: ['./ebm-project-member.component.scss']
})
export class EbmProjectMemberComponent implements OnInit, AfterViewInit {
  Columns = [
    { name: "負責人", prop: "UserName" },
    { name: "職稱", prop: "title" },
    { name: "專案名稱", prop: "ProjectName" },
    { name: "建立時間", prop: "CreateDateTime" },
    { name: "待辦", prop: "Todolist" },
  ]
  Source = [];
  Data = [];
  Filters = {};
  OrderBy = "CreateDateTime";
  PendingData = [];
  ProjectMember = {
    PID: "",
    ProjectName: ""
  }
  constructor(private api: DataStoreService, private router: Router, private route: ActivatedRoute, public location: Location) { }
  ngOnInit() {
    this.route.queryParams.subscribe((value) => {
      this.ProjectMember['ProjectName'] = value["ProjectName"];
      this.ProjectMember['PID'] = value["PID"];
      let model = {
        "Length": 9999,
        "OrderBy": "CreateDateTime",
        "Reverse": false,
        "PID": value['PID']
      }
      this.api.projectMemberData(model).subscribe(
        (data) => {
          this.Source = data.Data;
          this.filter();
        },
        (err) => {
          console.log(err);
        }
      )
    });
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
    this.ProjectMember['CreateDateTime'] = new Date();
    this.PendingData.unshift(this.ProjectMember);
  }
  delete(project) {
    let index = this.Source.findIndex(item => item.PID == project.PID);
    this.Source.splice(index, 1);
    this.Source = Object.assign([], this.Source);
    this.filter();
  }
  deletePending(index) {
    console.log(index)
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
