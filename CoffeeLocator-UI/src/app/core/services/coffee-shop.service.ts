import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CoffeeShopNearby } from '../../shared/models/coffee-shop.model';

@Injectable({
  providedIn: 'root'
})
export class CoffeeShopService {
  private apiUrl = 'https://localhost:7283/api/CoffeeShops';

  constructor(private http: HttpClient) { }

  /**
   * Fetches nearby shops from the C# Backend.
   */
  getNearbyShops(lat: number, lng: number, radius: number = 10): Observable<CoffeeShopNearby[]> {
    return this.http.get<CoffeeShopNearby[]>(`${this.apiUrl}/nearby?userLat=${lat}&userLon=${lng}&radiusInKm=${radius}`);
  }
}