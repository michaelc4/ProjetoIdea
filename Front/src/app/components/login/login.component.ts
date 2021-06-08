import { Component, OnDestroy, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { SocialAuthService } from "angularx-social-login";
import { FacebookLoginProvider, GoogleLoginProvider } from "angularx-social-login";
import { LoginService } from '../../providers/login.service';
import { Global } from '../../providers/global.service';
import { LoginParamModel, UsuarioPostParamModel } from '../../models/login.model';
import { NotifierService } from 'angular-notifier';
import { takeWhile } from "rxjs/operators";
import { NgxSpinnerService } from "ngx-spinner";
import { validateEmail, isValidImage } from '../../functions/string.functions';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [LoginService]
})
export class LoginComponent implements OnDestroy {
  newUser: boolean = false;
  userimage: string = '';
  username: string = '';
  password: string = '';
  confirmPassword: string = '';
  fone: string = '';
  alive: boolean = true;
  social: boolean = false;

  constructor(private socialservice: SocialAuthService,
    private global: Global,
    private loginService: LoginService,
    private router: Router,
    private notifierService: NotifierService,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.cleanData();
    this.socialservice.authState
      .pipe(takeWhile(() => this.alive))
      .subscribe((user: any) => {
        if (user) {
          this.social = true;
          let param = new LoginParamModel();
          param.email = user.email;
          param.senha = '';
          param.authToken = user.authToken;
          param.idToken = user.idToken;
          param.name = user.name;
          param.photoUrl = user.photoUrl;
          param.provider = user.provider;
          this.spinner.show();
          this.loginService.login(param)
            .pipe(takeWhile(() => this.alive))
            .subscribe((data: any) => {
              this.spinner.hide();
              this.notifierService.notify('success', 'Login com sucesso');
              this.global.setLoggedUser(data);
              this.router.navigateByUrl('/');
            });
        }
      });
  }

  ngOnDestroy() {
    if (this.socialservice && this.social) {
      this.socialservice.signOut();
    }
    this.alive = false;
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
    let isError = false;
    if (!this.username || this.username.trim() == '' || !validateEmail(this.username.trim())) {
      this.notifierService.notify('error', 'Informe o email.');
      isError = true;
    }

    if (!this.password || this.password.trim() == '') {
      this.notifierService.notify('error', 'Informe a senha.');
      isError = true;
    }

    if (!isError) {
      let param = new LoginParamModel();
      param.email = this.username;
      param.senha = this.password;
      param.provider = 'LOCAL';
      this.spinner.show();
      this.loginService.login(param)
        .pipe(takeWhile(() => this.alive))
        .subscribe((data: any) => {
          this.spinner.hide();
          if (data && data.authenticated) {
            this.notifierService.notify('success', 'Login com sucesso');
            this.global.setLoggedUser(data);
            this.router.navigateByUrl('/');
          } else {
            this.notifierService.notify('error', 'Falha na autenticação! Verifique o usuário e a senha.');
          }
        });
    }
  }

  cleanData() {
    this.newUser = false;
    this.userimage = this.global.getDefaultUserImg();
    this.username = '';
    this.password = '';
    this.confirmPassword = '';
    this.fone = '';
    this.alive = true;
    this.social = false;
  }

  createUser() {
    this.cleanData();
    this.newUser = true;
  }

  cancel() {
    this.cleanData();
  }

  create() {
    let isError = false;
    if (!this.username || this.username.trim() == '' || !validateEmail(this.username.trim())) {
      this.notifierService.notify('error', 'Informe o email.');
      isError = true;
    }

    if (!this.password || this.password.trim() == '') {
      this.notifierService.notify('error', 'Informe a senha.');
      isError = true;
    }

    if (!this.confirmPassword || this.confirmPassword.trim() == '') {
      this.notifierService.notify('error', 'Informe a confirmação de senha.');
      isError = true;
    }

    if (!isError && this.password.trim() !== this.confirmPassword.trim()) {
      this.notifierService.notify('error', 'Confirmação de senha não confere.');
      isError = true;
    }

    if (!isError) {
      let user = new UsuarioPostParamModel();
      user.desImagem = isValidImage(this.userimage, this.global.getDefaultUserImg()) ? this.userimage : '';
      user.desEmail = this.username;
      user.desSenha = this.password;
      user.desTelefone = this.fone;
      this.spinner.show();
      this.loginService.create(user)
        .pipe(takeWhile(() => this.alive))
        .subscribe((data: any) => {
          this.spinner.hide();
          this.notifierService.notify('success', 'Usuário Criado.');
          this.cleanData();
        });
    }
  }

  processFile(imageInput: any) {
    const file: File = imageInput.files[0];
    const reader = new FileReader();

    reader.addEventListener('load', (event: any) => {
      this.userimage = event.target.result.replace('data:image/jpeg;base64,', '');
    });

    reader.readAsDataURL(file);
  }

  @HostListener('window:keydown', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent) {
    if (event.key == 'Enter') {
      if (this.newUser) {
        this.create();
      } else {
        this.login();
      }
    }
  }
}
