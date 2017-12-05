import { Component, OnInit } from '@angular/core';
import { DataStoreService } from '../shared/services';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-memo',
  templateUrl: './memo.component.html',
  styleUrls: ['./memo.component.scss']
})
export class MemoComponent implements OnInit {

  constructor(public api: DataStoreService) { }
  Form = new FormGroup({
    start: new FormControl(),
    end: new FormControl(),
    UIDs: new FormControl([])
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
    { name: "註記", prop: "memo" },
    { name: "人員", prop: "WorkerName" },
    { name: "時間", prop: "CreateDateTime" },
    { name: "內容", prop: "Content" },
    { name: "標籤", prop: "Tag" },
    { name: "類型", prop: "memoType" }
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
      this.api.memoDataByUID(this.Form.value).subscribe(
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
      this.api.memoDataByTime(query).subscribe(
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
