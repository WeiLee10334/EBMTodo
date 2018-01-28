import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs/Subscription';
import { BaseServerPagingTableComponent } from '../../basecomponent/base-server-paging-table/base-server-paging-table.component';
declare var $: any;

@Component({
  selector: 'app-ebm-project-todolist',
  templateUrl: './ebm-project-todolist.component.html',
  styleUrls: ['./ebm-project-todolist.component.scss']
})
export class EbmProjectTodolistComponent extends BaseServerPagingTableComponent implements OnInit {
  QueryModel = {
    Skip: 0,
    Length: 50
  }
  Project = {
    PID: "",
    ProjectName: ""
  }
  checkUrl(Params) {
    super.checkUrl(Params);

    this.Project['ProjectName'] = Params["ProjectName"];
    this.Project['PID'] = Params["PID"];
    if (this.Project['PID'] && this.Project['ProjectName']) {
      this.QueryModel['PID'] = Params['PID'];
      this.QueryModel['ProjectName'] = Params['ProjectName'];
    }
    else {
      this.router.navigate(["/project"])
    }
  }
  getData(model) {
    super.getData(model);
    this.ajax = this.api.projectTodoListData(model).subscribe(
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
    let item = { ApplyDateTime: new Date(), PID: this.Project['PID'], ProjectName: this.Project['ProjectName'] };
    this.PagingData.unshift(item);
    this.PendingMap.set(item, null);
  }
  setEditable(event) {
    let tmp = Object.assign({}, event);
    this.PendingMap.set(event, tmp);
  }
  Save(event) {
    if (event.PTLID) {
      this.api.projectTodoListUpdate(event).subscribe(
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
      this.api.projectTodoListCreate(event).subscribe(
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
      this.api.projectTodoListDelete(event).subscribe(
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
        console.log(this.PendingMap.get(item));
        this.Cancel(item);
        break;
      case 'delete':
        this.Delete(item);
        break;
    }
  }
}
