import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './providers/authguard.service'
import { InicialComponent } from './components/inicial/inicial.component';
import { LoginComponent } from './components/login/login.component';
import { UsuariosComponent } from './components/auth/usuarios/usuarios.component';

const routes: Routes = [
  { path: '', component: InicialComponent },
  { path: 'login', component: LoginComponent },
  { path: 'usuarios', component: UsuariosComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
