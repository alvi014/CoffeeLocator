import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, BehaviorSubject } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly TOKEN_KEY = 'auth_token';
private apiUrl = 'https://localhost:7283/api/auth';

  private currentUserSubject = new BehaviorSubject<any>(this.getUserFromToken());
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {}

  /**
   * authenticates a user and stores the JWT token.
   */
  login(email: string, password: string): Observable<any> {
    const body = { Email: email, Password: password };
    return this.http.post<any>(`${this.apiUrl}/login`, body).pipe(
      tap((response) => {
        if (response && response.token) {
          localStorage.setItem(this.TOKEN_KEY, response.token);
          this.currentUserSubject.next(this.getUserFromToken());
        }
      })
    );
  }

  /**
   * Registers a new user in the system.
   */
  register(userData: any): Observable<any> {
    const body = {
      Email: userData.email?.trim(),
      Password: userData.password,
      FullName: (userData.fullName || userData.nombre || userData.FullName)?.trim(),
    };
    return this.http.post<any>(`${this.apiUrl}/register`, body);
  }

  /**
   * Placeholder for debugging initialization.
   */
  devLogin(): void {
    console.log('🚀 AuthService: Auth system initialized successfully.');
  }

  /**
   * Extracts user data from the stored JWT token.
   */
  private getUserFromToken(): any {
    const token = this.getToken();
    if (!token) return null;
    try {
      const decoded: any = jwtDecode(token);

      return {
        email: decoded.email,
        role: decoded.role || decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
      };
    } catch {
      return null;
    }
  }

  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    this.currentUserSubject.next(null);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}