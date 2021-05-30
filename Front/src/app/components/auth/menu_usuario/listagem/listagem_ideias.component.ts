import { Component, TemplateRef } from '@angular/core';
import { IdeiasPagedResult, IdeiaModel, IdeiaPostParamModel, IdeiaPutParamModel, FiltroIdeiaModel, IdeiaAnexoModel, IdeiaAnexoAddModel } from '../../../../models/ideia.model';
import { PageModel } from '../../../../models/page.model';
import { IdeiaService } from '../../../../providers/ideia.service';
import { Global } from '../../../../providers/global.service';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { NgxSpinnerService } from "ngx-spinner";
import { NotifierService } from 'angular-notifier';
import { saveAs } from 'file-saver';
import { base64ToBlob } from '../../../../functions/string.functions';

@Component({
  selector: 'app-auth-menu-usuarios-listagem-ideias',
  templateUrl: `listagem_ideias.component.html`,
  styleUrls: ['listagem_ideias.component.scss'],
  providers: [IdeiaService]
})
export class MenuUsuarioIdeiasComponent {
  modalRef: BsModalRef = new BsModalRef();
  modalRefExclusao: BsModalRef = new BsModalRef();
  config: PerfectScrollbarConfigInterface = {};
  ColumnMode = ColumnMode;
  page = new PageModel();
  ideias: Array<IdeiaModel> = new Array<IdeiaModel>();
  colunas: object[] = [{ name: 'id' }, { name: 'desIdeia' }, { name: 'desMotivoInvestir' }, { name: 'indInteresseCompartilhar' }, { name: 'indNivelDesenvolvimento' }, { name: 'indNivelSigilo' }, { name: 'indAprovado' }];
  changeText: boolean = false;
  ideia: IdeiaModel = new IdeiaModel();
  idExclusao: string = '';
  filtros: FiltroIdeiaModel = new FiltroIdeiaModel();
  uploadedFiles: Array<File> = [];
  files: Array<IdeiaAnexoModel> = new Array<IdeiaAnexoModel>();

  constructor(private global: Global,
    private ideiaService: IdeiaService,
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
    this.ideiaService.getAllPagedByUser(this.page.pageNumber + 1, this.page.size, this.global.getLoggedUser().id, this.filtros.ideaSearch, this.filtros.reasonSearch, this.filtros.shareSearch, this.filtros.developmentSearch, this.filtros.secretSearch, this.filtros.approvedSearch ? "1" : "", this.filtros.registrationDateIniSearch, this.filtros.registrationDateEndSearch)
      .subscribe((data: IdeiasPagedResult) => {
        this.spinner.hide();
        this.ideias = data.results;
        this.page.totalElements = data.rowCount;
        this.page.totalPages = data.pageCount;
      });
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
    this.filtros = new FiltroIdeiaModel();
  }

  // Add or Change
  openModalNew(template: TemplateRef<any>) {
    this.ideia = new IdeiaModel();
    this.modalRef = this.modalService.show(template, Object.assign({}, { class: 'modal-lg' }));
  }

  openModalChange(row: IdeiaModel, template: TemplateRef<any>) {
    this.ideia = row;
    this.files = row.anexos;
    if (!this.files) {
      this.files = new Array<IdeiaAnexoModel>();
    }
    this.modalRef = this.modalService.show(template, Object.assign({}, { class: 'modal-lg' }));
  }

  delete(id: string, template: TemplateRef<any>) {
    this.idExclusao = id;
    this.modalRefExclusao = this.modalService.show(template, Object.assign({}, { class: 'modal-md' }));
  }

  continuar() {
    this.spinner.show();
    this.ideiaService.delete(this.idExclusao)
      .subscribe((data: any) => {
        this.notifierService.notify('success', 'Idéia excluída com sucesso');
        this.modalRefExclusao.hide();
        this.spinner.hide();
        this.getAllPaged();
      });
  }

  salvar() {
    let isError = false;
    if (!this.ideia.desIdeia || this.ideia.desIdeia.trim() == '') {
      this.notifierService.notify('error', 'Informe a descrição da idéia.');
      isError = true;
    }

    if (!this.ideia.indInteresseCompartilhar || this.ideia.indInteresseCompartilhar.trim() == '' || this.ideia.indInteresseCompartilhar.trim() == '0') {
      this.notifierService.notify('error', 'Informe qual o interesse em compratilhar.');
      isError = true;
    }

    if (!this.ideia.indNivelDesenvolvimento || this.ideia.indNivelDesenvolvimento.trim() == '' || this.ideia.indNivelDesenvolvimento.trim() == '0') {
      this.notifierService.notify('error', 'Informe o nível de desenvolvimento da idéia.');
      isError = true;
    }

    if (!isError) {
      this.spinner.show();
      let arrAnexos = new Array<IdeiaAnexoAddModel>();
      if (this.files && this.files.length > 0) {
        for (let file of this.files) {
          let newFile = new IdeiaAnexoAddModel();
          newFile.desAnexo = file.desAnexo;
          newFile.desNomeOriginal = file.desNomeOriginal;
          newFile.indTipoArquivo = "1";
          newFile.ideiaId = "00000000-0000-0000-0000-000000000000";
          arrAnexos.push(newFile);
        }
      }

      if (this.ideia.id && this.ideia.id.trim() != '') {
        let ideiaPut = new IdeiaPutParamModel();
        ideiaPut.id = this.ideia.id;
        ideiaPut.desIdeia = this.ideia.desIdeia;
        ideiaPut.desMotivoInvestir = this.ideia.desMotivoInvestir;
        ideiaPut.indInteresseCompartilhar = this.ideia.indInteresseCompartilhar;
        ideiaPut.indNivelDesenvolvimento = this.ideia.indNivelDesenvolvimento;
        ideiaPut.indNivelSigilo = this.ideia.indNivelSigilo;
        ideiaPut.desComentario = this.ideia.desComentario;
        ideiaPut.usuarioId = this.global.getLoggedUser().id;
        ideiaPut.anexos = arrAnexos;

        this.ideiaService.put(ideiaPut)
          .subscribe((data: any) => {
            this.spinner.hide();
            this.notifierService.notify('success', 'Idéia alterada com sucesso');
            this.modalRef.hide();
            this.getAllPaged();
          });

      } else {
        let ideiaPost = new IdeiaPostParamModel();
        ideiaPost.desIdeia = this.ideia.desIdeia;
        ideiaPost.desMotivoInvestir = this.ideia.desMotivoInvestir;
        ideiaPost.indInteresseCompartilhar = this.ideia.indInteresseCompartilhar;
        ideiaPost.indNivelDesenvolvimento = this.ideia.indNivelDesenvolvimento;
        ideiaPost.indNivelSigilo = this.ideia.indNivelSigilo;
        ideiaPost.desComentario = this.ideia.desComentario;
        ideiaPost.usuarioId = this.global.getLoggedUser().id;
        ideiaPost.anexos = arrAnexos;

        this.ideiaService.post(ideiaPost)
          .subscribe((data: any) => {
            this.spinner.hide();
            this.notifierService.notify('success', 'Idéia cadastrada com sucesso');
            this.modalRef.hide();
            this.getAllPaged();
          });
      }
    }
  }

  // Anexos
  importarArquivos() {
    for (let i = 0; i < this.uploadedFiles.length; i++) {
      this.getBase64(this.uploadedFiles[i]).then(() => { });
    }
    this.uploadedFiles = [];
  }

  getBase64(file: File) {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        let encoded = reader.result != null ? reader.result.toString().replace(/^data:(.*,)?/, '') : '';
        if ((encoded.length % 4) > 0) {
          encoded += '='.repeat(4 - (encoded.length % 4));
        }

        if (!this.files.some(e => e.desNomeOriginal === file.name)) {
          let anexo = new IdeiaAnexoModel();
          anexo.desAnexo = encoded;
          anexo.indTipoArquivo = '1';
          anexo.desNomeOriginal = file.name;
          this.files.push(anexo);
        }
        resolve(encoded);
      };
      reader.onerror = error => reject(error);
    });
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