import { Component } from '@angular/core';
import { UsuarioService } from '../../../providers/usuario.service';

@Component({
  selector: 'app-auth-menu-administrativo',
  templateUrl: `menu_administrativo.component.html`,
  styleUrls: ['menu_administrativo.component.scss'],
  providers: [UsuarioService]
})
export class MenuAdministrativoComponent {

  constructor() { }

  ngOnInit() {

  }
}