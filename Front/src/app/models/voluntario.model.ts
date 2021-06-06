import { Serializable } from './serializable';
import { UsuarioModel } from './usuario.model';
import { IdeiaModel } from './ideia.model';
import { ProblemaModel } from './problema.model';

export class VoluntarioModel extends Serializable {
    public id: string = '';
    public usuarioId: string = '';
    public usuario: UsuarioModel = new UsuarioModel();
    public ideiaId: string = '';
    public ideia: IdeiaModel = new IdeiaModel();
    public problemaId: string = '';
    public problema: ProblemaModel = new ProblemaModel();
}

export class VoluntariosPagedResult extends Serializable {
    public currentPage: number = 0;
    public pageCount: number = 0;
    public pageSize: number = 0;
    public rowCount: number = 0;
    public firstRowOnPage: number = 0;
    public lastRowOnPage: number = 0;
    public results: Array<VoluntarioModel> = new Array<VoluntarioModel>();
}

export class VoluntarioPostParamModel extends Serializable {
    public usuarioId: string = '';
    public ideiaId: string = '';
    public problemaId: string = '';
}

export class VoluntarioPutParamModel extends Serializable {
    public id: string = '';
    public usuarioId: string = '';
    public ideiaId: string = '';
    public problemaId: string = '';
}
