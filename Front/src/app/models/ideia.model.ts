import { Serializable } from './serializable';
import { UsuarioModel } from './usuario.model';

export class IdeiaModel extends Serializable {
    public id: string = '';
    public desIdeia: string = '';
    public desMotivoInvestir: string = '';
    public indInteresseCompartilhar: string = '1';
    public indNivelDesenvolvimento: string = '1';
    public indNivelSigilo: string = '1';
    public desComentario: string = '';
    public numPotencialDisrupcao: number = 0;
    public numPessoasImpactadas: number = 0;
    public numPotencialGanho: number = 0;
    public numValorInvestimento: number = 0;
    public numImpactoAmbiental: number = 0;
    public numPontuacaoGeral: number = 0;
    public indAtivo: string = '';
    public indAprovado: string = '';
    public dataAtualizacao: string = '';
    public dataCriacao: string = '';
    public usuarioId: string = '';
    public numLikes: number = 0;
    public usuario: UsuarioModel = new UsuarioModel();
    public anexos: Array<IdeiaAnexoModel> = new Array<IdeiaAnexoModel>();
}

export class IdeiaAnexoModel extends Serializable {
    public id: string = '';
    public desAnexo: string = '';
    public indTipoArquivo: string = '';
    public desNomeOriginal: string = '';
    public dataAtualizacao: string = '';
    public dataCriacao: string = '';
    public ideiaId: string = '';
}

export class IdeiasPagedResult extends Serializable {
    public currentPage: number = 0;
    public pageCount: number = 0;
    public pageSize: number = 0;
    public rowCount: number = 0;
    public firstRowOnPage: number = 0;
    public lastRowOnPage: number = 0;
    public results: Array<IdeiaModel> = new Array<IdeiaModel>();
}

export class IdeiaPostParamModel extends Serializable {
    public desIdeia: string = '';
    public desMotivoInvestir: string = '';
    public indInteresseCompartilhar: string = '';
    public indNivelDesenvolvimento: string = '';
    public indNivelSigilo: string = '';
    public desComentario: string = '';
    public usuarioId: string = '';
    public anexos: Array<IdeiaAnexoAddModel> = new Array<IdeiaAnexoAddModel>();
}

export class IdeiaPutParamModel extends Serializable {
    public id: string = '';
    public desIdeia: string = '';
    public desMotivoInvestir: string = '';
    public indInteresseCompartilhar: string = '';
    public indNivelDesenvolvimento: string = '';
    public indNivelSigilo: string = '';
    public desComentario: string = '';
    public numPotencialDisrupcao: number = 0;
    public numPessoasImpactadas: number = 0;
    public numPotencialGanho: number = 0;
    public numValorInvestimento: number = 0;
    public numImpactoAmbiental: number = 0;
    public numPontuacaoGeral: number = 0;
    public indAtivo: string = '';
    public indAprovado: string = '';
    public usuarioId: string = '';
    public anexos: Array<IdeiaAnexoAddModel> = new Array<IdeiaAnexoAddModel>();
}

export class IdeiaAnexoAddModel extends Serializable {
    public ideiaId: string = '';
    public desAnexo: string = '';
    public indTipoArquivo: string = '';
    public desNomeOriginal: string = '';
}

export class FiltroIdeiaModel extends Serializable {
    public ideaSearch: string = '';
    public reasonSearch: string = '';
    public shareSearch: string = '';
    public developmentSearch: string = '';
    public secretSearch: string = '';
    public approvedSearch: boolean = false;
    public registrationDateIniSearch: string = '';
    public registrationDateEndSearch: string = '';
}
