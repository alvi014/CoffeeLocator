import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CoffeeShopService } from '../../../data/services/coffee-shop.service';
import { CoffeeShopNearby } from '../../../shared/models/coffee-shop.model';
import { CoffeeShopModalComponent } from '../coffee-shop-modal/coffee-shop-modal.component';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, CoffeeShopModalComponent],
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  shops: CoffeeShopNearby[] = [];
  isLoading = true;

  // Modal state
  showModal = false;
  selectedShop?: CoffeeShopNearby;  

  // Filters & sorting
  searchTerm = '';
  activeFilter: 'all' | 'premium' | 'standard' = 'all';
  sortAsc = false;

  // ── KPIs ──────────────────────────────────────────────────────────────────

  get avgRating(): number {
    if (!this.shops.length) return 0;
    return this.shops.reduce((s, x) => s + (x.averageRating ?? 0), 0) / this.shops.length;
  }

  get totalReviews(): number {
    return this.shops.reduce((s, x) => s + (x.totalReviews ?? 0), 0);
  }

  get premiumCount(): number {
    return this.shops.filter(s => s.isPremium).length;
  }

  get premiumPct(): number {
    return this.shops.length ? (this.premiumCount / this.shops.length) * 100 : 0;
  }

  // ── FILTERED LIST ─────────────────────────────────────────────────────────

  get filteredShops(): CoffeeShopNearby[] {
    let result = [...this.shops];

    if (this.searchTerm.trim()) {
      const term = this.searchTerm.toLowerCase();
      result = result.filter(s =>
        s.name.toLowerCase().includes(term) ||
        s.address?.toLowerCase().includes(term)
      );
    }

    if (this.activeFilter === 'premium')  result = result.filter(s => s.isPremium);
    if (this.activeFilter === 'standard') result = result.filter(s => !s.isPremium);

    result.sort((a, b) => {
      const diff = (a.averageRating ?? 0) - (b.averageRating ?? 0);
      return this.sortAsc ? diff : -diff;
    });

    return result;
  }

  // ── LIFECYCLE ─────────────────────────────────────────────────────────────

  constructor(private coffeeService: CoffeeShopService) {}

  ngOnInit(): void {
    this.loadShops();
  }

  loadShops(): void {
    this.isLoading = true;
    this.coffeeService.getAllShops().subscribe({
      next: (data) => { this.shops = data; this.isLoading = false; },
      error: (err) => { console.error('Error loading shops', err); this.isLoading = false; }
    });
  }

  // ── FILTER & SORT ─────────────────────────────────────────────────────────

  setFilter(filter: 'all' | 'premium' | 'standard'): void {
    this.activeFilter = filter;
  }

  toggleSort(): void {
    this.sortAsc = !this.sortAsc;
  }

  // ── MODAL ─────────────────────────────────────────────────────────────────

  openCreateModal(): void {
    this.selectedShop = undefined;
    this.showModal = true;
  }

  editShop(shop: CoffeeShopNearby): void {
    this.selectedShop = shop;
    this.showModal = true;
  }

  onModalClose(): void {
    this.showModal = false;
    this.selectedShop = undefined;
  }

  onModalSaved(): void {
    this.showModal = false;
    this.selectedShop = undefined;
    this.loadShops(); 
  }

  // ── DELETE ────────────────────────────────────────────────────────────────

  deleteShop(id: string): void {
    if (confirm('¿Confirmas la eliminación de esta cafetería del registro?')) {
      this.coffeeService.deleteShop(id).subscribe({
        next: () => { this.shops = this.shops.filter(s => s.id !== id); },
        error: (err) => {
          console.error('Delete failed', err);
          alert('No se pudo eliminar. Puede tener reseñas activas asociadas.');
        }
      });
    }
  }

  // ── ANALYTICS ────────────────────────────────────────────────────────────

  viewAnalytics(shopId: string): void {
    console.log('Analytics para shop:', shopId);
    // TODO: abrir panel de analytics
  }
}