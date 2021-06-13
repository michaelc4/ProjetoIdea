import { Component, ViewChild } from '@angular/core';
import { UsuarioService } from '../../../providers/usuario.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-auth-menu-usuarios',
  templateUrl: `menu_usuario.component.html`,
  styleUrls: ['menu_usuario.component.scss'],
  providers: [UsuarioService]
})
export class MenuUsuarioComponent {
  @ViewChild('staticTabs', { static: false }) staticTabs: any;
  @ViewChild('problemsList') problemsList: any;
  @ViewChild('ideaList') ideaList: any;
  @ViewChild('projectList') projectList: any;

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    let id = this.activatedRoute.snapshot.paramMap.get("id");
    if (id && id == '0') {
      setTimeout(() => { this.selectTab(0); }, 300)
    } else if (id && id == '1') {
      setTimeout(() => { this.selectTab(1); }, 300)
    } else if (id && id == '2') {
      setTimeout(() => { this.selectTab(2); }, 300)
    }
  }

  selectTab(tab: number) {
    this.staticTabs.tabs[tab].active = true;
    setTimeout(() => { this.openModal(tab); }, 600)
  }

  openModal(tab: number) {
    if (tab == 0) {
      this.problemsList.openModalNew();
    } else if (tab == 1) {
      this.ideaList.openModalNew();
    }
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