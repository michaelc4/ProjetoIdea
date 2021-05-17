import { HttpClient } from '@angular/common/http';
import { UsuarioModel, UsuarioPostParamModel, UsuarioPutParamModel, UsuariosPagedResult } from '../models/usuario.model';
import { Global } from "./global.service";
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';

@Injectable()
export class UsuarioService {

    constructor(public http: HttpClient,
        public global: Global) { }

    // Get
    get(id: string): Observable<UsuarioModel | any> {
        let url = this.global.getUrlApi() + "/api/usuario/get?id=" + id;

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as UsuarioModel;
            }), catchError((err: any) => {
                return err;
            }));
    }

    getAll(): Observable<Array<UsuarioModel> | any> {
        let url = this.global.getUrlApi() + "/api/usuario/getall";

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as Array<UsuarioModel>;
            }), catchError((err: any) => {
                return err;
            }));
    }

    getAllPaged(page: number, pageSize: number, nameSearch?: string, emailSearch?: string, foneSearch?: string, isAdminSearch?: boolean): Observable<UsuariosPagedResult | any> {
        let url = this.global.getUrlApi() + "/api/usuario/getallpaged?page=" + page + "&pageSize=" + pageSize;

        if (nameSearch && nameSearch.trim() != '') {
            url += "&nameSearch=" + nameSearch;
        }

        if (emailSearch && emailSearch.trim() != '') {
            url += "&emailSearch=" + emailSearch;
        }

        if (foneSearch && foneSearch.trim() != '') {
            url += "&foneSearch=" + foneSearch;
        }

        if (isAdminSearch) {
            url += "&isAdminSearch=" + isAdminSearch;
        }

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as UsuariosPagedResult;
            }), catchError((err: any) => {
                return err;
            }));
    }

    // Post, Put, Delete
    post(param: UsuarioPostParamModel): Observable<any> {
        let body = {
            desNome: param.desNome,
            desImagem: param.desImagem,
            desEmail: param.desEmail,
            desSenha: param.desSenha,
            desTelefone: param.desTelefone
        };

        let url = this.global.getUrlApi() + "/api/usuario/post";

        return this.http
            .post(url, body, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res;
            }), catchError((err: any) => {
                return err;
            }));
    }

    put(param: UsuarioPutParamModel): Observable<any> {
        let body = {
            id: param.id,
            desNome: param.desNome,
            desImagem: param.desImagem,
            desSenha: param.desSenha,
            desTelefone: param.desTelefone,
            desEspecialidade: param.desEspecialidade,
            desExperiencia: param.desExperiencia
        };

        let url = this.global.getUrlApi() + "/api/usuario/put";

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

        let url = this.global.getUrlApi() + "/api/usuario/delete?id=" + id;

        return this.http
            .put(url, body, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res;
            }), catchError((err: any) => {
                return err;
            }));
    }
}
