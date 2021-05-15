import { Component, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { Global } from './providers/global.service';
import { LoginModel } from './models/login.model';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  modalRef: BsModalRef = new BsModalRef();
  logged: boolean = false;
  baseImg: string = this.global.getDefaultUserImg();

  constructor(public router: Router,
    private modalService: BsModalService,
    private global: Global) { }

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
      this.modalRef = this.modalService.show(template);
    }
  }
}
