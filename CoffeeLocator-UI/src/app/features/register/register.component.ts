import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, AbstractControl } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router'; 
import { AuthService } from '../../core/services/auth/auth.service'; 

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  registerForm: FormGroup;
  
  mensajeFeedback: string | null = null;
  tipoFeedback: 'success' | 'error' | null = null;
  cargando: boolean = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService, 
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      nombre: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [
        Validators.required, 
        Validators.minLength(8), 
        Validators.pattern(/(?=.*[A-Z])/)
      ]],
      confirmPassword: ['', [Validators.required]]
    }, { validators: this.passwordMatchValidator }); 
  }

  passwordMatchValidator(control: AbstractControl) {
    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }

  get f() { return this.registerForm.controls; }

  get passwordsMatch(): boolean {
    const confirmValue = this.registerForm.get('confirmPassword')?.value;
    return !!confirmValue && !this.registerForm.errors?.['passwordMismatch'];
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
      this.cargando = true;
      this.mensajeFeedback = null;

      this.authService.register(this.registerForm.value).subscribe({
        next: () => {
          this.tipoFeedback = 'success';
          this.mensajeFeedback = '¡Registro exitoso! Entrando al Gremio...';
          setTimeout(() => this.router.navigate(['/login']), 2000);
        },
        error: (err) => {
          this.cargando = false;
          this.tipoFeedback = 'error';
        
          this.mensajeFeedback = err.error?.errors?.Password?.[0] || 'Error al crear la cuenta';
        }
      });
    }
  }
}