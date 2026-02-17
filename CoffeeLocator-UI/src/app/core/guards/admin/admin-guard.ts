import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { map, take } from 'rxjs/operators';

export const adminGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

 /**
  * Function to check if the user is authenticated. 
  */
  return authService.currentUser$.pipe(
    take(1),
    map(user => {
      if (user && user.role === 'Admin') {
        return true;
      }
      console.warn('Acceso denegado: Se requiere rol de Administrador');
      router.navigate(['/login']);
      return false;
    })
  );
};