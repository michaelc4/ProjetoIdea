import { Serializable } from './serializable';

export class UsuarioModel extends Serializable {
    public id: string = '';
    public desNome: string = '';
    public desImagem: string = '';
    public desEmail: string = '';
    public desTelefone: string = '';
    public desEspecialidade: string = '';
    public desExperiencia: string = '';
    public admin: number = 0;
    public local: number = 0;
}

export class UsuariosPagedResult extends Serializable {
    public currentPage: number = 0;
    public pageCount: number = 0;
    public pageSize: number = 0;
    public rowCount: number = 0;
    public firstRowOnPage: number = 0;
    public lastRowOnPage: number = 0;
    public results: Array<UsuarioModel> = new Array<UsuarioModel>();
}

export class UsuarioPostParamModel extends Serializable {
    public desNome: string = '';
    public desImagem: string = '';
    public desEmail: string = '';
    public desSenha: string = '';
    public desTelefone: string = '';
}

export class UsuarioPutParamModel extends Serializable {
    public id: string = '';
    public desNome: string = '';
    public desImagem: string = '';
    public desSenha: string = '';
    public desTelefone: string = '';
    public desEspecialidade: string = '';
    public desExperiencia: string = '';
}
