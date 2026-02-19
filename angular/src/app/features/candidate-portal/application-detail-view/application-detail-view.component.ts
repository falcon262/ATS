import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CandidatePortalService } from '../../../proxy/candidates/candidate-portal.service';
import { ApplicationDto } from '../../../proxy/applications/dtos/models';

@Component({
  selector: 'app-application-detail-view',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container-fluid py-4" *ngIf="application">
      <!-- Header -->
      <div class="row mb-4">
        <div class="col">
          <a routerLink="/candidate/dashboard" class="btn btn-outline-secondary mb-3">
            <i class="fas fa-arrow-left me-2"></i>Back to My Applications
          </a>
          <h1 class="display-5 fw-bold">{{ application.jobTitle }}</h1>
          <p class="text-muted">Application Details</p>
        </div>
      </div>

      <div class="row">
        <!-- Main Content -->
        <div class="col-lg-8">
          <!-- Status Card -->
          <div class="card shadow-sm mb-4">
            <div class="card-body">
              <h5 class="card-title mb-3">Application Status</h5>
              <div class="row">
                <div class="col-md-6 mb-3">
                  <label class="text-muted small">Current Status</label>
                  <div>
                    <span class="badge" [ngClass]="getStatusBadgeClass(application.status)">
                      {{ getStatusText(application.status) }}
                    </span>
                  </div>
                </div>
                <div class="col-md-6 mb-3">
                  <label class="text-muted small">Current Stage</label>
                  <div>
                    <span class="badge bg-secondary">
                      {{ getStageText(application.stage) }}
                    </span>
                  </div>
                </div>
                <div class="col-md-6">
                  <label class="text-muted small">Applied Date</label>
                  <p class="mb-0">{{ application.appliedDate | date: 'MMMM d, y' }}</p>
                </div>
                <div class="col-md-6" *ngIf="application.aiScore">
                  <label class="text-muted small">AI Match Score</label>
                  <div class="score-display">
                    <div class="score-circle" [ngClass]="getScoreClass(application.aiScore)">
                      {{ application.aiScore | number: '1.0-0' }}%
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- Timeline -->
          <div class="card shadow-sm mb-4">
            <div class="card-body">
              <h5 class="card-title mb-4">Application Timeline</h5>
              <div class="timeline">
                <div class="timeline-item" [class.completed]="application.appliedDate">
                  <div class="timeline-marker" [class.active]="application.appliedDate"></div>
                  <div class="timeline-content">
                    <h6>Application Submitted</h6>
                    <p class="text-muted small mb-0" *ngIf="application.appliedDate">
                      {{ application.appliedDate | date: 'MMM d, y, h:mm a' }}
                    </p>
                  </div>
                </div>

                <div class="timeline-item" [class.completed]="application.screeningCompletedDate">
                  <div class="timeline-marker" [class.active]="application.screeningCompletedDate"></div>
                  <div class="timeline-content">
                    <h6>Screening</h6>
                    <p class="text-muted small mb-0" *ngIf="application.screeningCompletedDate">
                      {{ application.screeningCompletedDate | date: 'MMM d, y' }}
                    </p>
                  </div>
                </div>

                <div class="timeline-item" [class.completed]="application.firstInterviewDate">
                  <div class="timeline-marker" [class.active]="application.firstInterviewDate"></div>
                  <div class="timeline-content">
                    <h6>First Interview</h6>
                    <p class="text-muted small mb-0" *ngIf="application.firstInterviewDate">
                      {{ application.firstInterviewDate | date: 'MMM d, y' }}
                    </p>
                  </div>
                </div>

                <div class="timeline-item" [class.completed]="application.finalInterviewDate">
                  <div class="timeline-marker" [class.active]="application.finalInterviewDate"></div>
                  <div class="timeline-content">
                    <h6>Final Interview</h6>
                    <p class="text-muted small mb-0" *ngIf="application.finalInterviewDate">
                      {{ application.finalInterviewDate | date: 'MMM d, y' }}
                    </p>
                  </div>
                </div>

                <div class="timeline-item" [class.completed]="application.decisionDate">
                  <div class="timeline-marker" [class.active]="application.decisionDate"></div>
                  <div class="timeline-content">
                    <h6>Decision</h6>
                    <p class="text-muted small mb-0" *ngIf="application.decisionDate">
                      {{ application.decisionDate | date: 'MMM d, y' }}
                    </p>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- Cover Letter -->
          <div class="card shadow-sm mb-4" *ngIf="application.coverLetter">
            <div class="card-body">
              <h5 class="card-title mb-3">Your Cover Letter</h5>
              <p class="whitespace-pre-line">{{ application.coverLetter }}</p>
            </div>
          </div>
        </div>

        <!-- Sidebar -->
        <div class="col-lg-4">
          <!-- AI Analysis -->
          <div class="card shadow-sm mb-4" *ngIf="application.aiScore">
            <div class="card-body">
              <h5 class="card-title mb-3">AI Match Analysis</h5>
              <div class="ai-score-large mb-3">
                <div class="score-circle-large" [ngClass]="getScoreClass(application.aiScore)">
                  {{ application.aiScore | number: '1.0-0' }}
                </div>
                <p class="text-center mt-2 mb-0 small text-muted">Overall Match Score</p>
              </div>
              <div *ngIf="application.aiMatchSummary">
                <h6>Summary</h6>
                <p class="small">{{ application.aiMatchSummary }}</p>
              </div>
            </div>
          </div>

          <!-- Interview Information -->
          <div class="card shadow-sm mb-4" *ngIf="application.interviewDate">
            <div class="card-body">
              <h5 class="card-title mb-3">
                <i class="fas fa-calendar-alt text-primary me-2"></i>Interview Scheduled
              </h5>
              <div class="mb-2">
                <label class="text-muted small">Date & Time</label>
                <p class="mb-0">{{ application.interviewDate | date: 'MMMM d, y, h:mm a' }}</p>
              </div>
              <div *ngIf="application.interviewLocation">
                <label class="text-muted small">Location</label>
                <p class="mb-0">{{ application.interviewLocation }}</p>
              </div>
            </div>
          </div>

          <!-- Offer Information -->
          <div class="card shadow-sm mb-4 border-success" *ngIf="application.offerDate">
            <div class="card-body">
              <h5 class="card-title text-success mb-3">
                <i class="fas fa-trophy me-2"></i>Job Offer
              </h5>
              <div class="mb-2" *ngIf="application.offeredSalary">
                <label class="text-muted small">Offered Salary</label>
                <p class="mb-0 h5">{{ application.offeredSalary | currency }}</p>
              </div>
              <div class="mb-2">
                <label class="text-muted small">Offer Date</label>
                <p class="mb-0">{{ application.offerDate | date: 'MMMM d, y' }}</p>
              </div>
              <div *ngIf="application.offerExpiryDate">
                <label class="text-muted small">Offer Expires</label>
                <p class="mb-0">{{ application.offerExpiryDate | date: 'MMMM d, y' }}</p>
              </div>
            </div>
          </div>

          <!-- Help Card -->
          <div class="card shadow-sm bg-light">
            <div class="card-body">
              <h6><i class="fas fa-question-circle me-2"></i>Need Help?</h6>
              <p class="small mb-2">If you have questions about your application, please contact the hiring team.</p>
              <p class="small mb-0 text-muted" *ngIf="application.assignedToName">
                <strong>Recruiter:</strong> {{ application.assignedToName }}
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Loading State -->
    <div class="container py-5 text-center" *ngIf="!application && loading">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Loading...</span>
      </div>
      <p class="mt-3">Loading application details...</p>
    </div>

    <!-- Error State -->
    <div class="container py-5 text-center" *ngIf="!application && !loading">
      <div class="alert alert-danger">
        Application not found or you don't have permission to view it.
      </div>
      <a routerLink="/candidate/dashboard" class="btn btn-primary">
        Back to Dashboard
      </a>
    </div>
  `,
  styles: [`
    .score-display {
      .score-circle {
        display: inline-flex;
        width: 60px;
        height: 60px;
        border-radius: 50%;
        align-items: center;
        justify-content: center;
        font-weight: bold;
        color: white;
        font-size: 1.2rem;

        &.high-score {
          background: linear-gradient(135deg, #28a745 0%, #20c997 100%);
        }

        &.medium-score {
          background: linear-gradient(135deg, #ffc107 0%, #fd7e14 100%);
        }

        &.low-score {
          background: linear-gradient(135deg, #dc3545 0%, #e83e8c 100%);
        }
      }
    }

    .ai-score-large {
      text-align: center;

      .score-circle-large {
        display: inline-flex;
        width: 120px;
        height: 120px;
        border-radius: 50%;
        align-items: center;
        justify-content: center;
        font-weight: bold;
        color: white;
        font-size: 2.5rem;

        &.high-score {
          background: linear-gradient(135deg, #28a745 0%, #20c997 100%);
        }

        &.medium-score {
          background: linear-gradient(135deg, #ffc107 0%, #fd7e14 100%);
        }

        &.low-score {
          background: linear-gradient(135deg, #dc3545 0%, #e83e8c 100%);
        }
      }
    }

    .timeline {
      position: relative;
      padding-left: 2rem;

      &::before {
        content: '';
        position: absolute;
        left: 10px;
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

      .timeline-marker {
        position: absolute;
        left: -1.9rem;
        width: 20px;
        height: 20px;
        border-radius: 50%;
        background: #e9ecef;
        border: 3px solid #fff;
        box-shadow: 0 0 0 2px #e9ecef;

        &.active {
          background: #28a745;
          box-shadow: 0 0 0 2px #28a745;
        }
      }

      .timeline-content {
        h6 {
          font-weight: 600;
          margin-bottom: 0.25rem;
        }
      }

      &.completed .timeline-content h6 {
        color: #28a745;
      }
    }

    .whitespace-pre-line {
      white-space: pre-line;
    }
  `]
})
export class ApplicationDetailViewComponent implements OnInit {
  application: ApplicationDto | null = null;
  loading = true;

  constructor(
    private route: ActivatedRoute,
    private candidatePortalService: CandidatePortalService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadApplication(id);
    } else {
      this.loading = false;
    }
  }

  loadApplication(id: string): void {
    this.loading = true;
    this.candidatePortalService.getMyApplicationDetail(id).subscribe({
      next: (app) => {
        this.application = app;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading application:', error);
        this.loading = false;
      }
    });
  }

  getStatusBadgeClass(status: number): string {
    const statusMap: Record<number, string> = {
      0: 'bg-info', 1: 'bg-warning', 2: 'bg-primary', 3: 'bg-warning',
      4: 'bg-success', 5: 'bg-success', 6: 'bg-danger', 7: 'bg-secondary', 8: 'bg-secondary'
    };
    return statusMap[status] || 'bg-secondary';
  }

  getStatusText(status: number): string {
    const statusMap: Record<number, string> = {
      0: 'New', 1: 'In Review', 2: 'Shortlisted', 3: 'Interview',
      4: 'Offered', 5: 'Hired', 6: 'Rejected', 7: 'Withdrawn', 8: 'On Hold'
    };
    return statusMap[status] || 'Unknown';
  }

  getStageText(stage: number): string {
    const stageMap: Record<number, string> = {
      0: 'Applied', 1: 'Screening', 2: 'Phone Screen', 3: 'First Interview',
      4: 'Technical Assessment', 5: 'Final Interview', 6: 'Reference Check',
      7: 'Offer', 8: 'Hired', 9: 'Rejected'
    };
    return stageMap[stage] || 'Unknown';
  }

  getScoreClass(score: number): string {
    if (score >= 75) return 'high-score';
    if (score >= 50) return 'medium-score';
    return 'low-score';
  }
}

