import { HttpClient } from '@angular/common/http';
import { LikePostParamModel } from '../models/like.model';
import { Global } from "./global.service";
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';

@Injectable()
export class LikeService {

    constructor(public http: HttpClient,
        public global: Global) { }

    // Get
    getIPAddress(): Observable<any> {
        return this.http
            .get("http://api.ipify.org/?format=json")
            .pipe(map((res: any) => {
                return res;
            }), catchError((err: any) => {
                return err;
            }));
    }

    // Post
    post(param: LikePostParamModel): Observable<any> {
        let body = {
            key: 'ac5cc8c6-e508-4354-bb12-634a6390900b',
            ipUsr: param.ipUsr,
            ideiaId: param.ideiaId && param.ideiaId != '' ? param.ideiaId : null,
            problemaId: param.problemaId && param.problemaId != '' ? param.problemaId : null
        };

        let url = this.global.getUrlApi() + "/api/like/post";

        return this.http
            .post(url, body, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res;
            }), catchError((err: any) => {
                return err;
            }));
    }
}
