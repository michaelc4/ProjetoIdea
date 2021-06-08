import { Component, ViewChild } from '@angular/core';
import { UsuarioService } from '../../../providers/usuario.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-menu-administrativo',
  templateUrl: `menu_administrativo.component.html`,
  styleUrls: ['menu_administrativo.component.scss'],
  providers: [UsuarioService]
})
export class MenuAdministrativoComponent {
  @ViewChild('problemsList') problemsList: any;
  @ViewChild('ideaList') ideaList: any;
  @ViewChild('userList') userList: any;

  constructor(private router: Router) { }

  ngOnInit() {

  }

  homeLink() {
    this.router.navigateByUrl('/');
  }

  selectedTab(tabNumber: number) {
    if (tabNumber == 1 && this.problemsList) {
      setTimeout(() => { this.problemsList.recalculate(); }, 300)
    } else if (tabNumber == 2 && this.ideaList) {
      setTimeout(() => { this.ideaList.recalculate(); }, 300)
    } else if (tabNumber == 3 && this.userList) {
      setTimeout(() => { this.userList.recalculate(); }, 300)
    }
  }
}