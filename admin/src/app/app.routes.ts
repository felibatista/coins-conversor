import { Routes } from '@angular/router';
import { HomeComponent } from '../pages/home/home.component';
import { LoginComponent } from '../pages/login/login.component';
import { StatusComponent } from '../pages/status/status.component';
import { loggedGuard } from '../guards/loggedGuard.guard';
import { notLoggedGuard } from '../guards/notLoggedGuard.guard';
import { UsersComponent } from '../pages/users/users.component';
import { ConversionsComponent } from '../pages/conversions/conversions.component';
import { CurrenciesComponent } from '../pages/currencies/currencies.component';

export const routes: Routes = [
  {
    path: 'admin/usuarios',
    component: UsersComponent,
    title: 'Usuarios | Panel de control',
    canActivate: [loggedGuard],
  },
  {
    path: 'admin/divisas',
    component: CurrenciesComponent,
    title: 'Divisas | Panel de control',
    canActivate: [loggedGuard],
  },
  {
    path: 'admin/conversiones',
    component: ConversionsComponent,
    title: 'Conversiones | Panel de control',
    canActivate: [loggedGuard],
  },
  {
    path: 'admin/sistema',
    component: StatusComponent,
    title: 'Estado | Panel de control',
    canActivate: [loggedGuard],
  },
  {
    path: 'admin',
    component: HomeComponent,
    title: 'Inicio | Panel de control',
    canActivate: [loggedGuard],
  },
  {
    path: 'login',
    component: LoginComponent,
    title: 'Iniciar sesión | Panel de control',
    canActivate: [notLoggedGuard],
  },
  { path: '**', redirectTo: 'admin' },
];
