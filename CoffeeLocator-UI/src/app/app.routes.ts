import { Routes } from '@angular/router';
import { CoffeeMapComponent } from './features/coffee-map/coffee-map.component';
import { authGuard } from './core/guards/auth-guard';

export const routes: Routes = [

  { path: '', redirectTo: 'map', pathMatch: 'full' },
  
  { 
    path: 'map', 
    component: CoffeeMapComponent, 
    canActivate: [authGuard] 
  },
  
// routes protected by authGuard, solo accesibles si el usuario está autenticado
  { 
    path: 'login', 
    loadComponent: () => import('./features/auth/login/login.component').then(m => m.LoginComponent) 
  }
];