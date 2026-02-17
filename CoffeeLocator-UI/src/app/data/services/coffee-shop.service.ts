import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { 
  CoffeeShopNearby, 
  CoffeeShopDetail, 
  CreateCoffeeShop, 
  UpdateCoffeeShop 
} from '../../shared/models/coffee-shop.model';

@Injectable({
  providedIn: 'root'
})
export class CoffeeShopService {
  private apiUrl = 'https://localhost:7283/api/coffeeshops'; 

  constructor(private http: HttpClient) {}

  /**
   * Retrieves all coffee shops (Admin/General use)
   */
  getAllShops(): Observable<CoffeeShopNearby[]> {
    return this.http.get<CoffeeShopNearby[]>(this.apiUrl);
  }

  /**
   * Retrieves coffee shops based on proximity to the user.
   * Parameter names MUST match the Backend Action parameters.
   */
  getNearbyShops(lat: number, lng: number, radius: number = 5): Observable<CoffeeShopNearby[]> {
    const params = new HttpParams()
      .set('userLat', lat.toString())
      .set('userLng', lng.toString()) 
      .set('radiusInKm', radius.toString()); 

    return this.http.get<CoffeeShopNearby[]>(`${this.apiUrl}/nearby`, { params })
      .pipe(
        map(shops => (shops?.length > 0 ? shops : this.getMockShops())),
        catchError((err) => {
          console.error('Backend error, loading mocks:', err);
          return of(this.getMockShops());
        })
      );
  }

  /**
   * Retrieves full details of a specific shop
   */
  getShopById(id: string): Observable<CoffeeShopDetail> {
    return this.http.get<CoffeeShopDetail>(`${this.apiUrl}/${id}`);
  }

  /**
   * Admin: Creates a new coffee shop
   */
  createShop(shop: CreateCoffeeShop): Observable<CoffeeShopDetail> {
    return this.http.post<CoffeeShopDetail>(this.apiUrl, shop);
  }

  /**
   * Admin: Updates an existing coffee shop.
   * Matches the route [HttpPut("{id}")]
   */
  updateShop(id: string, shop: UpdateCoffeeShop): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, shop);
  }

  /**
   * Admin: Deletes a coffee shop
   */
  deleteShop(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  /**
   * Fallback data for development when the database is empty or the API fails.
   */
  private getMockShops(): CoffeeShopNearby[] {
    return [
      {
        id: '1',
        name: 'Café de Prueba 1 (Mock)',
        address: 'Frente al parque, Aguas Zarcas',
        latitude: 10.3750,
        longitude: -84.3440,
        distanceInKm: 0.1,
        averageRating: 4.5,
        totalReviews: 12,
        isPremium: true,
        imageUrl: 'https://images.unsplash.com/photo-1509042239860-f550ce710b93?w=200'
      }
    ];
  }
}