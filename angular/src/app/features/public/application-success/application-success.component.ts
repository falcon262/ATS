import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';

@Component({
  selector: 'app-application-success',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container py-5">
      <div class="row justify-content-center">
        <div class="col-md-8 col-lg-6">
          <div class="card shadow-lg border-0">
            <div class="card-body text-center p-5">
              <!-- Success Icon -->
              <div class="success-icon mb-4">
                <i class="fas fa-check-circle text-success" style="font-size: 5rem;"></i>
              </div>

              <!-- Success Message -->
              <h1 class="display-5 mb-3">Application Submitted!</h1>
              <p class="lead text-muted mb-4">
                Thank you for applying. We've received your application and will review it carefully.
              </p>

              <div class="alert alert-info">
                <i class="fas fa-info-circle me-2"></i>
                <strong>What's next?</strong><br>
                Create an account to track your application status and receive updates.
              </div>

              <!-- Action Buttons -->
              <div class="d-grid gap-3 mt-4">
                <a [routerLink]="['/register']" [queryParams]="{ applicationId: applicationId }"
                  class="btn btn-primary btn-lg">
                  <i class="fas fa-user-plus me-2"></i>Create Account to Track Application
                </a>
                <a routerLink="/" class="btn btn-outline-secondary">
                  <i class="fas fa-briefcase me-2"></i>Browse More Jobs
                </a>
              </div>

              <!-- Additional Info -->
              <div class="mt-4 pt-4 border-top">
                <p class="text-muted small mb-0">
                  <i class="fas fa-envelope me-2"></i>
                  You'll receive a confirmation email shortly at the address you provided.
                </p>
              </div>
            </div>
          </div>

          <!-- Timeline -->
          <div class="mt-5">
            <h4 class="text-center mb-4">Application Process Timeline</h4>
            <div class="timeline">
              <div class="timeline-item">
                <div class="timeline-marker bg-success"></div>
                <div class="timeline-content">
                  <h6>Application Received</h6>
                  <p class="text-muted small">Your application has been submitted successfully</p>
                </div>
              </div>
              <div class="timeline-item">
                <div class="timeline-marker"></div>
                <div class="timeline-content">
                  <h6>Initial Review</h6>
                  <p class="text-muted small">We'll review your application within 3-5 business days</p>
                </div>
              </div>
              <div class="timeline-item">
                <div class="timeline-marker"></div>
                <div class="timeline-content">
                  <h6>Interview Process</h6>
                  <p class="text-muted small">Qualified candidates will be contacted for interviews</p>
                </div>
              </div>
              <div class="timeline-item">
                <div class="timeline-marker"></div>
                <div class="timeline-content">
                  <h6>Final Decision</h6>
                  <p class="text-muted small">We'll notify you of our final decision</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: [`
    :host {
      display: block;
      background: #ffffff;
      min-height: 100vh;
    }

    .container {
      background: #ffffff;
    }

    .success-icon {
      animation: scaleIn 0.5s ease-out;
    }

    @keyframes scaleIn {
      from {
        transform: scale(0);
        opacity: 0;
      }
      to {
        transform: scale(1);
        opacity: 1;
      }
    }

    .card {
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

    .timeline {
      position: relative;
      padding-left: 2rem;

      &::before {
        content: '';
        position: absolute;
        left: 8px;
        top: 0;
        bottom: 0;
        width: 2px;
        background: #e9ecef;
      }
    }

    .timeline-item {
      position: relative;
      padding-bottom: 2rem;

      &:last-child {
        padding-bottom: 0;
      }
    }

    .timeline-marker {
      position: absolute;
      left: -2rem;
      width: 20px;
      height: 20px;
      border-radius: 50%;
      background: #e9ecef;
      border: 3px solid #fff;
      box-shadow: 0 0 0 2px #e9ecef;

      &.bg-success {
        background: #28a745;
        box-shadow: 0 0 0 2px #28a745;
      }
    }

    .timeline-content {
      h6 {
        font-weight: 600;
        margin-bottom: 0.25rem;
      }

      p {
        margin-bottom: 0;
      }
    }

    .btn-primary {
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      border: none;
      font-weight: 600;

      &:hover {
        transform: translateY(-2px);
        box-shadow: 0 4px 12px rgba(102, 126, 234, 0.4);
      }
    }
  `]
})
export class ApplicationSuccessComponent implements OnInit {
  applicationId: string = '';

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.applicationId = this.route.snapshot.queryParamMap.get('applicationId') || '';
  }
}

