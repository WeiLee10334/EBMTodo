import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';
import { Project_Operation } from '../components/project-table-row/project-table-row.component';
import { Observable } from 'rxjs/Observable';
import { Router, ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Subject } from 'rxjs/Subject';
import { Subscription } from 'rxjs/Subscription';
import { Location } from '@angular/common';
declare var $: any;

@Component({
  selector: 'app-ebm-project',
  templateUrl: './ebm-project.component.html',
  styleUrls: ['./ebm-project.component.scss']
})
export class EbmProjectComponent implements OnInit, AfterViewInit {
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
  Projects: any;
  ajax: Subscription;

  Filters = {};
  PendingData = [];

  constructor(private api: DataStoreService, private router: Router, private route: ActivatedRoute, public location: Location) { }
  trackByFn(index, item) {
    return index; // or item.name
  }
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
        this.location.replaceState(this.router.serializeUrl(this.router.createUrlTree(["/project"], { queryParams: Model })));
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
    let type = <Project_Operation>event.type;
    let project = event.data;
    switch (type) {
      case Project_Operation.Insert:
        this.Projects.unshift(project);
        this.deletePending(index);
        break;
      case Project_Operation.Update:
        break;
      case Project_Operation.Delete:
        let i = this.Projects.findIndex(item => item.PID == project.PID);
        this.Projects.splice(i, 1)
        this.Projects = Object.assign([], this.Projects);
        break;
      case Project_Operation.DeletePending:
        this.deletePending(index);
        break;
    }
  }
  add() {
    this.PendingData.unshift({ CreateDateTime: new Date() });
  }
  deletePending(index) {
    this.PendingData.splice(index, 1);
    this.PendingData = Object.assign([], this.PendingData);
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
