import { Component } from '@angular/core';
import { UsuarioService } from '../../../providers/usuario.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-menu-usuarios',
  templateUrl: `menu_usuario.component.html`,
  styleUrls: ['menu_usuario.component.scss'],
  providers: [UsuarioService]
})
export class MenuUsuarioComponent {

  constructor(private router: Router) { }

  ngOnInit() {

  }

  homeLink() {
    this.router.navigateByUrl('/');
  }
}