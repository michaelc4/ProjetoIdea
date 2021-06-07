import { Component, TemplateRef } from '@angular/core';
import { IdeiasPagedResult, IdeiaModel, IdeiaAnexoModel } from '../../../../models/ideia.model';
import { ProblemasPagedResult, ProblemaModel, FiltroProblemaModel, ProblemaAnexoModel } from '../../../../models/problema.model';
import { VoluntarioPostParamModel } from '../../../../models/voluntario.model';
import { PageModel } from '../../../../models/page.model';
import { IdeiaService } from '../../../../providers/ideia.service';
import { ProblemaService } from '../../../../providers/problema.service';
import { VoluntarioService } from '../../../../providers/voluntario.service';
import { Global } from '../../../../providers/global.service';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { NgxSpinnerService } from "ngx-spinner";
import { NotifierService } from 'angular-notifier';
import { saveAs } from 'file-saver';
import { base64ToBlob } from '../../../../functions/string.functions';

@Component({
  selector: 'app-auth-menu-usuarios-listagem-projetos',
  templateUrl: `listagem_projetos.component.html`,
  styleUrls: ['listagem_projetos.component.scss'],
  providers: [IdeiaService, ProblemaService, VoluntarioService]
})
export class MenuUsuarioProjetosComponent {
  modalRefI: BsModalRef = new BsModalRef();
  modalRefP: BsModalRef = new BsModalRef();
  config: PerfectScrollbarConfigInterface = {};
  ColumnMode = ColumnMode;
  pageI = new PageModel();
  pageP = new PageModel();
  ideias: Array<IdeiaModel> = new Array<IdeiaModel>();
  problemas: Array<ProblemaModel> = new Array<ProblemaModel>();
  colunasI: object[] = [{ name: 'id' }, { name: 'desIdeia' }, { name: 'desMotivoInvestir' }, { name: 'indInteresseCompartilhar' }, { name: 'indNivelDesenvolvimento' }];
  colunasP: object[] = [{ name: 'id' }, { name: 'desProblema' }, { name: 'desSolucao' }, { name: 'indTipoBeneficio' }, { name: 'indTipoSolucao' }];
  changeText: boolean = false;
  ideia: IdeiaModel = new IdeiaModel();
  problema: ProblemaModel = new ProblemaModel();
  filtros: FiltroProblemaModel = new FiltroProblemaModel();
  filesI: Array<IdeiaAnexoModel> = new Array<IdeiaAnexoModel>();
  filesP: Array<ProblemaAnexoModel> = new Array<ProblemaAnexoModel>();

  constructor(private global: Global,
    private ideiaService: IdeiaService,
    private problemaService: ProblemaService,
    private voluntarioService: VoluntarioService,
    private modalService: BsModalService,
    private notifierService: NotifierService,
    private spinner: NgxSpinnerService) {
    this.pageI.pageNumber = 0;
    this.pageP.pageNumber = 0;
    this.pageI.size = 5;
    this.pageP.size = 5;
  }

  ngOnInit() {
    this.setPageI({ offset: 0 });
    this.setPageP({ offset: 0 });
    this.getAllPagedI();
    this.getAllPagedP();
  }

  // List
  setPageI(pageInfo: any) {
    this.pageI.pageNumber = pageInfo.offset;
    this.getAllPagedI();
  }

  setPageP(pageInfo: any) {
    this.pageP.pageNumber = pageInfo.offset;
    this.getAllPagedP();
  }

  getAllPagedI() {
    this.spinner.show();
    this.ideiaService.getAllPagedInitialScreen(this.pageI.pageNumber + 1, this.pageI.size, this.global.getLoggedUser().id, this.filtros.problemSearch, undefined, undefined, undefined, this.filtros.registrationDateIniSearch, this.filtros.registrationDateEndSearch)
      .subscribe((data: IdeiasPagedResult) => {
        this.spinner.hide();
        this.ideias = data.results;
        this.pageI.totalElements = data.rowCount;
        this.pageI.totalPages = data.pageCount;
      });
  }

  getAllPagedP() {
    this.spinner.show();
    this.problemaService.getAllPagedInitialScreen(this.pageP.pageNumber + 1, this.pageP.size, this.global.getLoggedUser().id, this.filtros.problemSearch, undefined, undefined, this.filtros.registrationDateIniSearch, this.filtros.registrationDateEndSearch)
      .subscribe((data: ProblemasPagedResult) => {
        this.spinner.hide();
        this.problemas = data.results;
        this.pageP.totalElements = data.rowCount;
        this.pageP.totalPages = data.pageCount;
      });
  }

  getTipoBeneficio(indTipo: string) {
    if (indTipo == "2") {
      return "Consumidor";
    } else if (indTipo == "3") {
      return "Sociedade";
    } else if (indTipo == "4") {
      return "Governo";
    } else {
      return "Empresa";
    }
  }

  getTipoSolucao(indTipo: string) {
    if (indTipo == "2") {
      return "É um problema que gostaria de ver resolvido";
    } else if (indTipo == "3") {
      return "É apenas algo que eu imaginei";
    } else {
      return "É um problema que encontro na minha empresa";
    }
  }

  getInteresseCompartilhar(indTipo: string) {
    if (indTipo == "2") {
      return "Quero divulgar uma solução para alguém que esteja disposto a trabalhar nela";
    } else if (indTipo == "3") {
      return "Foi apenas algo que pensei";
    } else {
      return "Quero encontrar alguém que me ajude a colocá-la em prática";
    }
  }

  getNivelDesenvolvimento(indTipo: string) {
    if (indTipo == "2") {
      return "Tenho um projeto para executar esta ideia";
    } else if (indTipo == "3") {
      return "Já tenho um produto desenvolvido";
    } else if (indTipo == "3") {
      return "Já tenho um pedido de patente";
    } else {
      return "É apenas uma ideia";
    }
  }

  getStringRezise(str: string) {
    if (str && str.length >= 100) {
      return str.substring(0, 100) + "...";
    }

    return str;
  }

  cleanFilters() {
    this.filtros = new FiltroProblemaModel();
  }

  search() {
    this.pageI.pageNumber = 0;
    this.pageP.pageNumber = 0;
    this.getAllPagedI();
    this.getAllPagedP();
  }

  // View
  openModalViewIdeia(row: IdeiaModel, template: TemplateRef<any>) {
    this.ideia = row;
    this.filesI = row.anexos;
    if (!this.filesI) {
      this.filesI = new Array<IdeiaAnexoModel>();
    }
    this.modalRefI = this.modalService.show(template, Object.assign({}, { class: 'modal-xl' }));
  }

  openModalViewProblema(row: ProblemaModel, template: TemplateRef<any>) {
    this.problema = row;
    this.filesP = row.anexos;
    if (!this.filesP) {
      this.filesP = new Array<ProblemaAnexoModel>();
    }
    this.modalRefP = this.modalService.show(template, Object.assign({}, { class: 'modal-xl' }));
  }

  apoiarIdeia(row: IdeiaModel) {
    if (row.voluntarioId) {
      this.spinner.show();
      this.voluntarioService.delete(row.voluntarioId)
        .subscribe((data: any) => {
          this.spinner.hide();
          this.getAllPagedI();
        });
    } else {
      this.spinner.show();
      let post = new VoluntarioPostParamModel();
      post.ideiaId = row.id;
      post.usuarioId = this.global.getLoggedUser().id;
      this.voluntarioService.post(post)
        .subscribe((data: any) => {
          this.spinner.hide();
          this.getAllPagedI();
        });
    }
  }

  apoiarProblema(row: ProblemaModel) {
    if (row.voluntarioId) {
      this.spinner.show();
      this.voluntarioService.delete(row.voluntarioId)
        .subscribe((data: any) => {
          this.spinner.hide();
          this.getAllPagedI();
        });
    } else {
      this.spinner.show();
      let post = new VoluntarioPostParamModel();
      post.problemaId = row.id;
      post.usuarioId = this.global.getLoggedUser().id;
      this.voluntarioService.post(post)
        .subscribe((data: any) => {
          this.spinner.hide();
          this.getAllPagedI();
        });
    }
  }

  // Anexos
  downloadFile(b64encodedString: string, fileName: string) {
    if (b64encodedString) {
      var blob = base64ToBlob(b64encodedString, 'text/plain');
      saveAs(blob, fileName);
    }
  }
}