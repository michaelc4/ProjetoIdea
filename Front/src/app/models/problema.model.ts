import { Serializable } from './serializable';
import { UsuarioModel } from './usuario.model';

export class ProblemaModel extends Serializable {
    public id: string = '';
    public desProblema: string = '';
    public indTipoBeneficio: string = '';
    public indTipoSolucao: string = '';
    public numProbabilidadeInvestir: number = 0;
    public indAtivo: string = '';
    public indAprovado: string = '';
    public dataAtualizacao: string = '';
    public dataCriacao: string = '';
    public usuarioId: string = '';
    public numLikes: number = 0;
    public usuario: UsuarioModel = new UsuarioModel();
    public anexos: Array<ProblemaAnexoModel> = new Array<ProblemaAnexoModel>();
}

export class ProblemaAnexoModel extends Serializable {
    public id: string = '';
    public desAnexo: string = '';
    public indTipoArquivo: string = '';
    public desNomeOriginal: string = '';
    public dataAtualizacao: string = '';
    public dataCriacao: string = '';
    public problemaId: string = '';
}

export class ProblemasPagedResult extends Serializable {
    public currentPage: number = 0;
    public pageCount: number = 0;
    public pageSize: number = 0;
    public rowCount: number = 0;
    public firstRowOnPage: number = 0;
    public lastRowOnPage: number = 0;
    public results: Array<ProblemaModel> = new Array<ProblemaModel>();
}

export class ProblemaPostParamModel extends Serializable {
    public desProblema: string = '';
    public indTipoBeneficio: string = '';
    public indTipoSolucao: string = '';
    public numProbabilidadeInvestir: number = 0;
    public usuarioId: string = '';
    public anexos: Array<ProblemaAnexoAddModel> = new Array<ProblemaAnexoAddModel>();
}

export class ProblemaPutParamModel extends Serializable {
    public id: string = '';
    public desProblema: string = '';
    public indTipoBeneficio: string = '';
    public indTipoSolucao: string = '';
    public numProbabilidadeInvestir: number = 0;
    public indAtivo: string = '';
    public indAprovado: string = '';
    public usuarioId: string = '';
    public anexos: Array<ProblemaAnexoAddModel> = new Array<ProblemaAnexoAddModel>();
}

export class ProblemaAnexoAddModel extends Serializable {
    public problemaId: string = '';
    public desAnexo: string = '';
    public indTipoArquivo: string = '';
    public desNomeOriginal: string = '';
}
