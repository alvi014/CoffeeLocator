import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs'; 

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly TOKEN_KEY = 'auth_token';
  
private apiUrl = 'http://localhost:5224/api/auth';

  constructor(private http: HttpClient) {}

 login(email: string, password: string): Observable<any> {
 
  const body = { 
    Email: email, 
    Password: password 
  };
  return this.http.post<any>(`${this.apiUrl}/login`, body).pipe(
    tap(response => {
      if (response && response.token) {
        localStorage.setItem(this.TOKEN_KEY, response.token);
      }
    })
  );
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
    console.log('Modo desarrollo activo: El servicio está listo para conectar con .NET');
  }
}