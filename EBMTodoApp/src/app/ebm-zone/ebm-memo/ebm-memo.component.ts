import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';
import { BaseServerPagingTableComponent } from '../basecomponent/base-server-paging-table/base-server-paging-table.component';

@Component({
  selector: 'app-ebm-memo',
  templateUrl: './ebm-memo.component.html',
  styleUrls: ['./ebm-memo.component.scss']
})
export class EbmMemoComponent extends BaseServerPagingTableComponent implements OnInit {

  QueryModel = {
    Skip: 0,
    Length: 50,
    Start: undefined,
    End: undefined
  }
  getData(model) {
    super.getData(model);
    this.ajax = this.api.ebmMemoData(model).subscribe(
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
  //
  PendingMap = new Map<any, any>()
  getEditable(event) {
    return this.PendingMap.has(event);
  }
  add() {
    let item = { CreateDateTime: new Date() };
    this.PagingData.unshift(item);
    this.PendingMap.set(item, null);
  }
  setEditable(event) {
    let tmp = Object.assign({}, event);
    this.PendingMap.set(event, tmp);
  }
  Save(event) {
    if (event.MID) {
      this.api.ebmMemoUpdate(event).subscribe(
        (data) => {
          this.PendingMap.delete(event);
          Object.assign(event, data);
        },
        (err) => {
          console.log(err);
        }
      )
    }
    else {
      this.api.ebmMemoCreate(event).subscribe(
        (data) => {
          this.PendingMap.delete(event);
          Object.assign(event, data);
        },
        (err) => {
          console.log(err);
        }
      )
    }
  }
  Cancel(event) {
    let cache = this.PendingMap.get(event);
    if (cache) {
      Object.assign(event, cache);
      this.PendingMap.delete(event);
    }
    else {
      this.PendingMap.delete(event);
      this.PagingData.splice(this.PagingData.indexOf(event), 1);
    }
  }
  Delete(event) {
    if (confirm("確定刪除?")) {
      this.api.ebmMemoDelete(event).subscribe(
        (data) => {
          this.PagingData.splice(this.PagingData.indexOf(event), 1);
        },
        (err) => {
          console.log(err);
        }
      )
    }
  }
  DispatchAction(event, item) {
    switch (event) {
      case 'edit':
        this.setEditable(item);
        break;
      case 'save':
        this.Save(item);
        break;
      case 'cancel':
        this.Cancel(item);
        break;
      case 'delete':
        this.Delete(item);
        break;
    }
  }
}
