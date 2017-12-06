import { Component, OnInit } from '@angular/core';
import { DataStoreService } from '../../shared/services/index';

@Component({
  selector: 'app-ebmtodolist-list',
  templateUrl: './ebmtodolist-list.component.html',
  styleUrls: ['./ebmtodolist-list.component.scss']
})
export class EbmtodolistListComponent implements OnInit {

  constructor(private api: DataStoreService) { }
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
  ngOnInit() {
    this.api.todolistData().subscribe(
      (data) => {
        console.log(data);
        this.Data = data;
      },
      (err) => {
        console.log(err);
      }
    )
  }

}
