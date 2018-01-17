import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs/Subscription';
import { BaseServerPagingTableComponent } from '../../basecomponent/base-server-paging-table/base-server-paging-table.component';
declare var $: any;

@Component({
  selector: 'app-ebm-user-list',
  templateUrl: './ebm-user-list.component.html',
  styleUrls: ['./ebm-user-list.component.scss']
})
export class EbmUserListComponent extends BaseServerPagingTableComponent implements OnInit, AfterViewInit {

  Columns = [
    { name: "組員名稱", prop: "UserName", orderby: undefined },
    { name: "Email", prop: "Email", orderby: undefined },
    { name: "Line", prop: "UID", orderby: undefined },
  ]
  getData(model) {
    super.getData(model);
    this.ajax = this.api.userData(model).subscribe(
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
