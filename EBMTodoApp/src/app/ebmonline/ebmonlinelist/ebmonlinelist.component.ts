import { Component, OnInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';

@Component({
  selector: 'app-ebmonlinelist',
  templateUrl: './ebmonlinelist.component.html',
  styleUrls: ['./ebmonlinelist.component.scss']
})
export class EbmonlinelistComponent implements OnInit {

  constructor(private api: DataStoreService) { }
  Data = [];
  Columns = [
    { name: "標題", prop: "title" },
    { name: "提出時間", prop: "ApplyDateTime" },
    { name: "提出單位", prop: "ApplyDepartment" },
    { name: "申請者", prop: "ApplyName" },
    { name: "問題說明", prop: "Description" },
    { name: "處理時間", prop: "HandleDateTime" },
    { name: "處理者", prop: "HandleName" },
    { name: "處理完成時間", prop: "ResolveDateTime" },
    { name: "回報者", prop: "ResponseName" },
    { name: "附註", prop: "Memo" },
    { name: "進度", prop: "CompleteRate" },
  ];
  ngOnInit() {
    this.api.onlineData().subscribe(
      (data) => {
        this.Data = data;
      },
      (err) => {
        console.log(err);
      }
    )
  }

}
