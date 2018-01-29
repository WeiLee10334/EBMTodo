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
        { name: "專案名稱", prop: "ProjectName" },
        { name: "專案號", prop: "ProjectNo" },
        { name: "建立時間", prop: "CreateDateTime", template: 'date' }
    ]

    getData(model) {
        super.getData(model);
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
    //
    PendingMap = new Map<any, any>()
    add() {
        let item = { CreateDateTime: new Date(), _Editable: true };
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
        if (event.PID) {
            this.api.projectUpdate(model).subscribe(
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
            this.api.projectCreate(model).subscribe(
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
            this.api.projectDelete(event).subscribe(
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