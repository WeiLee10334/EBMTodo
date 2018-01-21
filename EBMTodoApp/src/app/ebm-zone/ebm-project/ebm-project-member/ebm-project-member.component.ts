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

  ProjectMember = {
    PID: "",
    ProjectName: ""
  }
  checkUrl(Params) {
    super.checkUrl(Params);
    this.ProjectMember['ProjectName'] = Params["ProjectName"];
    this.ProjectMember['PID'] = Params["PID"];
    if (this.ProjectMember['PID'] && this.ProjectMember['ProjectName']) {
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
  getEditable(event) {
    return this.PendingMap.has(event);
  }
  add() {
    let item = { CreateDateTime: new Date(), PID: this.ProjectMember['PID'], ProjectName: this.ProjectMember['ProjectName'] };
    this.PagingData.unshift(item);
    this.PendingMap.set(item, null);
  }
  setEditable(event) {
    let tmp = Object.assign({}, event);
    this.PendingMap.set(event, tmp);
  }
  Save(event) {
    if (event.PMID) {
      this.api.projectMemberUpdate(event).subscribe(
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
      this.api.projectMemberCreate(event).subscribe(
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
