import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';
import { BaseServerPagingTableComponent } from '../basecomponent/base-server-paging-table/base-server-paging-table.component';

@Component({
  selector: 'app-ebm-online',
  templateUrl: './ebm-online.component.html',
  styleUrls: ['./ebm-online.component.scss']
})
export class EbmOnlineComponent extends BaseServerPagingTableComponent implements OnInit, AfterViewInit {

  Columns = [
    { name: "標題", prop: "title" },
    { name: "提出時間", prop: "ApplyDateTime" },
    { name: "提出單位", prop: "ApplyDepartment" },
    { name: "申請者", prop: "ApplyName" },
    { name: "問題說明", prop: "Description" },
    { name: "處理時間", prop: "HandleDateTime" },
    { name: "處理者", prop: "HandleName" },
    { name: "處理完成時間", prop: "ResolveDateTime" },
    { name: "回報者", prop: "ResponseName" },
    { name: "附註", prop: "Memo" },
    { name: "進度", prop: "CompleteRate" },
  ];
  refresh() {
    this.getData(this.QueryModel);
  }
  getData(model) {
    super.getData(model);
    this.ajax = this.api.projectOnlineData(model).subscribe(
      (data) => {
        this.PagingInfo.TotalItems = data.Total;
        this.PagingData = data.Data;

        //why???
        setTimeout(() => {
          this.PagingInfo.CurrentPage = <number>(this.QueryModel['Skip'] / this.QueryModel['Length'] + 1);
        }, 0)
      },
      (err) => {
        console.log(err);
      });
  }

  add() {
    this.PagingData.unshift({ ApplyDateTime: new Date() });
  }
  //
  lockedData = [];
  pendingData = [];
  timeoutId;
  timeoutDelay = 200;
  update() {
    console.log('update loop')
    let event = this.pendingData.pop();
    while (event) {
      this.api.projectOnlineUpdate(event).subscribe(
        (data) => {
          // this.PagingData[this.PagingData.findIndex(x => x === event)] = data;
          //Object.assign(this.PagingData[this.PagingData.findIndex(x => x === event)], data);
        },
        (err) => {
          console.log(err);
        }
      )
      event = this.pendingData.pop();
    }
    this.timeoutId = undefined;
  }
  delete(event) {
    if (this.lockedData.findIndex(x => x === event) == -1) {
      this.lockedData.push(event);
      this.api.projectOnlineDelete(event).subscribe(
        (data) => {
          this.lockedData.splice(this.lockedData.findIndex(x => x === event), 1);
          this.PagingData.splice(this.PagingData.findIndex(x => x === event), 1);
        },
        (err) => {
          console.log(err);
        }
      )
    }
  }
  modelChanged(event) {
    console.log('change', event.POID)
    if (event.POID) {
      let index = this.pendingData.findIndex(x => x === event);
      if (index == -1) {
        this.pendingData.push(event);
      }
      if (!this.timeoutId) {
        this.timeoutId = setTimeout(() => {
          this.update()
        }, this.timeoutDelay);
      }
    }
    else {
      if (this.lockedData.findIndex(x => x === event) == -1) {
        this.lockedData.push(event);
        this.api.projectOnlineCreate(event).subscribe(
          (data) => {
            this.lockedData.splice(this.lockedData.findIndex(x => x === event), 1);
            Object.assign(this.PagingData[this.PagingData.findIndex(x => x === event)], data);
          },
          (err) => {
            console.log(err);
          }
        )
      }
    }
  }
}
