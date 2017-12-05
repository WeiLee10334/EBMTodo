import { Component, OnInit } from '@angular/core';
import { DataStoreService } from '../shared/services';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-ebmschedule',
  templateUrl: './ebmschedule.component.html',
  styleUrls: ['./ebmschedule.component.scss']
})
export class EbmscheduleComponent implements OnInit {

  constructor(public api: DataStoreService) { }
  Form = new FormGroup({
    start: new FormControl(),
    end: new FormControl(),
    UIDs: new FormControl([]),
    PIDs: new FormControl([]),
  })
  Options;
  Settings: IMultiSelectSettings = {
    checkedStyle: 'fontawesome',
    buttonClasses: 'btn btn-success btn-block',
    displayAllSelectedText: true,
    showCheckAll: true,
    showUncheckAll: true,
  };
  Data;
  Groupby = 'day';
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
  ngOnInit() {
    this.api.workingInit().subscribe(
      (data) => {
        console.log(data);
        this.Options = data;
      },
      (err) => {
        console.log(err);
      }
    )
  }
  submit() {
    console.log(this.Form.value);
    if (this.Groupby == "member") {
      this.api.scheduleDataByUID(this.Form.value).subscribe(
        (data) => {
          console.log(data);
          this.Data = data;
        },
        (err) => {
          console.log(err);
        }
      )
    }
    else {
      let query = Object.assign({ groupby: this.Groupby }, this.Form.value);
      this.api.scheduleDataByTime(query).subscribe(
        (data) => {
          console.log(data);
          this.Data = data;
        },
        (err) => {
          console.log(err);
        }
      )
    }

  }
  changeGroup(by: string) {
    console.log(by);
    this.Groupby = by;
    this.submit();
  }

}
