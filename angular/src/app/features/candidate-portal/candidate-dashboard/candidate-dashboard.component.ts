import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CandidatePortalService } from '../../../proxy/candidates/candidate-portal.service';
import { CandidateApplicationListDto } from '../../../proxy/candidates/models';
import { ConfigStateService } from '@abp/ng.core';

@Component({
  selector: 'app-candidate-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container-fluid py-4">
      <!-- Header -->
      <div class="row mb-4">
        <div class="col">
          <h1 class="display-5 fw-bold">My Applications</h1>
          <p class="text-muted">Track the status of your job applications</p>
        </div>
      </div>

      <!-- Loading State -->
      <div *ngIf="loading" class="text-center py-5">
        <div class="spinner-border text-primary" role="status">
          <span class="visually-hidden">Loading...</span>
        </div>
        <p class="mt-3 text-muted">Loading your applications...</p>
      </div>

      <!-- Applications List -->
      <div *ngIf="!loading && applications.length > 0" class="row">
        <div class="col-12">
          <div class="card shadow-sm">
            <div class="card-body">
              <div class="table-responsive">
                <table class="table table-hover">
                  <thead>
                    <tr>
                      <th>Job Title</th>
                      <th>Applied Date</th>
                      <th>Status</th>
                      <th>Stage</th>
                      <th>AI Score</th>
                      <th>Actions</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr *ngFor="let app of applications">
                      <td>
                        <strong>{{ app.jobTitle }}</strong>
                        <br>
                        <small class="text-muted" *ngIf="app.company">{{ app.company }}</small>
                      </td>
                      <td>{{ app.appliedDate | date: 'MMM d, y' }}</td>
                      <td>
                        <span class="badge" [ngClass]="getStatusBadgeClass(app.status)">
                          {{ getStatusText(app.status) }}
                        </span>
                      </td>
                      <td>
                        <span class="badge bg-secondary">
                          {{ getStageText(app.stage) }}
                        </span>
                      </td>
                      <td>
                        <div class="ai-score" *ngIf="app.aiScore">
                          <div class="score-circle" [ngClass]="getScoreClass(app.aiScore)">
                            {{ app.aiScore | number: '1.0-0' }}
                          </div>
                        </div>
                        <span *ngIf="!app.aiScore" class="text-muted">-</span>
                      </td>
                      <td>
                        <a [routerLink]="['/candidate/applications', app.id]"
                          class="btn btn-sm btn-outline-primary">
                          <i class="fas fa-eye me-1"></i>View Details
                        </a>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>

          <!-- Summary Cards -->
          <div class="row mt-4">
            <div class="col-md-3">
              <div class="stat-card">
                <div class="stat-icon bg-primary">
                  <i class="fas fa-file-alt"></i>
                </div>
                <div class="stat-content">
                  <h3>{{ applications.length }}</h3>
                  <p>Total Applications</p>
                </div>
              </div>
            </div>
            <div class="col-md-3">
              <div class="stat-card">
                <div class="stat-icon bg-info">
                  <i class="fas fa-clock"></i>
                </div>
                <div class="stat-content">
                  <h3>{{ getStatusCount(0) + getStatusCount(1) }}</h3>
                  <p>In Review</p>
                </div>
              </div>
            </div>
            <div class="col-md-3">
              <div class="stat-card">
                <div class="stat-icon bg-warning">
                  <i class="fas fa-user-tie"></i>
                </div>
                <div class="stat-content">
                  <h3>{{ getStatusCount(3) }}</h3>
                  <p>Interviews</p>
                </div>
              </div>
            </div>
            <div class="col-md-3">
              <div class="stat-card">
                <div class="stat-icon bg-success">
                  <i class="fas fa-check-circle"></i>
                </div>
                <div class="stat-content">
                  <h3>{{ getStatusCount(4) }}</h3>
                  <p>Offers</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Empty State -->
      <div *ngIf="!loading && applications.length === 0" class="text-center py-5">
        <div class="empty-state">
          <i class="fas fa-briefcase text-muted" style="font-size: 5rem;"></i>
          <h3 class="mt-4">No Applications Yet</h3>
          <p class="text-muted">You haven't applied to any jobs yet. Start exploring opportunities!</p>
          <a routerLink="/" class="btn btn-primary mt-3">
            <i class="fas fa-search me-2"></i>Browse Jobs
          </a>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .stat-card {
      background: white;
      border-radius: 12px;
      padding: 1.5rem;
      box-shadow: 0 2px 8px rgba(0,0,0,0.08);
      display: flex;
      align-items: center;
      gap: 1rem;
      margin-bottom: 1rem;

      .stat-icon {
        width: 60px;
        height: 60px;
        border-radius: 12px;
        display: flex;
        align-items: center;
        justify-content: center;
        font-size: 1.5rem;
        color: white;
      }

      .stat-content {
        h3 {
          font-size: 2rem;
          font-weight: bold;
          margin: 0;
          color: #333;
        }

        p {
          margin: 0;
          color: #6c757d;
          font-size: 0.9rem;
        }
      }
    }

    .ai-score {
      .score-circle {
        width: 50px;
        height: 50px;
        border-radius: 50%;
        display: inline-flex;
        align-items: center;
        justify-content: center;
        font-weight: bold;
        color: white;
        font-size: 1.1rem;

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

    .table {
      th {
        font-weight: 600;
        color: #495057;
        border-bottom: 2px solid #dee2e6;
      }

      tbody tr {
        cursor: pointer;
        transition: background-color 0.2s;

        &:hover {
          background-color: #f8f9fa;
        }
      }
    }

    .empty-state {
      padding: 3rem;
    }
  `]
})
export class CandidateDashboardComponent implements OnInit {
  applications: CandidateApplicationListDto[] = [];
  loading = true;

  constructor(
    private candidatePortalService: CandidatePortalService,
    private configState: ConfigStateService
  ) {}

  ngOnInit(): void {
    this.loadApplications();
  }

  loadApplications(): void {
    this.loading = true;
    this.candidatePortalService.getMyApplications().subscribe({
      next: (apps) => {
        this.applications = apps;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading applications:', error);
        this.loading = false;
      }
    });
  }

  getStatusBadgeClass(status: number): string {
    const statusMap: Record<number, string> = {
      0: 'bg-info',      // New
      1: 'bg-warning',   // InReview
      2: 'bg-primary',   // Shortlisted
      3: 'bg-warning',   // Interview
      4: 'bg-success',   // Offered
      5: 'bg-success',   // Hired
      6: 'bg-danger',    // Rejected
      7: 'bg-secondary', // Withdrawn
      8: 'bg-secondary'  // OnHold
    };
    return statusMap[status] || 'bg-secondary';
  }

  getStatusText(status: number): string {
    const statusMap: Record<number, string> = {
      0: 'New',
      1: 'In Review',
      2: 'Shortlisted',
      3: 'Interview',
      4: 'Offered',
      5: 'Hired',
      6: 'Rejected',
      7: 'Withdrawn',
      8: 'On Hold'
    };
    return statusMap[status] || 'Unknown';
  }

  getStageText(stage: number): string {
    const stageMap: Record<number, string> = {
      0: 'Applied',
      1: 'Screening',
      2: 'Phone Screen',
      3: 'First Interview',
      4: 'Technical Assessment',
      5: 'Final Interview',
      6: 'Reference Check',
      7: 'Offer',
      8: 'Hired',
      9: 'Rejected'
    };
    return stageMap[stage] || 'Unknown';
  }

  getScoreClass(score: number): string {
    if (score >= 75) return 'high-score';
    if (score >= 50) return 'medium-score';
    return 'low-score';
  }

  getStatusCount(status: number): number {
    return this.applications.filter(app => app.status === status).length;
  }
}

