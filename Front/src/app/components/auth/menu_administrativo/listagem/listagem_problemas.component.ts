import { Component, TemplateRef } from '@angular/core';
import { ProblemasPagedResult, ProblemaModel, ProblemaPostParamModel, ProblemaPutParamModel, FiltroProblemaModel, ProblemaAnexoModel, ProblemaAnexoAddModel } from '../../../../models/problema.model';
import { PageModel } from '../../../../models/page.model';
import { ProblemaService } from '../../../../providers/problema.service';
import { Global } from '../../../../providers/global.service';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { NgxSpinnerService } from "ngx-spinner";
import { NotifierService } from 'angular-notifier';
import { saveAs } from 'file-saver';
import { base64ToBlob, getBase64 } from '../../../../functions/string.functions';

@Component({
  selector: 'app-auth-menu-admin-listagem-problemas',
  templateUrl: `listagem_problemas.component.html`,
  styleUrls: ['listagem_problemas.component.scss'],
  providers: [ProblemaService]
})
export class MenuAdminProblemasComponent {
  modalRef: BsModalRef = new BsModalRef();
  modalRefExclusao: BsModalRef = new BsModalRef();
  config: PerfectScrollbarConfigInterface = {};
  ColumnMode = ColumnMode;
  page = new PageModel();
  problemas: Array<ProblemaModel> = new Array<ProblemaModel>();
  colunas: object[] = [{ name: 'id' }, { name: 'desProblema' }, { name: 'desSolucao' }, { name: 'indTipoBeneficio' }, { name: 'indTipoSolucao' }, { name: 'indAprovado' }];
  changeText: boolean = false;
  problema: ProblemaModel = new ProblemaModel();
  idExclusao: string = '';
  filtros: FiltroProblemaModel = new FiltroProblemaModel();
  uploadedFiles: Array<File> = [];
  files: Array<ProblemaAnexoModel> = new Array<ProblemaAnexoModel>();

  constructor(private global: Global,
    private problemaService: ProblemaService,
    private modalService: BsModalService,
    private notifierService: NotifierService,
    private spinner: NgxSpinnerService) {
    this.page.pageNumber = 0;
    this.page.size = 10;
    this.uploadedFiles = [];
  }

  ngOnInit() {
    this.setPage({ offset: 0 });
    this.getAllPaged();
  }

  // List
  setPage(pageInfo: any) {
    this.page.pageNumber = pageInfo.offset;
    this.getAllPaged();
  }

  getAllPaged() {
    this.spinner.show();
    this.problemaService.getAllPaged(this.page.pageNumber + 1, this.page.size, this.filtros.problemSearch, this.filtros.benefitTypeSearch, this.filtros.solutionTypeSearch, this.filtros.approvedSearch ? "1" : "", this.filtros.registrationDateIniSearch, this.filtros.registrationDateEndSearch)
      .subscribe((data: ProblemasPagedResult) => {
        this.spinner.hide();
        this.problemas = data.results;
        this.page.totalElements = data.rowCount;
        this.page.totalPages = data.pageCount;
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
    this.page.pageNumber = 0;
    this.getAllPaged();
  }

  // Add or Change
  openModalNew(template: TemplateRef<any>) {
    this.problema = new ProblemaModel();
    this.uploadedFiles = [];
    this.files = new Array<ProblemaAnexoModel>();
    this.modalRef = this.modalService.show(template, Object.assign({}, { class: 'modal-xl' }));
  }

  openModalChange(row: ProblemaModel, template: TemplateRef<any>) {
    this.problema = row;
    this.files = row.anexos;
    if (!this.files) {
      this.files = new Array<ProblemaAnexoModel>();
    }
    this.modalRef = this.modalService.show(template, Object.assign({}, { class: 'modal-xl' }));
  }

  delete(id: string, template: TemplateRef<any>) {
    this.idExclusao = id;
    this.modalRefExclusao = this.modalService.show(template, Object.assign({}, { class: 'modal-md' }));
  }

  continuar() {
    this.spinner.show();
    this.problemaService.delete(this.idExclusao)
      .subscribe((data: any) => {
        this.notifierService.notify('success', 'Problema excluído com sucesso');
        this.modalRefExclusao.hide();
        this.spinner.hide();
        this.getAllPaged();
      });
  }

  salvar() {
    let isError = false;
    if (!this.problema.desProblema || this.problema.desProblema.trim() == '') {
      this.notifierService.notify('error', 'Informe a descrição do problema.');
      isError = true;
    }

    if (!this.problema.indTipoBeneficio || this.problema.indTipoBeneficio.trim() == '' || this.problema.indTipoBeneficio.trim() == '0') {
      this.notifierService.notify('error', 'Informe a que público a resolução do problema beneficia.');
      isError = true;
    }

    if (!this.problema.indTipoSolucao || this.problema.indTipoSolucao.trim() == '' || this.problema.indTipoSolucao.trim() == '0') {
      this.notifierService.notify('error', 'Informe qual é o seu interesse na resolução deste problema.');
      isError = true;
    }

    if (!isError) {
      this.spinner.show();
      let arrAnexos = new Array<ProblemaAnexoAddModel>();
      if (this.files && this.files.length > 0) {
        for (let file of this.files) {
          let newFile = new ProblemaAnexoAddModel();
          newFile.desAnexo = file.desAnexo;
          newFile.desNomeOriginal = file.desNomeOriginal;
          newFile.indTipoArquivo = "1";
          newFile.problemaId = "00000000-0000-0000-0000-000000000000";
          arrAnexos.push(newFile);
        }
      }

      if (this.problema.id && this.problema.id.trim() != '') {
        let problemaPut = new ProblemaPutParamModel();
        problemaPut.id = this.problema.id;
        problemaPut.desProblema = this.problema.desProblema;
        problemaPut.desSolucao = this.problema.desSolucao;
        problemaPut.indTipoBeneficio = this.problema.indTipoBeneficio;
        problemaPut.indTipoSolucao = this.problema.indTipoSolucao;
        problemaPut.numProbabilidadeInvestir = this.problema.numProbabilidadeInvestir;
        problemaPut.usuarioId = this.global.getLoggedUser().id;
        problemaPut.anexos = arrAnexos;

        this.problemaService.put(problemaPut)
          .subscribe((data: any) => {
            this.spinner.hide();
            this.notifierService.notify('success', 'Problema alterado com sucesso');
            this.modalRef.hide();
            this.getAllPaged();
          });

      } else {
        let problemaPost = new ProblemaPostParamModel();
        problemaPost.desProblema = this.problema.desProblema;
        problemaPost.desSolucao = this.problema.desSolucao;
        problemaPost.indTipoBeneficio = this.problema.indTipoBeneficio;
        problemaPost.indTipoSolucao = this.problema.indTipoSolucao;
        problemaPost.numProbabilidadeInvestir = this.problema.numProbabilidadeInvestir;
        problemaPost.usuarioId = this.global.getLoggedUser().id;
        problemaPost.anexos = arrAnexos;

        this.problemaService.post(problemaPost)
          .subscribe((data: any) => {
            this.spinner.hide();
            this.notifierService.notify('success', 'Problema cadastrado com sucesso');
            this.modalRef.hide();
            this.getAllPaged();
          });
      }
    }
  }

  // Anexos
  importarArquivos() {
    for (let i = this.uploadedFiles.length - 1; i >= 0; i--) {
      getBase64(this.uploadedFiles[i]).then((encoded: any) => {
        if (!this.files.some(e => e.desNomeOriginal === this.uploadedFiles[i].name) && this.files.length < 5) {
          let anexo = new ProblemaAnexoModel();
          anexo.desAnexo = encoded;
          anexo.indTipoArquivo = '1';
          anexo.desNomeOriginal = this.uploadedFiles[i].name;
          this.files.push(anexo);
        }
        this.uploadedFiles = this.uploadedFiles.filter(obj => obj.name !== this.uploadedFiles[i].name);
      });
    }
  }

  downloadFile(b64encodedString: string, fileName: string) {
    if (b64encodedString) {
      var blob = base64ToBlob(b64encodedString, 'text/plain');
      saveAs(blob, fileName);
    }
  }

  removeFile(fileName: string) {
    this.files = this.files.filter(obj => obj.desNomeOriginal !== fileName);
  }
}