import { Injectable } from "@angular/core";
import { Router, CanActivate } from '@angular/router';
import { Global } from "../providers/global.service";
import { LoginModel } from '../models/login.model';

@Injectable()
export class AuthGuardAdmin implements CanActivate {

  constructor(public router: Router,
    public global: Global) { }

  canActivate(): boolean {
    const token = this.global.getLoggedUser().accessToken;
    const expiration = this.global.getLoggedUser().expiration;
    const admin = this.global.getLoggedUser().admin;
    const expirationDate = new Date(expiration);
    const now = new Date();
    if (!token || token.trim() === '' || now > expirationDate || !admin) {
      this.router.navigate(['login']);
      this.global.setLoggedUser(new LoginModel());
      return false;
    }
    return true;
  }
}
