import { Component, OnInit, AfterViewInit, Input, ChangeDetectorRef } from '@angular/core';
import { DataStoreService } from '../../../shared/services';
import { Observable } from 'rxjs/Observable';
import { Router, ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Subject } from 'rxjs/Subject';
import { Subscription } from 'rxjs/Subscription';
import { Location } from '@angular/common';
declare var $: any;

@Component({
  selector: 'app-base-server-paging-table',
  templateUrl: './base-server-paging-table.component.html',
  styleUrls: ['./base-server-paging-table.component.scss']
})
export class BaseServerPagingTableComponent implements OnInit, AfterViewInit {
  trackByFn(index, obj) {
    return obj;
  }
  Columns = []
  QueryModel = {
    Skip: 0,
    Length: 10
  }
  PagingInfo = {
    CurrentPage: 1,
    TotalItems: 0,
    MaxSize: 10
  }

  @Input() PagingData = [];
  ajax: Subscription;
  Filters = {};

  constructor(protected api: DataStoreService, protected router: Router, protected route: ActivatedRoute, public location: Location, protected ref: ChangeDetectorRef) { }

  ngOnInit() {
    this.route.queryParams.subscribe((Params) => {
      this.checkUrl(Params);
      this.location.replaceState(this.router.serializeUrl(this.router.createUrlTree(["./"], { relativeTo: this.route, queryParams: this.QueryModel })));
      this.getData(this.QueryModel);
    });
  }
  checkUrl(Params) {
    let Skip = Params['Skip'];
    let Length = Params['Length'];
    let OrderBy = Params['OrderBy'];
    if (Skip && Length) {
      this.QueryModel['Skip'] = Skip;
      this.QueryModel['Length'] = Length;
      this.QueryModel['OrderBy'] = OrderBy;
    }
  }
  getData(model) {
    if (this.ajax) {
      this.ajax.unsubscribe();
    }
  }
  changePage(event) {
    if (event != this.PagingInfo.CurrentPage) {
      this.QueryModel.Skip = this.QueryModel.Length * (event.page - 1);
      this.location.replaceState(this.router.serializeUrl(this.router.createUrlTree(["./"], { relativeTo: this.route, queryParams: this.QueryModel })));
      this.getData(this.QueryModel);
    }
    console.log(event)

  }
  changeOrderBy(prop, reverse) {
    this.QueryModel['OrderBy'] = prop;
    this.QueryModel['Reverse'] = reverse;
    this.Columns.find(x => x.prop === prop).orderby = reverse;
    this.location.replaceState(this.router.serializeUrl(this.router.createUrlTree(["./"], { relativeTo: this.route, queryParams: this.QueryModel })));
    this.getData(this.QueryModel);
  }
  updateFilters(event, column) {
    this.Filters[column.prop] = event.target.value;
    this.QueryModel['Filters'] = this.Filters;
    this.getData(this.QueryModel);
  }
  dispatchAction(event, index) {
    if (!event) {
      this.PagingData.splice(index, 1);
    }
    else {
      this.PagingData[index] = event;
    }
  }
  add() {
    this.PagingData.unshift({ CreateDateTime: new Date() });
  }
  ngAfterViewInit() {
    $("table th").resizable({
      handles: "e",
      minWidth: 0,
      resize: function (event, ui) {
        $(event.target).width(ui.size.width);
      }
    });
  }

}