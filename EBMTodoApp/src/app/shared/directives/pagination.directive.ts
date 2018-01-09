import { Directive, Input, SimpleChanges, OnInit, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Directive({
  selector: '[appPagination]',
  exportAs: 'p-data'
})
export class PaginationDirective {
  list = new Array();
  numberOfPages = 0;
  @Input() set data(d) {
    this.list = d;
    this.numberOfPages = this.getNumberOfPages();
    this.setSort();
    this.loadList();
  }
  @Input() pageList = new Array();
  @Input() currentPage = 1;
  @Input() numberPerPage = 10;

  @Input() sort = {};//true:desc,false:asc
  @Output() sorting = new EventEmitter<string>();
  @Output() paging = new EventEmitter<number>();
  constructor() { }
  public changeSort(prop: string) {
    this.sorting.emit(prop);
    if (this.sort[prop] === "undefined") {
      this.sort[prop] = true;
    }
    if (this.sort[prop]) {
      this.sort[prop] = false;
      this.list.sort((x, y) => {
        if (x[prop] && y[prop]) {
          if (typeof x[prop] === "string") {
            return x[prop].localeCompare(y[prop]);
          }
          if (typeof x[prop] === "number") {
            return x[prop] - y[prop];
          }
        }
        return 0;
      })
    }
    else {
      this.sort[prop] = true;
      this.list.sort((x, y) => {
        if (x[prop] && y[prop]) {
          if (typeof x[prop] === "string") {
            return y[prop].localeCompare(x[prop]);
          }
          if (typeof x[prop] === "number") {
            return y[prop] - x[prop];
          }
        }
        return 0;
      })
    }
    this.loadList();
  }

  public getNumberOfPages() {
    return Math.ceil(this.list.length / this.numberPerPage);
  }

  public getPageArr(num: number) {
    let arr = new Array<number>();
    let start = this.currentPage;
    let end = start + num - 1 > this.numberOfPages ? this.numberOfPages : start + num - 1;
    for (var i = start; i <= end; i++) {
      arr.push(i);
    }

    if (arr.length < num) {
      for (var i = 0; i <= num - arr.length; i++) {
        if (this.currentPage - 1 - i <= 0) {
          break;
        }
        arr.unshift(this.currentPage - 1 - i);
      }
    }
    return arr;
  }

  public changePage(num: number) {
    if (num <= this.numberOfPages && num > 0) {
      this.currentPage = num;
      this.loadList();
      this.paging.emit(this.currentPage);
    }

  }
  public nextPage() {
    if (this.currentPage < this.numberOfPages) {
      this.currentPage += 1;
      this.loadList();
      this.paging.emit(this.currentPage);
    }
  }

  public previousPage() {
    if (this.currentPage > 1) {
      this.currentPage -= 1;
      this.loadList();
      this.paging.emit(this.currentPage);
    }
  }

  public firstPage() {
    this.currentPage = 1;
    this.loadList();
    this.paging.emit(this.currentPage);
  }

  public lastPage() {
    this.currentPage = this.numberOfPages;
    this.loadList();
    this.paging.emit(this.currentPage);
  }
  private setSort() {
    Object.keys(this.sort).forEach((prop) => {
      if (!this.sort[prop]) {
        this.list.sort((x, y) => {
          if (x[prop] && y[prop]) {
            if (typeof x[prop] === "string") {
              return x[prop].localeCompare(y[prop]);
            }
            if (typeof x[prop] === "number") {
              return x[prop] - y[prop];
            }
          }
          return 0;
        })
      }
      else {
        this.list.sort((x, y) => {
          if (x[prop] && y[prop]) {
            if (typeof x[prop] === "string") {
              return y[prop].localeCompare(x[prop]);
            }
            if (typeof x[prop] === "number") {
              return y[prop] - x[prop];
            }
          }
          return 0;
        })
      }
    })
  }
  public loadList() {
    var begin = ((this.currentPage - 1) * this.numberPerPage);
    var end = begin + this.numberPerPage;
    this.pageList = this.list.slice(begin, end);
  }
}
