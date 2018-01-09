import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/of';
import { Http, Headers, Response, URLSearchParams, RequestOptions, ResponseContentType } from '@angular/http';
import { Router } from '@angular/router';
import { strictEqual } from 'assert';
import { StoreType } from '../models/store-type.enum';

@Injectable()
export class DataStoreService {

  constructor(public http: Http, private router: Router) {
  }
  dataStore = new Map<StoreType, any>();
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
    if (this.dataStore.has(StoreType.LineUser) && this.dataStore.has(StoreType.Project)) {
      return Observable.of({ lineusers: this.dataStore.get(StoreType.LineUser), projects: this.dataStore.get(StoreType.Project) });
    }
    else {
      return this.HttpPost(null, '/api/ebmpworking/initData').do(
        (data) => {
          this.dataStore.set(StoreType.LineUser, data.lineusers);
          this.dataStore.set(StoreType.Project, data.projects);
        }
      );
    }
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
    if (this.dataStore.has(StoreType.Member)) {
      return Observable.of(this.dataStore.get(StoreType.Member));
    }
    else {
      return this.HttpPost(null, '/api/ebmtodoList/initData').do(
        (data) => {
          console.log(data)
          this.dataStore.set(StoreType.Member, data);
        }
      );
    }
  }
  todolistData() {
    return this.HttpPost(null, '/api/ebmtodoList/getData');
  }
  createOrUpdateTodolist(model) {
    return this.HttpPost(model, '/api/ebmtodoList/CreateOrUpdate');
  }

  onlineData() {
    return this.HttpPost(null, '/api/ebmonline/getData');
  }
  onlineInsert(model) {
    return this.HttpPost(model, '/api/ebmonline/insert');
  }
  onlineUpdate(model) {
    return this.HttpPost(model, '/api/ebmonline/update');
  }
  onlineDelete(model) {
    return this.HttpPost(model, '/api/ebmonline/delete');
  }

  projectData(model: any) {
    return this.HttpPost(model, '/api/back/EBMProject/GetList');
  }
  projectCreate(model){
    return this.HttpPost(model, '/api/back/EBMProject/Create');
  }
  projectUpdate(model){
    return this.HttpPost(model, '/api/back/EBMProject/Update');
  }
  projectDelete(model){
    return this.HttpPost(model, '/api/back/EBMProject/Delete');
  }

  projectMemberData(model){
    return this.HttpPost(model, '/api/back/EBMProjectMember/GetList');
  }
  projectMemberCreate(model){
    return this.HttpPost(model, '/api/back/EBMProjectMember/Create');
  }
  projectMemberUpdate(model){
    return this.HttpPost(model, '/api/back/EBMProjectMember/Update');
  }
  projectMemberDelete(model){
    return this.HttpPost(model, '/api/back/EBMProjectMember/Delete');
  }

  projectWorkingData(model){
    return this.HttpPost(model, '/api/back/EBMProjectWorking/GetList');
  }

  userData(model){
    return this.HttpPost(model, '/api/back/EBMUser/GetList');
  }
  userCreate(model){
    return this.HttpPost(model, '/api/back/EBMUser/Create');
  }
  userUpdate(model){
    return this.HttpPost(model, '/api/back/EBMUser/Update');
  }
  userDelete(model){
    return this.HttpPost(model, '/api/back/EBMUser/Delete');
  }
  lineuserData(model){
    return this.HttpPost(model, '/api/back/EBMUser/GetLineList');
  }
  projectTodoListData(model){
    return this.HttpPost(model, '/api/back/EBMProjectTodoList/GetList');
  }
  projectTodoListCreate(model){
    return this.HttpPost(model, '/api/back/EBMProjectTodoList/Create');
  }
  projectTodoListUpdate(model){
    return this.HttpPost(model, '/api/back/EBMProjectTodoList/Update');
  }
  projectTodoListDelete(model){
    return this.HttpPost(model, '/api/back/EBMProjectTodoList/Delete');
  }
}
