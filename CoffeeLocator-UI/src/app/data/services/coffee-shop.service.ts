import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { CoffeeShopNearby } from '../../shared/models/coffee-shop.model';

@Injectable({
  providedIn: 'root'
})
export class CoffeeShopService {
  private apiUrl = 'http://localhost:5224/api/coffeeshops'; 

  constructor(private http: HttpClient) {}

  /**
   * Metod for locatizate coffeeshops
   */

  getNearbyShops(lat: number, lng: number): Observable<CoffeeShopNearby[]> {
    return this.http.get<CoffeeShopNearby[]>(`${this.apiUrl}/nearby?latitude=${lat}&longitude=${lng}`)
      .pipe(
        map(shops => {
          // Si la API devuelve vacío (BD vacía), usamos datos falsos para probar
          if (!shops || shops.length === 0) {
            console.warn('⚠️ API devolvió 0 resultados. Usando MOCK DATA para desarrollo.');
            return this.getMockShops();
          }
          return shops;
        }),
        catchError(err => {
          console.error('❌ Error API. Usando MOCK DATA.', err);
          return of(this.getMockShops());
        })
      );
  }

  private getMockShops(): CoffeeShopNearby[] {
    return [
      {
        id: '1',
        name: 'Café de Prueba 1',
        address: 'Frente al parque, Aguas Zarcas',
        latitude: 10.3750,
        longitude: -84.3440,
        distanceInKm: 0.1,
        averageRating: 4.5,
        totalReviews: 12,
        isPremium: true,
        imageUrl: 'https://images.unsplash.com/photo-1509042239860-f550ce710b93?w=200'
      },
      {
        id: '2',
        name: 'Espresso Test',
        address: '200m Norte de la Iglesia',
        latitude: 10.3760,
        longitude: -84.3420,
        distanceInKm: 0.3,
        averageRating: 5.0,
        totalReviews: 5,
        isPremium: false,
        imageUrl: 'https://images.unsplash.com/photo-1554118811-1e0d58224f24?w=200'
      }
    ];
  }
}