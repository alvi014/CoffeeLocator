import { Routes } from '@angular/router';
import { CoffeeMapComponent } from './features/coffee-map/coffee-map.component';
import { authGuard } from './core/guards/auth/auth-guard';
import { HomeComponent } from './features/home/home.component';
import { RegisterComponent } from './features/register/register.component';
import { adminGuard } from './core/guards/admin/admin-guard';
import { AdminDashboardComponent } from './features/admin/admin-dashboard/admin-dashboard.component';



export const routes: Routes = [
  { path: '', component: HomeComponent }, 
  {
    path: 'map', 
    component: CoffeeMapComponent,
    canActivate: [authGuard],
  },
  {
    path: 'login', 
    loadComponent: () => import('./features/auth/login/login.component').then((m) => m.LoginComponent),
  },
  { path: 'register', component: RegisterComponent },
  {
    path: 'admin',
    component: AdminDashboardComponent,
    canActivate: [adminGuard], 
    title: 'Admin Dashboard - Coffee Locator',
  },
  { path: '**', redirectTo: '' },
];