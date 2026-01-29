import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoffeeShopService } from './core/services/coffee-shop.service';
import { CoffeeShopNearby } from './shared/models/coffee-shop.model';
import { CoffeeMapComponent } from './features/coffee-map/coffee-map';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, CoffeeMapComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'      
})
export class AppComponent implements OnInit {
  shops: CoffeeShopNearby[] = [];

  constructor(private coffeeService: CoffeeShopService) {}

  ngOnInit(): void {
    // Calling Aguas Zarcas test coordinates
    this.coffeeService.getNearbyShops(10.3748, -84.3435).subscribe({
      next: (data) => {
        this.shops = data;
        console.log('API connection successful:', this.shops);
      },
      error: (err) => console.error('API connection failed:', err)
    });
  }
}