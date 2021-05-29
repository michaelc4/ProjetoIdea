import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { SocialLoginModule, SocialAuthServiceConfig } from 'angularx-social-login';
import { GoogleLoginProvider, FacebookLoginProvider } from 'angularx-social-login';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { RatingModule } from 'ngx-bootstrap/rating';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from "@angular/common";
import { FormsModule } from '@angular/forms';
import { NotifierModule, NotifierOptions } from 'angular-notifier';
import { NgxSpinnerModule } from "ngx-spinner";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { PerfectScrollbarConfigInterface, PerfectScrollbarModule, PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { FileUploadModule } from '@iplab/ngx-file-upload';

// Services
import { Global } from "./providers/global.service";
import { AuthGuard } from "./providers/authguard.service";

//Components
import { AppComponent } from './app.component';
import { InicialComponent } from "../app/components/inicial/inicial.component";
import { LoginComponent } from "../app/components/login/login.component";
import { MenuUsuarioComponent } from "../app/components/auth/menu_usuario/menu_usuario.component";
import { MenuUsuarioProblemasComponent } from "../app/components/auth/menu_usuario/listagem/listagem_problemas.component";
import { MenuUsuarioIdeiasComponent } from "../app/components/auth/menu_usuario/listagem/listagem_ideias.component";

const customNotifierOptions: NotifierOptions = {
  position: {
    horizontal: {
      position: 'right',
      distance: 12,
    },
    vertical: {
      position: 'top',
      distance: 12,
      gap: 10,
    },
  },
  theme: 'material',
  behaviour: {
    onClick: false,
    onMouseover: 'pauseAutoHide',
    showDismissButton: true,
    stacking: 4,
  },
  animations: {
    enabled: true,
    show: {
      preset: 'slide',
      speed: 300,
      easing: 'ease',
    },
    hide: {
      preset: 'fade',
      speed: 300,
      easing: 'ease',
      offset: 50,
    },
    shift: {
      speed: 300,
      easing: 'ease',
    },
    overlap: 150,
  },
};

const maskConfig: Partial<IConfig> = {
  validation: false,
};

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true,
  suppressScrollY: false
};

@NgModule({
  declarations: [
    AppComponent,
    InicialComponent,
    LoginComponent,
    MenuUsuarioComponent,
    MenuUsuarioProblemasComponent,
    MenuUsuarioIdeiasComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    CommonModule,
    FormsModule,
    NgxMaskModule.forRoot(maskConfig),
    NotifierModule.withConfig(customNotifierOptions),
    NgxSpinnerModule,
    AppRoutingModule,
    SocialLoginModule,
    HttpClientModule,
    PerfectScrollbarModule,
    FileUploadModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    TabsModule.forRoot(),
    RatingModule.forRoot(),
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
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
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
