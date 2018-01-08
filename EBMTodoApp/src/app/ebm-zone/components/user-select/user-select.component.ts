import { Component, OnInit, forwardRef, EventEmitter, Output } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { DataStoreService } from '../../../shared/services';

@Component({
  selector: 'app-user-select',
  templateUrl: './user-select.component.html',
  styleUrls: ['./user-select.component.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => UserSelectComponent),
    multi: true
  }]
})
export class UserSelectComponent implements OnInit, ControlValueAccessor {
  data;
  @Output() change = new EventEmitter<any>();
  constructor(private api: DataStoreService) { }
  // the method set in registerOnChange, it is just 
  // a placeholder for a method that takes one parameter, 
  // we use it to emit changes back to the form
  private setModel = (_: any) => { };
  // this is the initial value set to the component
  public writeValue(obj: any) {
    console.log("write", obj);
    this.data = obj;
  }
  // registers 'fn' that will be fired when changes are made
  // this is how we emit the changes back to the form
  public registerOnChange(fn: any) {
    this.setModel = fn;
  }
  // not used, used for touch input
  public registerOnTouched() { }

  //Api section
  Users = [];
  Skip = 0;
  Length = 20;
  Total = 0;
  IsEnd = false;
  Filters = {};
  PagingUsers = {};
  ngOnInit() {
    this.getUsers();
  }
  getUsers() {
    let model = {
      Skip: this.Skip,
      Length: this.Length,
      Filters: this.Filters
    }
    this.api.userData(model).subscribe(
      (data) => {
        this.Total = data.Total;
        data.Data.forEach((item) => {
          this.Users.push(item);
        });

        this.Skip += this.Length;
        console.log(this.Users);
      },
      (err) => {
        console.log(err);
      }
    )
  }
  select(event) {
    this.data = event;
    this.setModel(this.data);
    this.change.emit(this.data);
  }
  onScrollDown() {
    if (this.Skip < this.Total) {
      this.getUsers();
    }
    console.log('scrolled down!!')
  }
  search(event) {
    this.Filters = {
      UserName: event.target.value
    }
    this.Users = [];
    this.Skip = 0;
    this.getUsers();
    console.log("search", event);
  }
  onChange(event) {

  }
}
