import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { SocialLoginModule, SocialAuthServiceConfig } from 'angularx-social-login';
import { GoogleLoginProvider, FacebookLoginProvider } from 'angularx-social-login';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination'
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from "@angular/common";
import { FormsModule } from '@angular/forms';
import { NotifierModule } from 'angular-notifier';

// Services
import { Global } from "./providers/global.service";
import { AuthGuard } from "./providers/authguard.service";

//Components
import { AppComponent } from './app.component';
import { LoginComponent } from "../app/components/login/login.component";
import { UsuariosComponent } from "../app/components/auth/usuarios/usuarios.component";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UsuariosComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    NotifierModule,
    AppRoutingModule,
    SocialLoginModule,
    HttpClientModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    NgxDatatableModule.forRoot({
      messages: {
        emptyMessage: 'Nenhum dado encontrado',
        totalMessage: 'total',
        selectedMessage: 'selecionado'
      }
    })
  ],
  providers: [
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider('740118931275-2dqkv6vacunok9bj7a17gvenc4po0us0.apps.googleusercontent.com')
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider('444951796825941')
          }
        ]
      } as SocialAuthServiceConfig,
    },
    Global,
    AuthGuard
  ],
  bootstrap: [
    AppComponent
  ],
  exports: [
    BsDropdownModule,
    TooltipModule,
    ModalModule
  ]
})
export class AppModule { }
