import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.registerForm = this.fb.group({
      nombre: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }


  isInvalid(field: string): boolean {
    const control = this.registerForm.get(field);
    return !!(control && control.invalid && (control.dirty || control.touched));
  }


  isValid(field: string): boolean {
    const control = this.registerForm.get(field);
    return !!(control && control.valid && (control.dirty || control.touched));
  }

  onSubmit() {
    if (this.registerForm.valid) {
      console.log('¡Bienvenido al gremio!', this.registerForm.value);
    } else {
      this.registerForm.markAllAsTouched(); 
    }
  }
}