import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map'
import { Http, Headers, Response, URLSearchParams, RequestOptions, ResponseContentType } from '@angular/http';
import { Router } from '@angular/router';
import { strictEqual } from 'assert';

@Injectable()
export class DataStoreService {

  constructor(public http: Http, private router: Router) { }
  extractData(res: Response) {
    try {
      return res.json();
    }
    catch (err) {
      return res.text();
    }
  }


  handleError(error: any) {
    let errMsg = (error.message) ? error.message :
      error.status ? `${error.status} - ${error.statusText}` : "Server error";
    console.error(errMsg); // log to console instead
    return Observable.throw(errMsg);
  }
  HttpGet(model: any, url: string) {
    let params = new URLSearchParams();
    if (model) {
      for (let key in model) {
        params.set(key, model[key])
      }
    }
    let requestOptions = new RequestOptions();
    requestOptions.params = params;
    return this.http.get(url, requestOptions)
      .map(this.extractData).catch(this.handleError);
  }
  HttpPost(model: any, url: string) {
    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });
    return this.http.post(url, JSON.stringify(model), options)
      .map(this.extractData)
      .do((data) => {
      })
      .catch(this.handleError);
  }
  workingInit() {
    return this.HttpPost(null, '/api/ebmpworking/initData');
  }
  workingData(model) {
    return this.HttpPost(model, '/api/ebmpworking/getData');
  }
  workingDataByTime(model) {
    return this.HttpPost(model, '/api/ebmpworking/getDataByTime');
  }
  workingDataByUID(model) {
    return this.HttpPost(model, '/api/ebmpworking/getDataByUID');
  }

  memoData(model) {
    return this.HttpPost(model, '/api/memo/getData');
  }
  memoDataByTime(model) {
    return this.HttpPost(model, '/api/memo/getDataByTime');
  }
  memoDataByUID(model) {
    return this.HttpPost(model, '/api/memo/getDataByUID');
  }

  scheduleData(model) {
    return this.HttpPost(model, '/api/schedule/getData');
  }
  scheduleDataByTime(model) {
    return this.HttpPost(model, '/api/schedule/getDataByTime');
  }
  scheduleDataByUID(model) {
    return this.HttpPost(model, '/api/schedule/getDataByUID');
  }

  todolistInit() {
    return this.HttpPost(null, '/api/ebmtodoList/initData');
  }
  todolistData() {
    return this.HttpPost(null, '/api/ebmtodoList/getData');
  }
  createTodolist(model) {
    return this.HttpPost(model, '/api/ebmtodoList/create');
  }
}
