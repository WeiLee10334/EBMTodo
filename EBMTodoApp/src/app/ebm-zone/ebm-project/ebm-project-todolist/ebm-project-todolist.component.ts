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
  add() {
    this.PagingData.unshift({ PID: this.ProjectMember['PID'], ProjectName: this.ProjectMember['ProjectName'], CreateDateTime: new Date() });
  }


}
