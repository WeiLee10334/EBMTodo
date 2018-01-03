import { Component, OnInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-ebm-project-member-list',
  templateUrl: './ebm-project-member-list.component.html',
  styleUrls: ['./ebm-project-member-list.component.scss']
})
export class EbmProjectMemberListComponent implements OnInit {

  constructor(private api: DataStoreService, private router: Router, private route: ActivatedRoute, public location: Location) { }
  Data = [];
  ngOnInit() {
    this.route.queryParams.subscribe((value) => {
      // alert("PID:" + value['PID']);
      let model = {
        "Length": 9999,
        "OrderBy": "CreateDateTime",
        "Reverse": false,
        "PID": value['PID']
      }
      this.api.projectMemberData(model).subscribe(
        (data) => {
          this.Data = data.Data;
          console.log(this.Data);
        },
        (err) => {
          console.log(err);
        }
      )

    });
  }

}
