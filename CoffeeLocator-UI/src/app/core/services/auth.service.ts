import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly TOKEN_KEY = 'auth_token';
  // Token temporal para desarrollo
  private readonly DEV_TOKEN = 'PEGAR_TU_TOKEN_JWT_AQUI'; 

  constructor() {}

  login(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  devLogin(): void {
    if (!this.isAuthenticated() && this.DEV_TOKEN !== 'PEGAR_TU_TOKEN_JWT_AQUI') {
      console.warn('AuthService: Modo Desarrollo - Usando token temporal.');
      this.login(this.DEV_TOKEN);
    }
  }
}