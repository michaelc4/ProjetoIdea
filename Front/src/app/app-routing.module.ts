import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './providers/authguard.service';
import { AuthGuardAdmin } from './providers/authguardadmin.service';
import { InicialComponent } from './components/inicial/inicial.component';
import { LoginComponent } from './components/login/login.component';
import { MenuUsuarioComponent } from './components/auth/menu_usuario/menu_usuario.component';
import { MenuAdministrativoComponent } from './components/auth/menu_administrativo/menu_administrativo.component';

const routes: Routes = [
  { path: '', component: InicialComponent },
  { path: 'login', component: LoginComponent },
  { path: 'menu-usuario', component: MenuUsuarioComponent, canActivate: [AuthGuard] },
  { path: 'menu-usuario/:id', component: MenuUsuarioComponent, canActivate: [AuthGuard] },
  { path: 'menu-administrativo', component: MenuAdministrativoComponent, canActivate: [AuthGuardAdmin] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
