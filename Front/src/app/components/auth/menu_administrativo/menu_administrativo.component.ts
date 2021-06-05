import { Component } from '@angular/core';
import { UsuarioService } from '../../../providers/usuario.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-menu-administrativo',
  templateUrl: `menu_administrativo.component.html`,
  styleUrls: ['menu_administrativo.component.scss'],
  providers: [UsuarioService]
})
export class MenuAdministrativoComponent {

  constructor(private router: Router) { }

  ngOnInit() {

  }

  homeLink() {
    this.router.navigateByUrl('/');
  }
}