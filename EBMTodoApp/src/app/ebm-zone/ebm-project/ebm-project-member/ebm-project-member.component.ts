import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs/Subscription';
import { BaseServerPagingTableComponent } from '../../basecomponent/base-server-paging-table/base-server-paging-table.component';
declare var $: any;

@Component({
  selector: 'app-ebm-project-member',
  templateUrl: './ebm-project-member.component.html',
  styleUrls: ['./ebm-project-member.component.scss']
})
export class EbmProjectMemberComponent extends BaseServerPagingTableComponent implements OnInit, AfterViewInit {

  Columns = [
    { name: "負責人", prop: "UserName", orderby: undefined },
    { name: "職稱", prop: "title", orderby: undefined },
    { name: "專案名稱", prop: "ProjectName", orderby: undefined },
    { name: "建立時間", prop: "CreateDateTime", orderby: undefined }
  ]

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
    this.ajax = this.api.projectMemberData(model).subscribe(
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
  add() {
    let item = { CreateDateTime: new Date(), _Editable: true, PID: this.Project['PID'], ProjectName: this.Project['ProjectName'] };
    this.PagingData.unshift(item);
    this.PendingMap.set(item, null);
  }
  setEditable(event) {
    let tmp = Object.assign({}, event);
    this.PendingMap.set(event, tmp);
    event._Editable = true;
  }
  Save(event) {
    let model = Object.assign({}, event);
    delete model._Editable;
    if (event.PMID) {
      this.api.projectMemberUpdate(model).subscribe(
        (data) => {
          this.PendingMap.delete(event);
          Object.assign(event, data);
        },
        (err) => {
          console.log(err);
        },
        () => {
          delete event._Editable;
        }
      )
    }
    else {
      this.api.projectMemberCreate(model).subscribe(
        (data) => {
          this.PendingMap.delete(event);
          Object.assign(event, data);
        },
        (err) => {
          console.log(err);
        },
        () => {
          delete event._Editable;
        }
      )
    }
  }
  Cancel(event) {
    let cache = this.PendingMap.get(event);
    if (cache) {
      delete event._Editable;
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
      this.api.projectMemberDelete(event).subscribe(
        (data) => {
          this.PagingData.splice(this.PagingData.indexOf(event), 1);
        },
        (err) => {
          console.log(err);
        }
      )
    }
  }
}
