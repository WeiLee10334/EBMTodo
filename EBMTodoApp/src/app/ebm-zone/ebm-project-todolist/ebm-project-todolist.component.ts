import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs/Subscription';
declare var $: any;

@Component({
  selector: 'app-ebm-project-todolist',
  templateUrl: './ebm-project-todolist.component.html',
  styleUrls: ['./ebm-project-todolist.component.scss']
})
export class EbmProjectTodolistComponent implements OnInit {

  constructor(private api: DataStoreService, private router: Router, private route: ActivatedRoute, public location: Location) { }
  trackByFn(index, item) {
    return index; // or item.name
  }
  ProjectName;
  TodoLists = [];
  PendingData = [];
  QueryModel;
  ngOnInit() {
    this.QueryModel = {
      Skip: 0,
      Length: 9999
    }
    this.route.queryParams.subscribe((Params) => {
      let PID = Params['PID'];
      this.ProjectName = Params['ProjectName'];
      if (PID) {
        this.QueryModel['PID'] = PID;
        this.getData(this.QueryModel);
      }
      else {
        this.location.back();
      }
    })
  }
  getData(model) {
    this.api.projectTodoListData(model).subscribe(
      (data) => {
        this.TodoLists = data.Data;
      },
      (err) => {
        console.log(err);
      }
    )
  }
  add() {
    this.PendingData.unshift({});
  }
  todoChanged(event, index) {
    if (event === null) {
      this.PendingData.splice(index, 1);
    }
    else {
      if (event.PTLID) {
        this.api.projectTodoListUpdate(event).subscribe(
          (data) => {
            this.TodoLists[index] = Object.assign({}, data);
          },
          (err) => {
            alert('操作錯誤');
            console.log(err);
          }
        )
      }
      else {
        this.api.projectTodoListCreate(event).subscribe(
          (data) => {
            this.TodoLists[index] = Object.assign({}, data);
          },
          (err) => {
            alert('操作錯誤');
            console.log(err);
          }
        )
      }

    }
  }

}
