import { Component, OnInit, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import * as L from 'leaflet';
import { CoffeeShopService } from '../../data/services/coffee-shop.service';
import { CoffeeShopNearby } from '../../shared/models/coffee-shop.model';

@Component({
  selector: 'app-coffee-map',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './coffee-map.component.html',
  styleUrl: './coffee-map.component.css'
})
export class CoffeeMapComponent implements OnInit, AfterViewInit {
  private map!: L.Map;
  private markersLayer = L.layerGroup(); 
  shops: CoffeeShopNearby[] = [];

  constructor(private coffeeService: CoffeeShopService) {}

  ngOnInit(): void {
    this.fixLeafletIcons();
  }

  ngAfterViewInit(): void {
    this.initMap();
  }

  private initMap(): void {
    // --- MODO PRUEBA: Forzamos la ubicación de Aguas Zarcas para ver los datos del Backend ---
    // Esto asegura que veas los pines aunque estés en otro lugar físico.
    this.loadMap(10.3748, -84.3435); // <--- ¡CAMBIA ESTOS NÚMEROS POR LOS DE TU BD!

    // --- MODO REAL: Descomentar esto cuando quieras usar el GPS real ---
    // if (navigator.geolocation) {
    //   navigator.geolocation.getCurrentPosition(
    //     (position) => {
    //       const { latitude, longitude } = position.coords;
    //       this.loadMap(latitude, longitude);
    //     },
    //     () => {
    //       // Si falla o niegan permiso, ubicación por defecto
    //       this.loadMap(10.3748, -84.3435);
    //     }
    //   );
    // } else {
    //   this.loadMap(10.3748, -84.3435);
    // }
  }

  private loadMap(lat: number, lng: number): void {
    // Limpiar mapa si ya existe para evitar errores de inicialización doble
    if (this.map) {
      this.map.remove();
    }

    this.map = L.map('map').setView([lat, lng], 15);

    // Agregar la capa de marcadores al mapa
    this.markersLayer.addTo(this.map);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 19,
      attribution: '© OpenStreetMap'
    }).addTo(this.map);

    // Marcador de "Tú estás aquí"
    L.marker([lat, lng]).addTo(this.map).bindPopup('¡Estás aquí!').openPopup();

    this.loadNearbyShops(lat, lng);

    // --- EVENTO DE MOVIMIENTO ---
    // Si el usuario "viaja" por el mapa, cargamos las cafeterías de la nueva zona
    this.map.on('moveend', () => {
      const center = this.map.getCenter();
      this.loadNearbyShops(center.lat, center.lng);
    });
  }

  private loadNearbyShops(lat: number, lng: number): void {
    this.coffeeService.getNearbyShops(lat, lng).subscribe(data => {
      this.shops = data;
      console.log('📍 Cafeterías encontradas para el mapa:', data.length);
      this.addMarkers();
    });
  }

  private addMarkers(): void {
    // Limpiamos los marcadores anteriores para no duplicar
    this.markersLayer.clearLayers();

    this.shops.forEach(shop => {
      const marker = L.marker([shop.latitude, shop.longitude]);
      
      // HTML del Popup
      const popupContent = `
          <div style="min-width: 160px; text-align: center;">
            <h3 style="margin: 0 0 5px 0; color: #6f4e37;">${shop.name}</h3>
            <img src="${shop.imageUrl}" style="width: 100%; height: 100px; object-fit: cover; border-radius: 4px; margin-bottom: 5px;">
            <p style="margin: 5px 0; font-size: 0.9em;">${shop.address}</p>
            <button id="btn-details-${shop.id}" style="background: #6f4e37; color: white; border: none; padding: 5px 10px; border-radius: 3px; cursor: pointer; width: 100%;">Ver Detalles</button>
          </div>
        `;

      marker.bindPopup(popupContent);

      // Evento para detectar cuando se abre el popup y vincular el click del botón
      marker.on('popupopen', () => {
        const btn = document.getElementById(`btn-details-${shop.id}`);
        if (btn) {
          btn.onclick = () => this.openShopDetails(shop);
        }
      });

      // Agregamos el marcador a la capa (no directamente al mapa)
      this.markersLayer.addLayer(marker);
    });
  }

  // Función que se ejecuta al dar click en "Ver Detalles"
  private openShopDetails(shop: CoffeeShopNearby): void {
    // Aquí podrías abrir un Modal real. Por ahora usamos un alert informativo.
    alert(`☕ ${shop.name}\n⭐ Calificación: ${shop.averageRating}/5\n💬 Reviews: ${shop.totalReviews}\n\nAquí se mostrarían los comentarios detallados.`);
  }

  private fixLeafletIcons(): void {
    // Solución para el bug de iconos perdidos en Leaflet + Angular
    const iconRetinaUrl = 'https://unpkg.com/leaflet@1.7.1/dist/images/marker-icon-2x.png';
    const iconUrl = 'https://unpkg.com/leaflet@1.7.1/dist/images/marker-icon.png';
    const shadowUrl = 'https://unpkg.com/leaflet@1.7.1/dist/images/marker-shadow.png';

    const defaultIcon = L.icon({
      iconRetinaUrl,
      iconUrl,
      shadowUrl,
      iconSize: [25, 41],
      iconAnchor: [12, 41],
      popupAnchor: [1, -34],
      tooltipAnchor: [16, -28],
      shadowSize: [41, 41]
    });

    L.Marker.prototype.options.icon = defaultIcon;
  }
}