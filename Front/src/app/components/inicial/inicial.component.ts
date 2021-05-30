import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inicial',
  templateUrl: './inicial.component.html',
  styleUrls: ['./inicial.component.scss'],
  providers: []
})
export class InicialComponent {

  constructor(private router: Router) { }

  menuUsuario() {
    this.router.navigateByUrl('/menu-usuario');
  }

  menuAdministrativo() {
    this.router.navigateByUrl('/menu-administrativo');
  }
}
