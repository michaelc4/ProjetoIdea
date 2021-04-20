import { Serializable } from './serializable';

export class UsuarioModel extends Serializable {
    public id: string = '';
    public desImagem: string = '';
    public desEmail: string = '';
    public desTelefone: string = '';
    public desEspecialidade: string = '';
    public desExperiencia: string = '';
    public admin: number = 0;
    public local: number = 0;
}

export class PagedResult extends Serializable {
    public currentPage: number = 0;
    public pageCount: number = 0;
    public pageSize: number = 0;
    public rowCount: number = 0;
    public firstRowOnPage: number = 0;
    public lastRowOnPage: number = 0;
    public results: Array<UsuarioModel> = new Array<UsuarioModel>();
}