import { HttpClient } from '@angular/common/http';
import { LoginParamModel, UsuarioPostParamModel, LoginModel } from '../models/login.model';
import { Global } from "./global.service";
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators'

@Injectable()
export class LoginService {

    constructor(public http: HttpClient,
        public global: Global) { }

    login(param: LoginParamModel): Observable<LoginModel | any> {
        let body = {
            email: param.email,
            senha: param.senha,
            authToken: param.authToken,
            idToken: param.idToken,
            name: param.name,
            photoUrl: param.photoUrl,
            provider: param.provider,
        };

        let url = this.global.getUrlApi() + "/api/login";

        return this.http
            .post(url, body, this.global.getOptions())
            .pipe(map((res: any) => {
                return res as LoginModel;
            }), catchError((err: any) => {
                return err;
            }));
    }

    create(param: UsuarioPostParamModel): Observable<any> {
        let body = {
            desImagem: param.desImagem,
            desEmail: param.desEmail,
            desSenha: param.desSenha,
            desTelefone: param.desTelefone
        };

        let url = this.global.getUrlApi() + "/api/login/createuser";

        return this.http
            .post(url, body, this.global.getOptions())
            .pipe(map((res: any) => {
                return res;
            }), catchError((err: any) => {
                return err;
            }));
    }
}
