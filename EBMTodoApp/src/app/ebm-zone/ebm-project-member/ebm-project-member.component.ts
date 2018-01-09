import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs/Subscription';
import { ProjectMember_Operation } from '../components/project-member-table-row/project-member-table-row.component';
declare var $: any;

@Component({
  selector: 'app-ebm-project-member',
  templateUrl: './ebm-project-member.component.html',
  styleUrls: ['./ebm-project-member.component.scss']
})
export class EbmProjectMemberComponent implements OnInit, AfterViewInit {
  Columns = [
    { name: "負責人", prop: "UserName", orderby: undefined },
    { name: "職稱", prop: "title", orderby: undefined },
    { name: "專案名稱", prop: "ProjectName", orderby: undefined },
    { name: "建立時間", prop: "CreateDateTime", orderby: undefined },
    { name: "待辦", prop: "Todolist", orderby: undefined },
  ]
  QueryModel = {
    Skip: 0,
    Length: 10
  }
  CurrentPage;
  TotalItems;
  Members: any;
  ajax: Subscription;

  Filters = {};
  PendingData = [];
  ProjectMember = {
    PID: "",
    ProjectName: ""
  }
  constructor(private api: DataStoreService, private router: Router, private route: ActivatedRoute, public location: Location) { }
  trackByFn(index, item) {
    return index; // or item.name
  }
  ngOnInit() {
    this.route.queryParams.subscribe((Params) => {
      this.ProjectMember['ProjectName'] = Params["ProjectName"];
      this.ProjectMember['PID'] = Params["PID"];
      let Skip = Params['Skip'];
      let Length = Params['Length'];
      let OrderBy = Params['OrderBy'];
      if (this.ProjectMember['PID']) {
        if (Skip && Length) {
          this.QueryModel['Skip'] = Skip;
          this.QueryModel['Length'] = Length;
          this.QueryModel['OrderBy'] = OrderBy;
          this.QueryModel['PID'] = this.ProjectMember['PID'];
          this.getData(this.QueryModel);
        }
        else {
          let Model = {
            Skip: 0,
            Length: 10,
            PID: Params["PID"],
            ProjectName: Params["ProjectName"]
          }
          this.location.replaceState(this.router.serializeUrl(this.router.createUrlTree(["/projectmember"], { queryParams: Model })));
          this.QueryModel['Skip'] = 0;
          this.QueryModel['Length'] = 10;
          this.QueryModel['OrderBy'] = OrderBy;
          this.QueryModel['PID'] = this.ProjectMember['PID'];
          this.getData(this.QueryModel);
        }
      }
      else {
        this.router.navigate(["/project"])
      }

    });
  }
  getData(model) {
    if (this.ajax) {
      this.ajax.unsubscribe();
    }
    this.ajax = this.api.projectMemberData(model).subscribe(
      (data) => {
        this.TotalItems = data.Total;
        this.Members = data.Data;
      },
      (err) => {
        console.log(err);
      });
  }
  changePage(event) {
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
    let type = <ProjectMember_Operation>event.type;
    let project = event.data;
    switch (type) {
      case ProjectMember_Operation.Insert:
        this.Members.unshift(project);
        this.deletePending(index);
        break;
      case ProjectMember_Operation.Update:
        break;
      case ProjectMember_Operation.Delete:
        let i = this.Members.findIndex(item => item.PMId == project.PMId);
        this.Members.splice(i, 1);
        this.Members = Object.assign([], this.Members);
        break;
      case ProjectMember_Operation.DeletePending:
        this.deletePending(index);
        break;
    }
  }
  add() {
    this.ProjectMember['CreateDateTime'] = new Date();
    this.PendingData.unshift(this.ProjectMember);
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
