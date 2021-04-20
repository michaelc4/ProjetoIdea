import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SocialAuthService } from "angularx-social-login";
import { FacebookLoginProvider, GoogleLoginProvider } from "angularx-social-login";
import { LoginService } from '../../providers/login.service';
import { Global } from '../../providers/global.service';
import { LoginParamModel, UsuarioPostParamModel, LoginModel } from '../../models/login.model';
import { NotifierService } from 'angular-notifier';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [LoginService]
})
export class LoginComponent {
  newUser: boolean = false;
  userimage: string = '';
  username: string = '';
  password: string = '';
  confirmPassword: string = '';
  fone: string = '';

  constructor(private socialservice: SocialAuthService,
    private global: Global,
    private loginService: LoginService,
    private router: Router,
    private notifierService: NotifierService) { }

  ngOnInit() {
    this.cleanData();
    this.socialservice.authState.subscribe((user) => {
      let param = new LoginParamModel();
      param.email = user.email;
      param.senha = '';
      param.authToken = user.authToken;
      param.idToken = user.idToken;
      param.name = user.name;
      param.photoUrl = user.photoUrl;
      param.provider = user.provider;
      this.loginService.login(param).subscribe((data: LoginModel) => {
        this.global.setLoggedUser(data);
        this.router.navigateByUrl('/usuarios');
      });
    });
  }

  socialsigin(platform: string) {
    let socialPlatformProvider = FacebookLoginProvider.PROVIDER_ID;
    if (platform == "facebook") {
      socialPlatformProvider = FacebookLoginProvider.PROVIDER_ID;
    } else if (platform == "gmail") {
      socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;
    }
    this.socialservice.signIn(socialPlatformProvider);
  }

  login() {
    let param = new LoginParamModel();
    param.email = this.username;
    param.senha = this.password;
    param.provider = 'LOCAL';
    this.loginService.login(param).subscribe((data: LoginModel) => {
      this.notifierService.notify('success', 'Login com sucesso');
      this.global.setLoggedUser(data);
      this.router.navigateByUrl('/usuarios');
    });
  }

  cleanData() {
    this.newUser = false;
    this.userimage = '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';
    this.username = '';
    this.password = '';
    this.confirmPassword = '';
    this.fone = '';
  }

  createUser() {
    this.cleanData();
    this.newUser = true;
  }

  cancel() {
    this.cleanData();
  }

  create() {
    let user = new UsuarioPostParamModel();
    user.desImagem = this.userimage;
    user.desEmail = this.username;
    user.desSenha = this.password;
    user.desTelefone = this.fone;
    this.loginService.create(user).subscribe((data: any) => {
      this.notifierService.notify('success', 'Usuário Criado');
      this.cleanData();
    });
  }

  processFile(imageInput: any) {
    const file: File = imageInput.files[0];
    const reader = new FileReader();

    reader.addEventListener('load', (event: any) => {
      this.userimage = event.target.result.replace('data:image/jpeg;base64,', '');
    });

    reader.readAsDataURL(file);
  }
}
