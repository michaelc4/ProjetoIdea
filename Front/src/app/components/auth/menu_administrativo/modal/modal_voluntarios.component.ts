import { Component, AfterViewInit, ViewChild } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { NgxSpinnerService } from "ngx-spinner";
import { VoluntarioService } from '../../../../providers/voluntario.service';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { PageModel } from '../../../../models/page.model';
import { VoluntariosPagedResult } from '../../../../models/voluntario.model';
import { UsuarioModel } from '../../../../models/usuario.model';

@Component({
  selector: 'app-auth-modal-voluntarios',
  templateUrl: `modal_voluntarios.component.html`,
  styleUrls: ['modal_voluntarios.component.scss'],
  providers: [VoluntarioService]
})
export class ModalVoluntariosComponent implements AfterViewInit {
  @ViewChild('myTable') table: any;

  config: PerfectScrollbarConfigInterface = {};
  ColumnMode = ColumnMode;
  page = new PageModel();
  voluntarios: Array<UsuarioModel> = new Array<UsuarioModel>();
  colunas: object[] = [{ name: 'id' }, { name: 'desNome' }, { name: 'desEmail' }, { name: 'desTelefone' }, { name: 'admin' }, { name: 'local' }];
  expanded: any = {};
  public uuid: string = '';
  public type: string = '';

  constructor(public bsModalRef: BsModalRef,
    private spinner: NgxSpinnerService,
    private voluntarioService: VoluntarioService) { }

  ngOnInit() {
    this.page.pageNumber = 0;
    this.page.size = 10;
  }

  ngAfterViewInit() {
    this.spinner.show();
    setTimeout(() => { this.loadVoluntarios() }, 1500)
  }

  loadVoluntarios() {
    this.uuid = this.bsModalRef.content?.uuid;
    this.type = this.bsModalRef.content?.type;
    this.spinner.hide();
    this.setPage({ offset: 0 });
    this.getAllPaged();
  }

  toggleExpandRow(row: any) {
    this.table.rowDetail.toggleExpandRow(row);
  }

  onDetailToggle(event: any) {

  }

  // List
  setPage(pageInfo: any) {
    this.page.pageNumber = pageInfo.offset;
    this.getAllPaged();
  }

  getAllPaged() {
    this.spinner.show();
    if (this.type == 'Problema') {
      this.voluntarioService.getAllPagedByProblemOrIdeia(this.page.pageNumber + 1, this.page.size, this.uuid, undefined)
        .subscribe((data: VoluntariosPagedResult) => {
          this.spinner.hide();
          this.voluntarios = new Array<UsuarioModel>();
          if (data.results && data.results.length > 0) {
            for (let vol of data.results) {
              this.voluntarios.push(vol.usuario);
            }
          }
          this.page.totalElements = data.rowCount;
          this.page.totalPages = data.pageCount;
        });
    } else if (this.type == 'Idéia') {
      this.voluntarioService.getAllPagedByProblemOrIdeia(this.page.pageNumber + 1, this.page.size, undefined, this.uuid)
        .subscribe((data: VoluntariosPagedResult) => {
          this.spinner.hide();
          this.voluntarios = new Array<UsuarioModel>();
          if (data.results && data.results.length > 0) {
            for (let vol of data.results) {
              this.voluntarios.push(vol.usuario);
            }
          }
          this.page.totalElements = data.rowCount;
          this.page.totalPages = data.pageCount;
        });
    }
  }
}