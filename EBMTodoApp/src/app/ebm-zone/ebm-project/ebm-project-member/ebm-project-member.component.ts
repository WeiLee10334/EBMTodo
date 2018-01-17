import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs/Subscription';
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
    { name: "建立時間", prop: "CreateDateTime", orderby: undefined }
  ]
  QueryModel = {
    Skip: 0,
    Length: 10
  }
  CurrentPage;
  TotalItems;
  Members = [];
  ajax: Subscription;

  Filters = {};
  ProjectMember = {
    PID: "",
    ProjectName: ""
  }
  constructor(private api: DataStoreService, private router: Router, private route: ActivatedRoute, public location: Location) { }
  ngOnInit() {
    this.route.queryParams.subscribe((Params) => {
      this.ProjectMember['ProjectName'] = Params["ProjectName"];
      this.ProjectMember['PID'] = Params["PID"];

      let Skip = Params['Skip'];
      let Length = Params['Length'];
      let OrderBy = Params['OrderBy'];
      if (this.ProjectMember['PID'] && this.ProjectMember['ProjectName']) {
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
          this.location.replaceState(this.router.serializeUrl(this.router.createUrlTree(["./"], { relativeTo: this.route, queryParams: Model })));
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
    if (!event) {
      this.Members.splice(index, 1);
    }
    else {
      this.Members[index] = event;
    }
  }
  add() {
    this.Members.unshift(this.ProjectMember);
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
