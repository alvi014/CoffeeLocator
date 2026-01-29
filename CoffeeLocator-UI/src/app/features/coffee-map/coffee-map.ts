import { Component, OnInit, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import * as L from 'leaflet';
import { CoffeeShopService } from '../../core/services/coffee-shop.service';
import { CoffeeShopNearby } from '../../shared/models/coffee-shop.model';

@Component({
  selector: 'app-coffee-map',
  standalone: true,
  imports: [CommonModule],
templateUrl: './coffee-map.component.html', 
  styleUrl: './coffee-map.component.scss'
})
export class CoffeeMapComponent implements OnInit, AfterViewInit {
  private map!: L.Map;
  shops: CoffeeShopNearby[] = [];

  constructor(private coffeeService: CoffeeShopService) {}

  ngOnInit(): void {
  
  }

  ngAfterViewInit(): void {
    this.initMap();
  }

  private initMap(): void {

    this.map = L.map('map').setView([10.3748, -84.3435], 15);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 19,
      attribution: '© OpenStreetMap'
    }).addTo(this.map);

    this.loadNearbyShops();
  }

  private loadNearbyShops(): void {
    this.coffeeService.getNearbyShops(10.3748, -84.3435).subscribe(data => {
      this.shops = data;
      this.addMarkers();
    });
  }

  private addMarkers(): void {
    this.shops.forEach(shop => {
      const marker = L.marker([shop.latitude, shop.longitude])
        .addTo(this.map)
        .bindPopup(`
          <b>${shop.name}</b><br>
          ${shop.address}<br>
          <img src="${shop.imageUrl}" width="100px">
        `);
    });
  }
}