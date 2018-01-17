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

}
