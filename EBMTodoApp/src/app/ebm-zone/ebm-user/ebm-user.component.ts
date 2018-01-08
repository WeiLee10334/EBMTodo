import { Component, OnInit, AfterViewInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { User_Operation } from '../components/user-table-row/user-table-row.component';
declare var $: any;
@Component({
  selector: 'app-ebm-user',
  templateUrl: './ebm-user.component.html',
  styleUrls: ['./ebm-user.component.scss']
})
export class EbmUserComponent implements OnInit, AfterViewInit {
  Columns = [
    { name: "組員名稱", prop: "UserName" },
    { name: "Email", prop: "Email" },
    { name: "Line", prop: "UID" },
  ]
  Source = [];
  Data = [];
  Filters = {};
  OrderBy = "UserName";
  PendingData = [];
  constructor(private api: DataStoreService, private router: Router, private route: ActivatedRoute, public location: Location) { }
  ngOnInit() {
    this.route.queryParams.subscribe((value) => {
      let model = {
        "Length": 9999,
        "OrderBy": "UserName",
        "Reverse": false
      }
      this.api.userData(model).subscribe(
        (data) => {
          this.Source = data.Data;
          this.filter();
        },
        (err) => {
          console.log(err);
        }
      )
    });
  }
  updateFilters(event, column) {
    this.Filters[column.prop] = event.target.value;
    console.log(this.Filters);
    this.filter();
  }
  filter() {
    let temp = Object.assign([], this.Source);
    Object.keys(this.Filters).forEach((key) => {
      temp = temp.filter((value) => {
        if (value[key]) {
          return value[key].toString().toLowerCase().indexOf(this.Filters[key].toString().toLowerCase()) !== -1;
        }
        else {
          return false;
        }
      })
    });
    this.Data = Object.assign([], temp);
  }
  dispatchAction(event, index) {
    let type = <User_Operation>event.type;
    let user = event.data;
    console.log(type);
    switch (type) {
      case User_Operation.Insert:
        this.Source.unshift(user);
        this.deletePending(index);
        this.filter();
        break;
      case User_Operation.Update:
        break;
      case User_Operation.Delete:
        let i = this.Source.findIndex(item => item.Id == user.Id);
        this.Source.splice(i, 1);
        this.Source = Object.assign([], this.Source);
        this.filter();
        break;
      case User_Operation.DeletePending:
        this.deletePending(index);
        break;
    }
  }
  deletePending(index) {
    this.PendingData.splice(index, 1);
    this.PendingData = Object.assign([], this.PendingData);
  }
  add() {
    this.PendingData.unshift({});
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
