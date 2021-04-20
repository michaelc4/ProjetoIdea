import { Injectable } from "@angular/core";
import { HttpHeaders } from '@angular/common/http';
import { LoginModel } from '../models/login.model';

@Injectable()
export class Global {
  private loggedUser: LoginModel = new LoginModel();
  private urlApi: string = '';

  constructor() {
    this.setUrlApi("https://ucsinovaaplication.azurewebsites.net");
    this.load();
  }

  public getLoggedUser() { return this.loggedUser; }
  public setLoggedUser(value: LoginModel) {
    this.loggedUser = value;
    this.saveObject();
  }

  public getUrlApi() { return this.urlApi; }
  public setUrlApi(value: string) {
    this.urlApi = value;
    this.saveObject();
  }

  public saveObject() {
    localStorage.setItem('global', JSON.stringify({
      loggedUser: this.loggedUser,
      urlApi: this.urlApi
    }));
  }

  public load() {
    try {
      let json = localStorage.getItem('global');
      if (json) {
        let object = JSON.parse(json);
        this.loggedUser = object.loggedUser;
        this.urlApi = object.urlApi;
      }
    } catch (ex) { }
  }

  public getAutheticatedOptions() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + this.loggedUser.accessToken
      })
    };

    return httpOptions;
  }

  public getOptions() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    return httpOptions;
  }
}
