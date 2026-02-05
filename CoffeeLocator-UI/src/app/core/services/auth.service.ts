import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly TOKEN_KEY = 'auth_token';

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    // Simulación de login exitoso
    const mockResponse = { token: 'fake-jwt-token', user: { email } };
    localStorage.setItem(this.TOKEN_KEY, mockResponse.token);
    return of(mockResponse);
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
    console.log('Modo desarrollo activo');
  }
}