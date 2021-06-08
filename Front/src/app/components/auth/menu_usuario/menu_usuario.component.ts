import { Component, ViewChild } from '@angular/core';
import { UsuarioService } from '../../../providers/usuario.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-menu-usuarios',
  templateUrl: `menu_usuario.component.html`,
  styleUrls: ['menu_usuario.component.scss'],
  providers: [UsuarioService]
})
export class MenuUsuarioComponent {
  @ViewChild('problemsList') problemsList: any;
  @ViewChild('ideaList') ideaList: any;
  @ViewChild('projectList') projectList: any;

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
    } else if (tabNumber == 3 && this.projectList) {
      setTimeout(() => { this.projectList.recalculate(); }, 300)
    }
  }
}