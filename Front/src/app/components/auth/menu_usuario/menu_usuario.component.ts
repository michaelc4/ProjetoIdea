import { Component } from '@angular/core';
import { UsuarioService } from '../../../providers/usuario.service';

@Component({
  selector: 'app-auth-menu-usuarios',
  templateUrl: `menu_usuario.component.html`,
  styleUrls: ['menu_usuario.component.scss'],
  providers: [UsuarioService]
})
export class MenuUsuarioComponent {

  constructor() { }

  ngOnInit() {

  }
}