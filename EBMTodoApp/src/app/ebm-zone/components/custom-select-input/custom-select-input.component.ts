import { Component, OnInit, forwardRef, EventEmitter, Output, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { DataStoreService } from '../../../shared/services';
import { Console } from '@angular/core/src/console';

@Component({
  selector: 'app-custom-select-input',
  templateUrl: './custom-select-input.component.html',
  styleUrls: ['./custom-select-input.component.scss'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => CustomSelectInputComponent),
    multi: true
  }]
})
export class CustomSelectInputComponent implements OnInit, ControlValueAccessor {
  trackByFn(index, obj) {
    return obj;
  }
  @Input() enableInput = true;
  @Input() LabelName = "";
  @Input() type;
  @Input() filter;
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
  SelectArray = [];
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
    if (this.filter) {
      model.Filters = this.filter;
    }
    switch (this.type) {
      case "user":
        this.api.userData(model).subscribe(
          (data) => {
            this.Total = data.Total;
            data.Data.forEach((item) => {
              let selectItem = {
                Id: item.Id,
                LabelName: item.UserName
              }
              this.SelectArray.push(selectItem);
            });
            this.Skip += this.Length;
            if (this.Id) {
              this.SelectIndex = this.SelectArray.findIndex((value, index, array) => {
                if (value['Id'] == this.Id) {
                  return true;
                }
                return false;
              })

            }
          },
          (err) => {
            console.log(err);
          }
        )
        break;
      case "lineuser":
        this.api.lineuserData(model).subscribe(
          (data) => {
            this.Total = data.Total;
            data.Data.forEach((item) => {
              let selectItem = {
                Id: item.UID,
                LabelName: item.UserName
              }
              this.SelectArray.push(selectItem);
            });
            this.Skip += this.Length;
            if (this.Id) {
              this.SelectIndex = this.SelectArray.findIndex((value, index, array) => {
                if (value['Id'] == this.Id) {
                  return true;
                }
                return false;
              })

            }
          },
          (err) => {
            console.log(err);
          }
        )
        break;
      case "project":
        this.api.projectData(model).subscribe(
          (data) => {
            this.Total = data.Total;
            data.Data.forEach((item) => {
              let selectItem = {
                Id: item.PID,
                LabelName: item.ProjectName
              }
              this.SelectArray.push(selectItem);
            });
            this.Skip += this.Length;
            if (this.Id) {
              this.SelectIndex = this.SelectArray.findIndex((value, index, array) => {
                if (value['Id'] == this.Id) {
                  return true;
                }
                return false;
              })

            }
          },
          (err) => {
            console.log(err);
          }
        )
        break;
      case "projectmember":
        this.api.projectMemberData(model).subscribe(
          (data) => {
            this.Total = data.Total;
            data.Data.forEach((item) => {
              let selectItem = {
                Id: item.PMID,
                LabelName: item.title
              }
              this.SelectArray.push(selectItem);
            });
            this.Skip += this.Length;
            if (this.Id) {
              this.SelectIndex = this.SelectArray.findIndex((value, index, array) => {
                if (value['Id'] == this.Id) {
                  return true;
                }
                return false;
              })
            }
          },
          (err) => {
            console.log(err);
          }
        )
        break;
    }

  }
  select(event) {
    this.SelectIndex = event;
    this.setModel(this.SelectArray[event]['Id']);
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
    this.SelectArray = [];
    this.Skip = 0;
    this.getUsers();
  }
}
