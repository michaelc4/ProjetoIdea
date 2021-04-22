import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Global } from './providers/global.service';
import { LoginModel } from './models/login.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  logged: boolean = false;
  baseImg: string = this.global.getDefaultUserImg();

  constructor(public router: Router,
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
}
