import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CandidatePortalService } from '../../../proxy/candidates/candidate-portal.service';
import { ToasterService } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-candidate-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  template: `
    <div class="container py-5">
      <div class="row justify-content-center">
        <div class="col-md-6 col-lg-5">
          <div class="card shadow-lg border-0">
            <div class="card-body p-5">
              <!-- Header -->
              <div class="text-center mb-4">
                <h2 class="fw-bold">Create Your Account</h2>
                <p class="text-muted">Track your application and receive updates</p>
              </div>

              <!-- Registration Form -->
              <form [formGroup]="registerForm" (ngSubmit)="onSubmit()">
                <!-- Email -->
                <div class="mb-3">
                  <label for="email" class="form-label">Email Address</label>
                  <input type="email" class="form-control form-control-lg" id="email"
                    formControlName="email"
                    [class.is-invalid]="isFieldInvalid('email')">
                  <div class="invalid-feedback">Valid email is required</div>
                </div>

                <!-- Password -->
                <div class="mb-3">
                  <label for="password" class="form-label">Password</label>
                  <div class="input-group">
                    <input [type]="showPassword ? 'text' : 'password'"
                      class="form-control form-control-lg"
                      id="password"
                      formControlName="password"
                      [class.is-invalid]="isFieldInvalid('password')">
                    <button class="btn btn-outline-secondary" type="button"
                      (click)="showPassword = !showPassword">
                      <i class="fas" [ngClass]="showPassword ? 'fa-eye-slash' : 'fa-eye'"></i>
                    </button>
                  </div>
                  <div class="invalid-feedback d-block" *ngIf="isFieldInvalid('password')">
                    Password must be at least 6 characters
                  </div>
                  <div class="form-text">
                    <small>Minimum 6 characters</small>
                  </div>
                </div>

                <!-- Confirm Password -->
                <div class="mb-3">
                  <label for="confirmPassword" class="form-label">Confirm Password</label>
                  <input [type]="showConfirmPassword ? 'text' : 'password'"
                    class="form-control form-control-lg"
                    id="confirmPassword"
                    formControlName="confirmPassword"
                    [class.is-invalid]="isFieldInvalid('confirmPassword') || passwordMismatch()">
                  <div class="invalid-feedback d-block" *ngIf="passwordMismatch() && registerForm.get('confirmPassword')?.touched">
                    Passwords do not match
                  </div>
                </div>

                <!-- Terms and Conditions -->
                <div class="form-check mb-4">
                  <input class="form-check-input" type="checkbox" id="acceptTerms"
                    formControlName="acceptTerms"
                    [class.is-invalid]="isFieldInvalid('acceptTerms')">
                  <label class="form-check-label" for="acceptTerms">
                    I accept the <a href="#" class="text-decoration-none">Terms and Conditions</a>
                  </label>
                  <div class="invalid-feedback">You must accept the terms and conditions</div>
                </div>

                <!-- Submit Button -->
                <div class="d-grid">
                  <button type="submit" class="btn btn-primary btn-lg"
                    [disabled]="submitting || registerForm.invalid">
                    <span *ngIf="!submitting">
                      <i class="fas fa-user-plus me-2"></i>Create Account
                    </span>
                    <span *ngIf="submitting">
                      <span class="spinner-border spinner-border-sm me-2" role="status"></span>
                      Creating Account...
                    </span>
                  </button>
                </div>
              </form>

              <!-- Login Link -->
              <div class="text-center mt-4">
                <p class="text-muted">
                  Already have an account?
                  <a routerLink="/account/login" class="text-decoration-none fw-bold">Sign In</a>
                </p>
              </div>

              <!-- Security Note -->
              <div class="mt-4 p-3 bg-light rounded">
                <p class="text-muted small mb-0">
                  <i class="fas fa-shield-alt text-success me-2"></i>
                  Your information is secure and will only be used for recruitment purposes.
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .card {
      border-radius: 16px;
      animation: fadeInUp 0.6s ease-out;
    }

    @keyframes fadeInUp {
      from {
        transform: translateY(30px);
        opacity: 0;
      }
      to {
        transform: translateY(0);
        opacity: 1;
      }
    }

    .btn-primary {
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      border: none;
      font-weight: 600;
      padding: 0.75rem;

      &:hover {
        transform: translateY(-2px);
        box-shadow: 0 4px 12px rgba(102, 126, 234, 0.4);
      }
    }

    .form-control-lg {
      padding: 0.75rem 1rem;
      font-size: 1rem;
    }

    .input-group .btn {
      border-color: #ced4da;
    }
  `]
})
export class CandidateRegisterComponent implements OnInit {
  registerForm!: FormGroup;
  submitting = false;
  showPassword = false;
  showConfirmPassword = false;
  applicationId: string = '';

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private candidatePortalService: CandidatePortalService,
    private toaster: ToasterService
  ) {}

  ngOnInit(): void {
    this.applicationId = this.route.snapshot.queryParamMap.get('applicationId') || '';

    if (!this.applicationId) {
      this.toaster.warn('No application ID provided. Please apply to a job first.');
      this.router.navigate(['/']);
      return;
    }

    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
      acceptTerms: [false, Validators.requiredTrue]
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.registerForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  passwordMismatch(): boolean {
    const password = this.registerForm.get('password')?.value;
    const confirmPassword = this.registerForm.get('confirmPassword')?.value;
    return password !== confirmPassword && !!confirmPassword;
  }

  onSubmit(): void {
    if (this.registerForm.invalid) {
      Object.keys(this.registerForm.controls).forEach(key => {
        this.registerForm.get(key)?.markAsTouched();
      });
      return;
    }

    if (this.passwordMismatch()) {
      this.toaster.warn('Passwords do not match');
      return;
    }

    this.submitting = true;
    const formValue = this.registerForm.value;

    const registrationData = {
      email: formValue.email,
      password: formValue.password,
      confirmPassword: formValue.confirmPassword,
      applicationId: this.applicationId,
      acceptTerms: formValue.acceptTerms
    };

    this.candidatePortalService.registerFromApplication(registrationData).subscribe({
      next: () => {
        this.toaster.success('Account created successfully! Please log in.', 'Welcome!');
        // Redirect to login page with redirect URL to candidate dashboard
        this.router.navigate(['/account/login'], {
          queryParams: { returnUrl: '/candidate/dashboard' }
        });
      },
      error: (error) => {
        console.error('Registration error:', error);
        this.toaster.error(
          error.error?.error?.message || 'Failed to create account. Please try again.'
        );
        this.submitting = false;
      }
    });
  }
}

