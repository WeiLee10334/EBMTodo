import { Component, OnInit } from '@angular/core';
import { DataStoreService } from '../../shared/services';

@Component({
  selector: 'app-ebm-project',
  templateUrl: './ebm-project.component.html',
  styleUrls: ['./ebm-project.component.scss']
})
export class EbmProjectComponent implements OnInit {

  constructor(private api: DataStoreService) { }

  ngOnInit() {
    let model = {
      "Length": 9999,
      "OrderBy": "CreateDateTime",
      "Reverse": false
    }
    this.api.projectData(model).subscribe(
      (data) => {
        console.log(data);
      },
      (err) => {
        console.log(err);
      }
    )
  }

}
