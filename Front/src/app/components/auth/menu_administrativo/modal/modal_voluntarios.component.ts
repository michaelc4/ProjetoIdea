import { Component, AfterViewInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

@Component({
  selector: 'app-auth-modal-voluntarios',
  templateUrl: `modal_voluntarios.component.html`,
  styleUrls: ['modal_voluntarios.component.scss'],
  providers: []
})
export class ModalVoluntariosComponent implements AfterViewInit {
  config: PerfectScrollbarConfigInterface = {};
  public uuid: string = '';
  public type: string = '';

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {

  }

  ngAfterViewInit() {

  }

  loadVoluntarios() {
    console.log(this.bsModalRef.content);
  }
}