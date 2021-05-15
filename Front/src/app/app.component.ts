import { Component, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { Global } from './providers/global.service';
import { LoginModel } from './models/login.model';
import { UsuarioModel } from './models/usuario.model';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { UsuarioService } from './providers/usuario.service';
import { NotifierService } from 'angular-notifier';
import { NgxSpinnerService } from "ngx-spinner";
import { validateEmail, isValidImage } from './functions/string.functions'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [UsuarioService]
})
export class AppComponent {
  modalRef: BsModalRef = new BsModalRef();
  logged: boolean = false;
  baseImg: string = this.global.getDefaultUserImg();
  usuario: UsuarioModel = new UsuarioModel();
  senha: string = '';
  confSenha: string = '';

  constructor(public router: Router,
    private modalService: BsModalService,
    private global: Global,
    private usuarioService: UsuarioService,
    private notifierService: NotifierService,
    private spinner: NgxSpinnerService) { }

  ngDoCheck() {
    if (this.global.getLoggedUser() && this.global.getLoggedUser().accessToken.trim() != '') {
      this.logged = true;
    } else {
      this.logged = false;
    }

    if (this.global.getLoggedUser() && this.global.getLoggedUser().imagem && this.global.getLoggedUser().imagem.trim() != '') {
      this.baseImg = this.global.getLoggedUser().imagem;
    } else {
      this.baseImg = this.global.getDefaultUserImg();
    }
  }

  login() {
    this.router.navigateByUrl('/login');
  }

  logout() {
    this.global.setLoggedUser(new LoginModel());
    this.router.navigateByUrl('/');
  }

  openModal(template: TemplateRef<any>) {
    if (this.logged) {
      this.spinner.show();
      this.usuarioService
        .get(this.global.getLoggedUser().id)
        .subscribe((user: any) => {
          this.spinner.hide();
          this.usuario = user;
          this.modalRef = this.modalService.show(template);
        });
    }
  }

  processFile(imageInput: any) {
    const file: File = imageInput.files[0];
    const reader = new FileReader();

    reader.addEventListener('load', (event: any) => {
      this.usuario.desImagem = event.target.result.replace('data:image/jpeg;base64,', '');
    });

    reader.readAsDataURL(file);
  }

  saveUser() {
    //let isError = false;
    //if (!this.username || this.username.trim() == '' || !validateEmail(this.username.trim())) {
    //  this.notifierService.notify('error', 'Informe o email.');
    //  isError = true;
    //}

    //if (!this.password || this.password.trim() == '') {
    //  this.notifierService.notify('error', 'Informe a senha.');
    //  isError = true;
    //}

    //if (!this.confirmPassword || this.confirmPassword.trim() == '') {
    //  this.notifierService.notify('error', 'Informe a confirmação de senha.');
    //  isError = true;
    //}

    //if (!isError && this.password.trim() !== this.confirmPassword.trim()) {
    //  this.notifierService.notify('error', 'Confirmação de senha não confere.');
    //  isError = true;
    //}

    //if (!isError) {
    //  let user = new UsuarioPostParamModel();
    //  user.desImagem = isValidImage(this.userimage, this.global.getDefaultUserImg()) ? this.userimage : '';
    //  user.desEmail = this.username;
    //  user.desSenha = this.password;
    //  user.desTelefone = this.fone;
    //  this.spinner.show();
    //  this.loginService.create(user)
    //    .pipe(takeWhile(() => this.alive))
    //    .subscribe((data: any) => {
    //      this.spinner.hide();
    //      this.notifierService.notify('success', 'Usuário Criado.');
    //      this.cleanData();
    //    });
    //}
  }
}
