import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { DataStoreService } from '../../shared/services/data-store.service';
import { NgControlStatus } from '@angular/forms/src/directives/ng_control_status';
import { validateConfig } from '@angular/router/src/config';

@Component({
  selector: 'app-ebm-todolist',
  templateUrl: './ebm-todolist.component.html',
  styleUrls: ['./ebm-todolist.component.scss']
})
export class EbmTodolistComponent implements OnInit {
  @ViewChild('keyword') keyword: ElementRef;
  constructor(private api: DataStoreService) { }
  Source = [];
  Data = [];
  Columns = [
    { name: "標題", prop: "title" },
    { name: "工作內容", prop: "Description" },
    { name: "完成度", prop: "CompleteRate" },
    { name: "人員", prop: "MemberTitle" }
  ];
  // public string PTLID { set; get; }

  // public DateTime CreateDateTime { set; get; }

  // public string title { set; get; }

  // public string Description { set; get; }

  // public int CompleteRate { set; get; }

  // public string PMID { set; get; }

  // public string MemberTitle { set; get; }
  Cards = new Map<string, boolean>();
  ngOnInit() {
    this.api.todolistData().subscribe(
      (data) => {
        this.Source = data;
        this.Data.forEach((item) => {
          this.Cards.set(item.PTLID, false);
        })
        this.filter();
      },
      (err) => {
        console.log(err);
      }
    )
  }
  newCard() {
    this.Data.unshift({});
  }
  todoChanged(event, index) {
    if (event === null) {
      this.Data.splice(index, 1);
    }
    else {
      this.api.createOrUpdateTodolist(event).subscribe(
        (data) => {
          this.Data[index] = Object.assign({}, data);
        },
        (err) => {
          alert('操作錯誤');
          console.log(err);
        }
      )
    }
  }
  filter() {
    if (this.keyword.nativeElement.value) {
      this.Data = Object.assign([], this.Source.filter((value) => {
        var keys = Object.keys(value);
        var match = false;
        keys.forEach((k) => {
          if (value[k] && value[k].toString().toLowerCase().indexOf(this.keyword.nativeElement.value.toLowerCase()) !== -1) {
            match = true;
            return;
          }
        });
        return match;
      }))
    }
    else {
      this.Data = this.Source;
    }

  }
}
