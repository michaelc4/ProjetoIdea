import { HttpClient } from '@angular/common/http';
import { IdeiaModel, IdeiaPostParamModel, IdeiaPutParamModel, IdeiasPagedResult } from '../models/ideia.model';
import { Global } from "./global.service";
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';

@Injectable()
export class IdeiaService {

    constructor(public http: HttpClient,
        public global: Global) { }

    // Get
    get(id: string): Observable<IdeiaModel | any> {
        let url = this.global.getUrlApi() + "/api/ideia/get?id=" + id;

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as IdeiaModel;
            }), catchError((err: any) => {
                return err;
            }));
    }

    getAll(): Observable<Array<IdeiaModel> | any> {
        let url = this.global.getUrlApi() + "/api/ideia/getall";

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as Array<IdeiaModel>;
            }), catchError((err: any) => {
                return err;
            }));
    }

    getAllPaged(page: number, pageSize: number, ideaSearch?: string, reasonSearch?: string, shareSearch?: string, developmentSearch?: string, secretSearch?: string, approvedSearch?: string, registrationDateIniSearch?: string, registrationDateEndSearch?: string): Observable<IdeiasPagedResult | any> {
        let url = this.global.getUrlApi() + "/api/ideia/getallpaged?page=" + page + "&pageSize=" + pageSize;

        if (ideaSearch && ideaSearch.trim() != '') {
            url += "&ideaSearch=" + ideaSearch;
        }

        if (reasonSearch && reasonSearch.trim() != '') {
            url += "&reasonSearch=" + reasonSearch;
        }

        if (shareSearch && shareSearch.trim() != '') {
            url += "&shareSearch=" + shareSearch;
        }

        if (developmentSearch && developmentSearch.trim() != '') {
            url += "&developmentSearch=" + developmentSearch;
        }

        if (secretSearch && secretSearch.trim() != '') {
            url += "&secretSearch=" + secretSearch;
        }

        if (approvedSearch && approvedSearch.trim() != '') {
            url += "&approvedSearch=" + approvedSearch;
        }

        if (registrationDateIniSearch && registrationDateIniSearch.trim() != '') {
            url += "&registrationDateIniSearch=" + registrationDateIniSearch;
        }

        if (registrationDateEndSearch && registrationDateEndSearch.trim() != '') {
            url += "&registrationDateEndSearch=" + registrationDateEndSearch;
        }

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as IdeiasPagedResult;
            }), catchError((err: any) => {
                return err;
            }));
    }

    getAllPagedByUser(page: number, pageSize: number, userId: string, ideaSearch?: string, reasonSearch?: string, shareSearch?: string, developmentSearch?: string, secretSearch?: string, approvedSearch?: string, registrationDateIniSearch?: string, registrationDateEndSearch?: string): Observable<IdeiasPagedResult | any> {
        let url = this.global.getUrlApi() + "/api/ideia/getallpagedbyuser?page=" + page + "&pageSize=" + pageSize + "&userId=" + userId;

        if (ideaSearch && ideaSearch.trim() != '') {
            url += "&ideaSearch=" + ideaSearch;
        }

        if (reasonSearch && reasonSearch.trim() != '') {
            url += "&reasonSearch=" + reasonSearch;
        }

        if (shareSearch && shareSearch.trim() != '') {
            url += "&shareSearch=" + shareSearch;
        }

        if (developmentSearch && developmentSearch.trim() != '') {
            url += "&developmentSearch=" + developmentSearch;
        }

        if (secretSearch && secretSearch.trim() != '') {
            url += "&secretSearch=" + secretSearch;
        }

        if (approvedSearch && approvedSearch.trim() != '') {
            url += "&approvedSearch=" + approvedSearch;
        }

        if (registrationDateIniSearch && registrationDateIniSearch.trim() != '') {
            url += "&registrationDateIniSearch=" + registrationDateIniSearch;
        }

        if (registrationDateEndSearch && registrationDateEndSearch.trim() != '') {
            url += "&registrationDateEndSearch=" + registrationDateEndSearch;
        }

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as IdeiasPagedResult;
            }), catchError((err: any) => {
                return err;
            }));
    }

    getAllPagedInitialScreen(page: number, pageSize: number): Observable<IdeiasPagedResult | any> {
        let url = this.global.getUrlApi() + "/api/ideia/getallpagedinitialscreen?page=" + page + "&pageSize=" + pageSize;

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as IdeiasPagedResult;
            }), catchError((err: any) => {
                return err;
            }));
    }

    // Post, Put, Delete
    post(param: IdeiaPostParamModel): Observable<any> {
        let body = {
            desIdeia: param.desIdeia,
            desMotivoInvestir: param.desMotivoInvestir,
            indInteresseCompartilhar: param.indInteresseCompartilhar,
            indNivelDesenvolvimento: param.indNivelDesenvolvimento,
            indNivelSigilo: param.indNivelSigilo,
            desComentario: param.desComentario,
            usuarioId: param.usuarioId,
            anexos: param.anexos
        };

        let url = this.global.getUrlApi() + "/api/ideia/post";

        return this.http
            .post(url, body, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res;
            }), catchError((err: any) => {
                return err;
            }));
    }

    put(param: IdeiaPutParamModel): Observable<any> {
        let body = {
            id: param.id,
            desIdeia: param.desIdeia,
            desMotivoInvestir: param.desMotivoInvestir,
            indInteresseCompartilhar: param.indInteresseCompartilhar,
            indNivelDesenvolvimento: param.indNivelDesenvolvimento,
            indNivelSigilo: param.indNivelSigilo,
            desComentario: param.desComentario,
            usuarioId: param.usuarioId,
            anexos: param.anexos
        };

        let url = this.global.getUrlApi() + "/api/ideia/put";

        return this.http
            .put(url, body, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res;
            }), catchError((err: any) => {
                return err;
            }));
    }

    putAvaliacao(param: IdeiaPutParamModel): Observable<any> {
        let body = {
            id: param.id,
            desIdeia: param.desIdeia,
            desMotivoInvestir: param.desMotivoInvestir,
            indInteresseCompartilhar: param.indInteresseCompartilhar,
            indNivelDesenvolvimento: param.indNivelDesenvolvimento,
            indNivelSigilo: param.indNivelSigilo,
            desComentario: param.desComentario,
            numPotencialDisrupcao: param.numPotencialDisrupcao,
            numPessoasImpactadas: param.numPessoasImpactadas,
            numPotencialGanho: param.numPotencialGanho,
            numValorInvestimento: param.numValorInvestimento,
            numImpactoAmbiental: param.numImpactoAmbiental,
            numPontuacaoGeral: param.numPontuacaoGeral,
            indAtivo: param.indAtivo,
            indAprovado: param.indAprovado,
            usuarioId: param.usuarioId,
            anexos: param.anexos
        };

        let url = this.global.getUrlApi() + "/api/ideia/putavaliacao";

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

        let url = this.global.getUrlApi() + "/api/ideia/delete?id=" + id;

        return this.http
            .put(url, body, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res;
            }), catchError((err: any) => {
                return err;
            }));
    }
}
