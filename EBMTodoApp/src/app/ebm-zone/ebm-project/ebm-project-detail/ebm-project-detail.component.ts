import { Component, OnInit } from '@angular/core';
import { DataStoreService } from '../../../shared/services/data-store.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-ebm-project-detail',
  templateUrl: './ebm-project-detail.component.html',
  styleUrls: ['./ebm-project-detail.component.scss']
})
export class EbmProjectDetailComponent implements OnInit {
  Project = {
    PID: "",
    ProjectName: ""
  }
  constructor(protected api: DataStoreService, protected router: Router, protected route: ActivatedRoute, public location: Location) { }

  ngOnInit() {
    this.route.queryParams.subscribe((Params) => {
      this.Project['ProjectName'] = Params["ProjectName"];
      this.Project['PID'] = Params["PID"];
      if (!this.Project['PID'] || !this.Project['ProjectName']) {
        this.router.navigate(["/project"])
      }
    });
  }

}
