import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoffeeShopService } from '../../../data/services/coffee-shop.service';
import { CoffeeShopNearby } from '../../../shared/models/coffee-shop.model';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin-dashboard.component.html'
})
export class AdminDashboardComponent implements OnInit {
  shops: CoffeeShopNearby[] = [];
  isLoading = true;

  constructor(private coffeeService: CoffeeShopService) {}

  ngOnInit(): void {
    this.loadShops();
  }

  /**
   * Method to load all coffee shops for the admin dashboard.
   */
  loadShops(): void {
    this.isLoading = true;
    this.coffeeService.getAllShops().subscribe({
      next: (data) => {
        this.shops = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading shops', err);
        this.isLoading = false;
      }
    });
  }

  /**
   * Method to handle the deletion of a coffee shop. It prompts the user for confirmation before proceeding with the deletion.  
   * @param id for the coffee shop to be deleted
   */
  deleteShop(id: string): void {
    if (confirm('Are you sure you want to delete this coffee shop? This action cannot be undone.')) {
      this.coffeeService.deleteShop(id).subscribe({
        next: () => {
          this.shops = this.shops.filter(s => s.id !== id);
         
        },
        error: (err) => alert('Error deleting shop')
      });
    }
  }

  /**
   * Method to handle editing a coffee shop. This is a placeholder and should be implemented to actually open a modal dialog for editing the selected coffee shop.
   * @param shop  The coffee shop to be edited. This parameter is currently not used in the method, but it can be utilized to pre-fill the edit form with the shop's existing data when the modal is implemented.
   */
  editShop(shop: CoffeeShopNearby): void {
    console.log('Editing shop:', shop);
    
  }

  /**
   * Metod to open the create coffee shop modal. This is a placeholder and should be implemented to actually open a modal dialog for creating a new coffee shop.
   */
  openCreateModal(): void {
    console.log('Opening create modal');
   
  }
}