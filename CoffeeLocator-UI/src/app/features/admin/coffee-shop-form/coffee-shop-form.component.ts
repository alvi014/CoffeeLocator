import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CoffeeShopService } from '../../../data/services/coffee-shop.service';
import { CoffeeShopDetail, CreateCoffeeShop, UpdateCoffeeShop } from '../../../shared/models/coffee-shop.model';

@Component({
  selector: 'app-coffee-shop-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './coffee-shop-form.component.html'
})
export class CoffeeShopFormComponent implements OnInit {
  @Input() shopToEdit?: CoffeeShopDetail; 
  @Output() formClosed = new EventEmitter<void>();
  @Output() shopSaved = new EventEmitter<void>();

  shopForm: FormGroup;
  isSubmitting = false;

  constructor(private fb: FormBuilder, private coffeeService: CoffeeShopService) {
    this.shopForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      address: ['', [Validators.required]],
      description: [''],
      latitude: [null, [Validators.required]],
      longitude: [null, [Validators.required]],
      googlePlaceId: [''],
      imageUrl: [''],
      isPremium: [false]
    });
  }

  ngOnInit(): void {
    if (this.shopToEdit) {
      this.shopForm.patchValue(this.shopToEdit);
    }
  }

  /**
   * Function to handle form submission for both creating and updating a coffee shop. It checks if the form is valid, then either sends a create or update request   
   * @returns 
   */
  save(): void {
    if (this.shopForm.invalid) return;

    this.isSubmitting = true;
    const formData = this.shopForm.value;

    if (this.shopToEdit) {
      const updateData: UpdateCoffeeShop = { ...formData, id: this.shopToEdit.id };
      this.coffeeService.updateShop(this.shopToEdit.id, updateData).subscribe({
        next: () => this.handleSuccess(),
        error: (err) => this.handleError(err)
      });
    } else {
      const createData: CreateCoffeeShop = formData;
      this.coffeeService.createShop(createData).subscribe({
        next: () => this.handleSuccess(),
        error: (err) => this.handleError(err)
      });
    }
  }

  /**
   * Function to handle successful save operation. 
   */
  private handleSuccess(): void {
    this.isSubmitting = false;
    this.shopSaved.emit();
  }

  /**
   * Function to handle errors during save operation. It resets the submitting state and shows an alert to the user.
   * @param err 
   */
  private handleError(err: any): void {
    this.isSubmitting = false;
    alert('An error occurred while saving the coffee shop.');
    console.error(err);
  }
}