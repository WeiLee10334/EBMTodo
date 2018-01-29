import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs/Subscription';
import { BaseServerPagingTableComponent } from '../../basecomponent/base-server-paging-table/base-server-paging-table.component';
declare var $: any;

@Component({
  selector: 'app-ebm-user-working',
  templateUrl: './ebm-user-working.component.html',
  styleUrls: ['./ebm-user-working.component.scss']
})
export class EbmUserWorkingComponent extends BaseServerPagingTableComponent implements OnInit, AfterViewInit {
  Columns = [
    { name: "專案名稱", prop: "ProjectName" },
    { name: "負責人", prop: "UserName" },
    { name: "時間", prop: "CreateDateTime", template: 'date' },
    { name: "內容", prop: "Description" },
    { name: "時數", prop: "WorkingHour" },
    { name: "類型", prop: "WorkingType" }
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
    this.ajax = this.api.projectWorkingData(model).subscribe(
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
}
