import { Component, OnInit, forwardRef, EventEmitter, Output, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { DataStoreService } from '../../../shared/services';
import { Console } from '@angular/core/src/console';

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
  @Input() enableInput = true;
  @Input() UserName;
  @Output() change = new EventEmitter<any>();
  constructor(private api: DataStoreService) { }
  // the method set in registerOnChange, it is just 
  // a placeholder for a method that takes one parameter, 
  // we use it to emit changes back to the form
  private setModel = (_: any) => { };
  // this is the initial value set to the component
  public writeValue(obj: any) {
    this.Id = obj;
    console.log(obj)
  }
  Id;
  // registers 'fn' that will be fired when changes are made
  // this is how we emit the changes back to the form
  public registerOnChange(fn: any) {
    this.setModel = fn;
  }
  // not used, used for touch input
  public registerOnTouched() { }

  //Api section
  Users = [];
  SelectIndex;
  Skip = 0;
  Length = 10;
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
        if (this.Id) {
          this.SelectIndex = this.Users.findIndex((value, index, array) => {
            if (value['Id'] == this.Id) {
              return true;
            }
            return false;
          })

        }
        console.log(this.Users);
      },
      (err) => {
        console.log(err);
      }
    )
  }
  select(event) {
    this.SelectIndex = event;
    this.setModel(this.Users[event]['Id']);
  }
  onScrollDown() {
    console.log('scrolldown')
    if (this.Skip < this.Total) {
      this.getUsers();
    }
  }
  search(event) {
    this.Filters = {
      UserName: event.target.value
    }
    this.Users = [];
    this.Skip = 0;
    this.getUsers();
  }
}
