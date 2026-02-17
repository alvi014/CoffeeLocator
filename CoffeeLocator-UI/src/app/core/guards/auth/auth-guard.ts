import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  
  console.log('¿Está autenticado?:', authService.isAuthenticated());

  if (authService.isAuthenticated()) {
    return true;
  } else {
    console.log('Acceso denegado, redirigiendo...');
    return router.parseUrl('/login');
  }
};