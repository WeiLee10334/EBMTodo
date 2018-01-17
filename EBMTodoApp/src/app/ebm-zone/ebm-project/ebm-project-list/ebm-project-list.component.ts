import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../../shared/services';
import { Observable } from 'rxjs/Observable';
import { Router, ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Subject } from 'rxjs/Subject';
import { Subscription } from 'rxjs/Subscription';
import { Location } from '@angular/common';
declare var $: any;

@Component({
  selector: 'app-ebm-project-list',
  templateUrl: './ebm-project-list.component.html',
  styleUrls: ['./ebm-project-list.component.scss']
})
export class EbmProjectListComponent implements OnInit, AfterViewInit {
  Columns = [
    { name: "專案名稱", prop: "ProjectName", orderby: undefined },
    { name: "專案號", prop: "ProjectNo", orderby: undefined },
    { name: "建立時間", prop: "CreateDateTime", orderby: undefined }
  ]
  QueryModel = {
    Skip: 0,
    Length: 10
  }
  CurrentPage;
  TotalItems;
  Projects = [];
  ajax: Subscription;
  Filters = {};

  constructor(private api: DataStoreService, private router: Router, private route: ActivatedRoute, public location: Location) { }

  ngOnInit() {
    this.route.queryParams.subscribe((Params) => {
      let Skip = Params['Skip'];
      let Length = Params['Length'];
      let OrderBy = Params['OrderBy'];
      if (Skip && Length) {
        this.QueryModel['Skip'] = Skip;
        this.QueryModel['Length'] = Length;
        this.QueryModel['OrderBy'] = OrderBy;
        this.getData(this.QueryModel);
      }
      else {
        let Model = {
          Skip: 0,
          Length: 10
        }
        this.location.replaceState(this.router.serializeUrl(this.router.createUrlTree(["./"], { relativeTo: this.route, queryParams: Model })));
        this.QueryModel['Skip'] = 0;
        this.QueryModel['Length'] = 10;
        this.QueryModel['OrderBy'] = OrderBy;
        this.getData(this.QueryModel);
      }
    });
  }
  getData(model) {
    if (this.ajax) {
      this.ajax.unsubscribe();
    }
    this.ajax = this.api.projectData(model).subscribe(
      (data) => {
        this.TotalItems = data.Total;
        this.Projects = data.Data;
      },
      (err) => {
        console.log(err);
      });
  }
  changePage(event) {
    console.log(event)
    this.QueryModel.Skip = this.QueryModel.Length * (event.page - 1);
    this.getData(this.QueryModel);
  }
  changeOrderBy(prop, reverse) {
    this.QueryModel['OrderBy'] = prop;
    this.QueryModel['Reverse'] = reverse;
    this.Columns.find(x => x.prop === prop).orderby = reverse;
    this.getData(this.QueryModel);
  }
  updateFilters(event, column) {
    this.Filters[column.prop] = event.target.value;
    this.QueryModel['Filters'] = this.Filters;
    this.getData(this.QueryModel);
  }
  dispatchAction(event, index) {
    if (!event) {
      this.Projects.splice(index, 1);
    }
    else {
      this.Projects[index] = event;
    }
  }
  add() {
    this.Projects.unshift({ CreateDateTime: new Date() });
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
