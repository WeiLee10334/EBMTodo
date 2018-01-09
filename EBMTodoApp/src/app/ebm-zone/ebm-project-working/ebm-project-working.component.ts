import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
declare var $: any;

@Component({
  selector: 'app-ebm-project-working',
  templateUrl: './ebm-project-working.component.html',
  styleUrls: ['./ebm-project-working.component.scss']
})
export class EbmProjectWorkingComponent implements OnInit, AfterViewInit {
  Columns = [
    { name: "專案名稱", prop: "ProjectName" },
    { name: "負責人", prop: "UserName" },
    { name: "時間", prop: "CreateDateTime" },
    { name: "內容", prop: "Description" },
    { name: "時數", prop: "WorkingHour" },
    { name: "類型", prop: "WorkingType" }
  ]
  Source = [];
  Data = [];
  Filters = {};
  OrderBy = "RecordDateTime";
  ProjectName: String = "";
  constructor(private api: DataStoreService, private router: Router, private route: ActivatedRoute, public location: Location) { }
  ngOnInit() {
    this.route.queryParams.subscribe((value) => {
      this.ProjectName = value['ProjectName'];
      let model = {
        "Length": 9999,
        "OrderBy": "RecordDateTime",
        "Reverse": true,
        "PID": value['PID']
      }
      this.api.projectWorkingData(model).subscribe(
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
