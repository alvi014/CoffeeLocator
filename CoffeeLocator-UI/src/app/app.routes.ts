import { Routes } from '@angular/router';
import { CoffeeMapComponent } from './features/coffee-map/coffee-map.component';
import { authGuard } from './core/guards/auth-guard';
import { HomeComponent } from './features/home/home.component';
import { RegisterComponent } from './features/register/register.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: '', redirectTo: 'map', pathMatch: 'full' },
  {
    path: 'map',
    component: CoffeeMapComponent,
    canActivate: [authGuard],
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./features/auth/login/login.component').then((m) => m.LoginComponent),
  },
  { path: 'register', component: RegisterComponent }
];
