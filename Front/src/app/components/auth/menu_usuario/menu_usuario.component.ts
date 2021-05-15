import { Component } from '@angular/core';
import { UsuariosPagedResult, UsuarioModel } from '../../../models/usuario.model';
import { PageModel } from '../../../models/page.model';
import { UsuarioService } from '../../../providers/usuario.service';
import { Global } from '../../../providers/global.service';
import { ColumnMode } from '@swimlane/ngx-datatable';

@Component({
  selector: 'app-auth-menu-usuarios',
  templateUrl: `menu_usuario.component.html`,
  styleUrls: ['menu_usuario.component.scss'],
  providers: [UsuarioService]
})
export class MenuUsuarioComponent {
  ColumnMode = ColumnMode;
  page = new PageModel();
  usuarios: Array<UsuarioModel> = new Array<UsuarioModel>();

  constructor(private global: Global,
    private usuarioService: UsuarioService) {
    this.page.pageNumber = 0;
    this.page.size = 10;
  }

  ngOnInit() {
    this.setPage({ offset: 0 });
    this.usuarioService.getAllPaged().subscribe((data: UsuariosPagedResult) => {
      this.usuarios = data.results;
      this.page.totalElements = data.rowCount;
      this.page.totalPages = data.pageCount;
    });
  }

  setPage(pageInfo: any) {
    //this.page.pageNumber = pageInfo.offset;
    // this.serverResultsService.getResults(this.page).subscribe(pagedData => {
    //   this.page = pagedData.page;
    //  this.rows = pagedData.data;
    //});
  }

  onSelect(row: any) {

  }

  onSelectBlue(row: any) {

  }
}