import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { formControlBinding } from '@angular/forms/src/directives/reactive_directives/form_control_directive';
import { DataStoreService } from '../../shared/services';

@Component({
  selector: 'app-create-todolist',
  templateUrl: './create-todolist.component.html',
  styleUrls: ['./create-todolist.component.scss']
})
export class CreateTodolistComponent implements OnInit {

  constructor(private api: DataStoreService) { }
  Form = new FormGroup({
    title: new FormControl('', Validators.required),
    Description: new FormControl('', Validators.required),
    PMID: new FormControl('', Validators.required)
  })
  Members = [];
  ngOnInit() {
    this.api.todolistInit().subscribe(
      (data) => {
        console.log(data);
        this.Members = data;
      },
      (err) => {
        console.log(err);
      }
    )
  }
  submit() {
    console.log(this.Form.value);
    if (this.Form.valid) {
      this.api.createTodolist(this.Form.value).subscribe(
        (data)=>{
          alert("新增成功")
        },
        (err)=>{
          alert("新增失敗")
        }
      )
    }
  }

}
