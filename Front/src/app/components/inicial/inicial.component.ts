import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IdeiasPagedResult, IdeiaModel } from '../../models/ideia.model';
import { ProblemasPagedResult, ProblemaModel } from '../../models/problema.model';
import { IdeiaService } from '../../providers/ideia.service';
import { ProblemaService } from '../../providers/problema.service';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-inicial',
  templateUrl: './inicial.component.html',
  styleUrls: ['./inicial.component.scss'],
  providers: [IdeiaService, ProblemaService]
})
export class InicialComponent {
  ideias: Array<IdeiaModel> = new Array<IdeiaModel>();
  problemas: Array<ProblemaModel> = new Array<ProblemaModel>();
  ideiaPaging: number = 1;
  ideiaPagingMax: number = 1;
  problemaPaging: number = 1;
  problemaPagingMax: number = 1;
  numItens: number = 10;

  constructor(private router: Router,
    private ideiaService: IdeiaService,
    private problemaService: ProblemaService,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.ideiaPaging = 1;
    this.problemaPaging = 1;
    this.ideias = new Array<IdeiaModel>();
    this.problemas = new Array<ProblemaModel>();
    this.getAllPagedI(false);
    this.getAllPagedP(false);
  }

  menuUsuario(param: number) {
    this.router.navigateByUrl('/menu-usuario/' + param);
  }

  menuAdministrativo() {
    this.router.navigateByUrl('/menu-administrativo');
  }

  getStringRezise(str: string) {
    if (str && str.length >= 120) {
      return str.substring(0, 120) + "...";
    }

    return str;
  }

  onProblemaScroll() {
    if (this.problemaPaging != 1 && this.problemaPaging < this.problemaPagingMax + 1) {
      this.getAllPagedP(true);
    }
  }

  getAllPagedI(enableSpinner: boolean) {
    if (enableSpinner)
      this.spinner.show();

    this.ideiaService.getAllPagedInitialScreen(this.ideiaPaging, this.numItens, undefined, undefined, undefined, undefined, undefined, undefined, undefined)
      .subscribe((data: IdeiasPagedResult) => {
        let itens = data.results;
        if (itens && itens.length > 0) {
          for (let item of itens) {
            this.ideias.push(item);
          }
        }

        this.ideiaPagingMax = data.pageCount;
        if (enableSpinner)
          this.spinner.hide();
      });
  }

  getAllPagedP(enableSpinner: boolean) {
    if (enableSpinner)
      this.spinner.show();

    this.problemaService.getAllPagedInitialScreen(this.problemaPaging, this.numItens, undefined, undefined, undefined, undefined, undefined, undefined)
      .subscribe((data: ProblemasPagedResult) => {
        let itens = data.results;
        if (itens && itens.length > 0) {
          for (let item of itens) {
            this.problemas.push(item);
          }
        }

        this.problemaPagingMax = data.pageCount;
        if (enableSpinner)
          this.spinner.hide();
        this.problemaPaging += 1;
      });
  }
}
