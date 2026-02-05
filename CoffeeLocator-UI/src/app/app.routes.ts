import { Routes } from '@angular/router';
import { CoffeeMapComponent } from './features/coffee-map/coffee-map.component';

export const routes: Routes = [
  { path: '', component: CoffeeMapComponent }, 
  { path: 'map', component: CoffeeMapComponent }
  //{ path: 'coffee-shop/:id', component: CoffeeShopDetailComponent }

];