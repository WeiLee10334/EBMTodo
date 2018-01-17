import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs/Subscription';
import { BaseServerPagingTableComponent } from '../../basecomponent/base-server-paging-table/base-server-paging-table.component';
declare var $: any;

@Component({
  selector: 'app-ebm-project-working',
  templateUrl: './ebm-project-working.component.html',
  styleUrls: ['./ebm-project-working.component.scss']
})
export class EbmProjectWorkingComponent extends BaseServerPagingTableComponent implements OnInit, AfterViewInit {
  Columns = [
    { name: "專案名稱", prop: "ProjectName", orderby: undefined },
    { name: "負責人", prop: "UserName", orderby: undefined },
    { name: "時間", prop: "CreateDateTime", orderby: undefined },
    { name: "內容", prop: "Description", orderby: undefined },
    { name: "時數", prop: "WorkingHour", orderby: undefined },
    { name: "類型", prop: "WorkingType", orderby: undefined }
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
