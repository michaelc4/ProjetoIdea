import { Component, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { Global } from './providers/global.service';
import { LoginModel } from './models/login.model';
import { UsuarioModel, UsuarioPutParamModel } from './models/usuario.model';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { UsuarioService } from './providers/usuario.service';
import { NotifierService } from 'angular-notifier';
import { NgxSpinnerService } from "ngx-spinner";
import { isValidImage } from './functions/string.functions';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

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
  config: PerfectScrollbarConfigInterface = {};

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
      this.usuarioService.get(this.global.getLoggedUser().id)
        .subscribe((user: any) => {
          this.spinner.hide();
          this.senha = '';
          this.confSenha = '';
          this.usuario = user;
          if (!this.usuario.desImagem || this.usuario.desImagem.trim() == '') {
            this.usuario.desImagem = this.global.getDefaultUserImg();
          }
          this.modalRef = this.modalService.show(template, Object.assign({}, { class: 'modal-md' }));
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
    let isError = false;
    if (this.senha && this.senha.trim() != '' && this.senha.trim() !== this.confSenha.trim()) {
      this.notifierService.notify('error', 'Confirmação de senha não confere.');
      isError = true;
    }

    if (!isError) {
      let user = new UsuarioPutParamModel();
      user.id = this.usuario.id;
      user.desNome = this.usuario.desNome;
      user.desImagem = isValidImage(this.usuario.desImagem, this.global.getDefaultUserImg()) ? this.usuario.desImagem : '';
      user.desSenha = this.senha && this.senha.trim() != '' ? this.senha : '';
      user.desTelefone = this.usuario.desTelefone;
      user.desEspecialidade = this.usuario.desEspecialidade;
      user.desExperiencia = this.usuario.desExperiencia;
      this.spinner.show();
      this.usuarioService.put(user)
        .subscribe((data: any) => {
          this.spinner.hide();
          this.global.getLoggedUser().imagem = this.usuario.desImagem;
          this.modalRef.hide();
          this.notifierService.notify('success', 'Usuário Alterado.');
        });
    }
  }
}
