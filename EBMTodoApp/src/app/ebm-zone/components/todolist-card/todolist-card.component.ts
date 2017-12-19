import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DataStoreService } from '../../../shared/services';

@Component({
  selector: 'app-todolist-card',
  templateUrl: './todolist-card.component.html',
  styleUrls: ['./todolist-card.component.scss']
})
export class TodolistCardComponent implements OnInit {
  @Input() set _Todo(value) {
    this.Todo = value;
    if (!value.PTLID) {
      this.api.todolistInit().subscribe(
        (data) => {
          this.Members = data;
        },
        (err) => {
          console.log(err);
        }
      )
      this.IsForm = true;
    }
  }
  @Output() changed = new EventEmitter<any>();
  IsForm = false;
  Todo: any;
  Form = new FormGroup({
    PTLID: new FormControl(),
    ApplyDateTime: new FormControl('', Validators.required),
    ApplyName: new FormControl(),
    title: new FormControl('', Validators.required),
    Description: new FormControl('', Validators.required),
    CompleteRate: new FormControl(0, [Validators.max(100), Validators.min(0)]),
    MemberTitle: new FormControl(),
    PMID: new FormControl('', Validators.required),
    Tag: new FormControl(),
    Memo: new FormControl()
  })
  Members = [];
  constructor(private api: DataStoreService) { }

  ngOnInit() {
  }
  submit() {
    if (this.Form.valid) {
      this.changed.emit(this.Form.value);
      this.IsForm = false;
    }
  }
  enable() {
    this.api.todolistInit().subscribe(
      (data) => {
        this.Members = data;
      },
      (err) => {
        console.log(err);
      }
    )
    this.Form.setValue(this.Todo);
    this.IsForm = true;
  }
  disable() {
    if (!this.Todo.PTLID) {
      this.changed.emit(null);
    }
    this.IsForm = false;
  }

}
