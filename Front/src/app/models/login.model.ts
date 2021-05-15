import { Serializable } from './serializable';

export class LoginModel extends Serializable {
    public authenticated: boolean = false;
    public message: string = '';
    public accessToken: string = '';
    public id: string = '';
    public email: string = '';
    public imagem: string = '';
    public admin: boolean = false;
    public created: string = '';
    public expiration: string = '';
}

export class LoginParamModel extends Serializable {
    public email: string = '';
    public senha: string = '';
    public authToken: string = '';
    public idToken: string = '';
    public name: string = '';
    public photoUrl: string = '';
    public provider: string = '';
}

export class UsuarioPostParamModel extends Serializable {
    public desImagem: string = '';
    public desEmail: string = '';
    public desSenha: string = '';
    public desTelefone: string = '';
}