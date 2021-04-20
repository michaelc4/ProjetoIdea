import { HttpClient } from '@angular/common/http';
import { PagedResult } from '../models/usuario.model';
import { Global } from "./global.service";
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators'

@Injectable()
export class UsuarioService {

    constructor(public http: HttpClient,
        public global: Global) { }

    getAllPaged(): Observable<PagedResult> {
        let url = this.global.getUrlApi() + "/api/usuario/getallpaged?page=1&pageSize=10";

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res;
            }));
    }
}
