import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs/Subscription';
import { BaseServerPagingTableComponent } from '../../basecomponent/base-server-paging-table/base-server-paging-table.component';
declare var $: any;

@Component({
  selector: 'app-ebm-user-member',
  templateUrl: './ebm-user-member.component.html',
  styleUrls: ['./ebm-user-member.component.scss']
})
export class EbmUserMemberComponent extends BaseServerPagingTableComponent implements OnInit, AfterViewInit {

  Columns = [
    { name: "負責人", prop: "UserName" },
    { name: "職稱", prop: "title" },
    { name: "專案名稱", prop: "ProjectName" },
    { name: "建立時間", prop: "CreateDateTime", template: 'date' }
  ]

  User = {
    Id: "",
    UserName: ""
  }
  checkUrl(Params) {
    super.checkUrl(Params);
    this.User['UserName'] = Params["UserName"];
    this.User['Id'] = Params["Id"];
    if (this.User['Id'] && this.User['UserName']) {
      this.QueryModel['Id'] = Params['Id'];
      this.QueryModel['UserName'] = Params['UserName'];
    }
    else {
      this.router.navigate(["/user"])
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
    let item = { CreateDateTime: new Date(), Id: this.User['Id'], UserName: this.User['UserName'] };
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
