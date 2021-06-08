import { Component, TemplateRef, ViewChild } from '@angular/core';
import { UsuariosPagedResult, UsuarioModel, FiltroUsuarioModel } from '../../../../models/usuario.model';
import { PageModel } from '../../../../models/page.model';
import { UsuarioService } from '../../../../providers/usuario.service';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { NgxSpinnerService } from "ngx-spinner";
import { NotifierService } from 'angular-notifier';

@Component({
  selector: 'app-auth-menu-admin-listagem-usuarios',
  templateUrl: `listagem_usuarios.component.html`,
  styleUrls: ['listagem_usuarios.component.scss'],
  providers: [UsuarioService]
})
export class MenuAdminUsuariosComponent {
  @ViewChild('myTable') table: any;

  modalRef: BsModalRef = new BsModalRef();
  modalRefExclusao: BsModalRef = new BsModalRef();
  config: PerfectScrollbarConfigInterface = {};
  ColumnMode = ColumnMode;
  page = new PageModel();
  usuarios: Array<UsuarioModel> = new Array<UsuarioModel>();
  colunas: object[] = [{ name: 'id' }, { name: 'desNome' }, { name: 'desEmail' }, { name: 'desTelefone' }, { name: 'admin' }, { name: 'local' }];
  changeText: boolean = false;
  usuario: UsuarioModel = new UsuarioModel();
  idExclusao: string = '';
  filtros: FiltroUsuarioModel = new FiltroUsuarioModel();

  constructor(private usuarioService: UsuarioService,
    private modalService: BsModalService,
    private notifierService: NotifierService,
    private spinner: NgxSpinnerService) {
    this.page.pageNumber = 0;
    this.page.size = 7;
  }

  ngOnInit() {
    this.setPage({ offset: 0 });
    this.getAllPaged();
  }

  recalculate() {
    if (this.table) {
      this.table.recalculate();
    }
  }

  // List
  setPage(pageInfo: any) {
    this.page.pageNumber = pageInfo.offset;
    this.getAllPaged();
  }

  getAllPaged() {
    this.spinner.show();
    this.usuarioService.getAllPaged(this.page.pageNumber + 1, this.page.size, this.filtros.nameSearch, this.filtros.emailSearch, this.filtros.foneSearch, this.filtros.isAdminSearch)
      .subscribe((data: UsuariosPagedResult) => {
        this.spinner.hide();
        this.usuarios = data.results;
        this.page.totalElements = data.rowCount;
        this.page.totalPages = data.pageCount;
      });
  }

  cleanFilters() {
    this.filtros = new FiltroUsuarioModel();
  }

  search() {
    this.page.pageNumber = 0;
    this.getAllPaged();
  }

  // View
  openModalView(row: UsuarioModel, template: TemplateRef<any>) {
    this.usuario = row;
    this.modalRef = this.modalService.show(template, Object.assign({}, { class: 'modal-xl' }));
  }

  delete(id: string, template: TemplateRef<any>) {
    this.idExclusao = id;
    this.modalRefExclusao = this.modalService.show(template, Object.assign({}, { class: 'modal-md' }));
  }

  continuar() {
    this.spinner.show();
    this.usuarioService.delete(this.idExclusao)
      .subscribe((data: any) => {
        this.notifierService.notify('success', 'Problema excluído com sucesso');
        this.modalRefExclusao.hide();
        this.spinner.hide();
        this.getAllPaged();
      });
  }
}