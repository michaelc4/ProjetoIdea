import { HttpClient } from '@angular/common/http';
import { ProblemaModel, ProblemaPostParamModel, ProblemaPutParamModel, ProblemasPagedResult } from '../models/problema.model';
import { Global } from "./global.service";
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';

@Injectable()
export class ProblemaService {

    constructor(public http: HttpClient,
        public global: Global) { }

    // Get
    get(id: string): Observable<ProblemaModel | any> {
        let url = this.global.getUrlApi() + "/api/problema/get?id=" + id;

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as ProblemaModel;
            }), catchError((err: any) => {
                return err;
            }));
    }

    getAll(): Observable<Array<ProblemaModel> | any> {
        let url = this.global.getUrlApi() + "/api/problema/getall";

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as Array<ProblemaModel>;
            }), catchError((err: any) => {
                return err;
            }));
    }

    getAllPaged(page: number, pageSize: number, problemSearch?: string, benefitTypeSearch?: string, solutionTypeSearch?: string, approvedSearch?: string, registrationDateIniSearch?: string, registrationDateEndSearch?: string): Observable<ProblemasPagedResult | any> {
        let url = this.global.getUrlApi() + "/api/problema/getallpaged?page=" + page + "&pageSize=" + pageSize;

        if (problemSearch && problemSearch.trim() != '') {
            url += "&problemSearch=" + problemSearch;
        }

        if (benefitTypeSearch && benefitTypeSearch.trim() != '') {
            url += "&benefitTypeSearch=" + benefitTypeSearch;
        }

        if (solutionTypeSearch && solutionTypeSearch.trim() != '') {
            url += "&solutionTypeSearch=" + solutionTypeSearch;
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
                return res as ProblemasPagedResult;
            }), catchError((err: any) => {
                return err;
            }));
    }

    getAllPagedByUser(page: number, pageSize: number, userId: string, problemSearch?: string, benefitTypeSearch?: string, solutionTypeSearch?: string, approvedSearch?: string, registrationDateIniSearch?: string, registrationDateEndSearch?: string): Observable<ProblemasPagedResult | any> {
        let url = this.global.getUrlApi() + "/api/problema/getallpagedbyuser?page=" + page + "&pageSize=" + pageSize + "&userId=" + userId;

        if (problemSearch && problemSearch.trim() != '') {
            url += "&problemSearch=" + problemSearch;
        }

        if (benefitTypeSearch && benefitTypeSearch.trim() != '') {
            url += "&benefitTypeSearch=" + benefitTypeSearch;
        }

        if (solutionTypeSearch && solutionTypeSearch.trim() != '') {
            url += "&solutionTypeSearch=" + solutionTypeSearch;
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
                return res as ProblemasPagedResult;
            }), catchError((err: any) => {
                return err;
            }));
    }

    getAllPagedInitialScreen(page: number, pageSize: number): Observable<ProblemasPagedResult | any> {
        let url = this.global.getUrlApi() + "/api/problema/getallpagedinitialscreen?page=" + page + "&pageSize=" + pageSize;

        return this.http
            .get(url, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res as ProblemasPagedResult;
            }), catchError((err: any) => {
                return err;
            }));
    }

    // Post, Put, Delete
    post(param: ProblemaPostParamModel): Observable<any> {
        let body = {
            desProblema: param.desProblema,
            desSolucao: param.desSolucao,
            indTipoBeneficio: param.indTipoBeneficio,
            indTipoSolucao: param.indTipoSolucao,
            numProbabilidadeInvestir: param.numProbabilidadeInvestir,
            usuarioId: param.usuarioId,
            anexos: param.anexos
        };

        let url = this.global.getUrlApi() + "/api/problema/post";

        return this.http
            .post(url, body, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res;
            }), catchError((err: any) => {
                return err;
            }));
    }

    put(param: ProblemaPutParamModel): Observable<any> {
        let body = {
            id: param.id,
            desProblema: param.desProblema,
            desSolucao: param.desSolucao,
            indTipoBeneficio: param.indTipoBeneficio,
            indTipoSolucao: param.indTipoSolucao,
            numProbabilidadeInvestir: param.numProbabilidadeInvestir,
            usuarioId: param.usuarioId,
            anexos: param.anexos
        };

        let url = this.global.getUrlApi() + "/api/problema/put";

        return this.http
            .put(url, body, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res;
            }), catchError((err: any) => {
                return err;
            }));
    }

    putAvaliacao(param: ProblemaPutParamModel): Observable<any> {
        let body = {
            id: param.id,
            desProblema: param.desProblema,
            indTipoBeneficio: param.indTipoBeneficio,
            indTipoSolucao: param.indTipoSolucao,
            numProbabilidadeInvestir: param.numProbabilidadeInvestir,
            indAtivo: param.indAtivo,
            indAprovado: param.indAprovado,
            usuarioId: param.usuarioId,
            anexos: param.anexos
        };

        let url = this.global.getUrlApi() + "/api/problema/putavaliacao";

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

        let url = this.global.getUrlApi() + "/api/problema/delete?id=" + id;

        return this.http
            .put(url, body, this.global.getAutheticatedOptions())
            .pipe(map((res: any) => {
                return res;
            }), catchError((err: any) => {
                return err;
            }));
    }
}
