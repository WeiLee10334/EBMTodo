import { Component, OnInit } from '@angular/core';
import { DataStoreService } from '../shared/services';
import { IMultiSelectOption, IMultiSelectSettings } from 'angular-2-dropdown-multiselect';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-memo',
  templateUrl: './memo.component.html',
  styleUrls: ['./memo.component.scss']
})
export class MemoComponent implements OnInit {

  constructor() { }
  ngOnInit() {
  }

}
