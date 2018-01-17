import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../../shared/services';
import { Observable } from 'rxjs/Observable';
import { Router, ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Subject } from 'rxjs/Subject';
import { Subscription } from 'rxjs/Subscription';
import { Location } from '@angular/common';
import { BaseServerPagingTableComponent } from '../../basecomponent/base-server-paging-table/base-server-paging-table.component';
declare var $: any;

@Component({
    selector: 'app-ebm-project-list',
    templateUrl: './ebm-project-list.component.html',
    styleUrls: ['./ebm-project-list.component.scss']
})

export class EbmProjectListComponent extends BaseServerPagingTableComponent implements OnInit, AfterViewInit {
    Columns = [
        { name: "專案名稱", prop: "ProjectName", orderby: undefined },
        { name: "專案號", prop: "ProjectNo", orderby: undefined },
        { name: "建立時間", prop: "CreateDateTime", orderby: undefined }
    ]
    getData(model) {
        super.getData(model);
        // this.CurrentPage = this.QueryModel['Skip'] / this.QueryModel['Length'] + 1;
        this.ajax = this.api.projectData(model).subscribe(
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