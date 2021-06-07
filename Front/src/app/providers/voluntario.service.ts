import { HttpClient } from '@angular/common/http';
import { VoluntarioModel, VoluntarioPostParamModel, VoluntarioPutParamModel, VoluntariosPagedResult } from '../models/voluntario.model';
import { Global } from "./global.service";
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';

@Injectable()
export class VoluntarioService {

    constructor(public http: HttpClient,
        public global: Global) { }

    // Get
    get(id: string): Observable<VoluntarioModel | any> {
        let url = this.global.getUrlApi() + "/api/voluntario/get?id=" + id;

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as VoluntarioModel;
            }), catchError((err: any) => {
                return err;
            }));
    }

    getAll(): Observable<Array<VoluntarioModel> | any> {
        let url = this.global.getUrlApi() + "/api/voluntario/getall";

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as Array<VoluntarioModel>;
            }), catchError((err: any) => {
                return err;
            }));
    }

    getAllPaged(page: number, pageSize: number): Observable<VoluntariosPagedResult | any> {
        let url = this.global.getUrlApi() + "/api/voluntario/getallpaged?page=" + page + "&pageSize=" + pageSize;

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as VoluntariosPagedResult;
            }), catchError((err: any) => {
                return err;
            }));
    }

    getAllPagedByUser(page: number, pageSize: number, userId: string): Observable<VoluntariosPagedResult | any> {
        let url = this.global.getUrlApi() + "/api/voluntario/getallpagedbyuser?page=" + page + "&pageSize=" + pageSize + "&userId=" + userId;

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as VoluntariosPagedResult;
            }), catchError((err: any) => {
                return err;
            }));
    }

    getAllPagedByProblemOrIdeia(page: number, pageSize: number, problemId?: string, ideaId?: string): Observable<VoluntariosPagedResult | any> {
        let url = this.global.getUrlApi() + "/api/voluntario/getallpagedbyproblemorideia?page=" + page + "&pageSize=" + pageSize;

        if (problemId && problemId.trim() != '') {
            url += "&problemId=" + problemId;
        }

        if (ideaId && ideaId.trim() != '') {
            url += "&ideaId=" + ideaId;
        }

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as VoluntariosPagedResult;
            }), catchError((err: any) => {
                return err;
            }));
    }

    // Post, Put, Delete
    post(param: VoluntarioPostParamModel): Observable<any> {
        let body = {
            usuarioId: param.usuarioId,
            ideiaId: param.ideiaId && param.ideiaId != '' ? param.ideiaId : null,
            problemaId: param.problemaId && param.problemaId != '' ? param.problemaId : null
        };

        let url = this.global.getUrlApi() + "/api/voluntario/post";

        return this.http
            .post(url, body, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res;
            }), catchError((err: any) => {
                return err;
            }));
    }

    put(param: VoluntarioPutParamModel): Observable<any> {
        let body = {
            id: param.id,
            usuarioId: param.usuarioId,
            ideiaId: param.ideiaId,
            problemaId: param.problemaId
        };

        let url = this.global.getUrlApi() + "/api/voluntario/put";

        return this.http
            .put(url, body, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res;
            }), catchError((err: any) => {
                return err;
            }));
    }

    delete(id: string): Observable<any> {
        let body = {};

        let url = this.global.getUrlApi() + "/api/voluntario/delete?id=" + id;

        return this.http
            .put(url, body, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res;
            }), catchError((err: any) => {
                return err;
            }));
    }
}
