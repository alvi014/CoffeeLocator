import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../core/services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})

export class HeaderComponent {
  constructor(
    public authService: AuthService,
    private router: Router,
  ) {}
  // Check if the current page is login or register
  isAuthPage(): boolean {
    return this.router.url === '/login' || this.router.url === '/register';
  }
  // Get name user from local storage
  get userName(): string {
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    return user.name || 'Coffee Lover';
  }
  // Logout user and navigate to login page
  onLogout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
