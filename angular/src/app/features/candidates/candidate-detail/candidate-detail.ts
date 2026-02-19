import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CandidateService } from '@proxy/candidates';
import { CandidateDto } from '@proxy/candidates/dtos/models';
import { CandidateStatus } from '@proxy/candidates/candidate-status.enum';
import { ApplicationService } from '@proxy/applications';
import { ApplicationListDto } from '@proxy/applications/dtos/models';
import { ApplicationStatus } from '@proxy/applications/application-status.enum';
import { PipelineStage } from '@proxy/applications/pipeline-stage.enum';
import { ToasterService } from '@abp/ng.theme.shared';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-candidate-detail',
  imports: [CommonModule, RouterModule],
  templateUrl: './candidate-detail.html',
  styleUrl: './candidate-detail.scss'
})
export class CandidateDetail implements OnInit {
  candidate: CandidateDto | null = null;
  applications: ApplicationListDto[] = [];
  loading = false;
  loadingApplications = false;
  downloadingResume = false;

  private toaster = inject(ToasterService);

  constructor(
    private candidateService: CandidateService,
    private applicationService: ApplicationService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];
      if (id) {
        this.loading = true;
        this.candidateService.get(id).subscribe({
          next: (candidate) => {
            this.candidate = candidate;
            this.loading = false;
            this.loadApplications(id);
          },
          error: (error) => {
            console.error('Failed to load candidate:', error);
            this.loading = false;
          }
        });
      }
    });
  }

  loadApplications(candidateId: string): void {
    this.loadingApplications = true;
    this.applicationService.getList({
      candidateId: candidateId,
      skipCount: 0,
      maxResultCount: 100,
      sorting: 'appliedDate DESC'
    }).subscribe({
      next: (result) => {
        this.applications = result.items || [];
        this.loadingApplications = false;
      },
      error: (error) => {
        console.error('Failed to load applications:', error);
        this.loadingApplications = false;
      }
    });
  }

  getLocation(): string {
    if (!this.candidate) return '';
    const parts = [this.candidate.city, this.candidate.state, this.candidate.country].filter(Boolean);
    return parts.join(', ');
  }

  getApplicationScoreClass(score: number | undefined): string {
    if (!score) return 'text-muted';
    if (score >= 80) return 'text-success';
    if (score >= 60) return 'text-warning';
    return 'text-danger';
  }

  getStatusBadgeClass(status: ApplicationStatus): string {
    switch (status) {
      case ApplicationStatus.New: return 'badge bg-info';
      case ApplicationStatus.InReview: return 'badge bg-warning';
      case ApplicationStatus.Shortlisted: return 'badge bg-primary';
      case ApplicationStatus.Interview: return 'badge bg-warning';
      case ApplicationStatus.Offered: return 'badge bg-success';
      case ApplicationStatus.Hired: return 'badge bg-success';
      case ApplicationStatus.Rejected: return 'badge bg-danger';
      case ApplicationStatus.Withdrawn: return 'badge bg-secondary';
      case ApplicationStatus.OnHold: return 'badge bg-secondary';
      default: return 'badge bg-secondary';
    }
  }

  getStatusText(status: ApplicationStatus): string {
    switch (status) {
      case ApplicationStatus.New: return 'New';
      case ApplicationStatus.InReview: return 'In Review';
      case ApplicationStatus.Shortlisted: return 'Shortlisted';
      case ApplicationStatus.Interview: return 'Interview';
      case ApplicationStatus.Offered: return 'Offered';
      case ApplicationStatus.Hired: return 'Hired';
      case ApplicationStatus.Rejected: return 'Rejected';
      case ApplicationStatus.Withdrawn: return 'Withdrawn';
      case ApplicationStatus.OnHold: return 'On Hold';
      default: return 'Unknown';
    }
  }

  getStageText(stage: PipelineStage): string {
    switch (stage) {
      case PipelineStage.Applied: return 'Applied';
      case PipelineStage.Screening: return 'Screening';
      case PipelineStage.PhoneScreen: return 'Phone Screen';
      case PipelineStage.FirstInterview: return 'First Interview';
      case PipelineStage.TechnicalAssessment: return 'Technical Assessment';
      case PipelineStage.FinalInterview: return 'Final Interview';
      case PipelineStage.ReferenceCheck: return 'Reference Check';
      case PipelineStage.Offer: return 'Offer';
      case PipelineStage.Hired: return 'Hired';
      case PipelineStage.Rejected: return 'Rejected';
      default: return 'Unknown';
    }
  }


  getCandidateStatusBadgeClass(): string {
    if (!this.candidate) return 'badge bg-secondary';
    switch (this.candidate.status) {
      case CandidateStatus.Active: return 'badge bg-success';
      case CandidateStatus.Inactive: return 'badge bg-secondary';
      case CandidateStatus.Blacklisted: return 'badge bg-danger';
      case CandidateStatus.Hired: return 'badge bg-primary';
      default: return 'badge bg-info';
    }
  }

  getCandidateStatusText(): string {
    if (!this.candidate) return 'Unknown';
    switch (this.candidate.status) {
      case CandidateStatus.Active: return 'Active';
      case CandidateStatus.Inactive: return 'Inactive';
      case CandidateStatus.Blacklisted: return 'Blacklisted';
      case CandidateStatus.Hired: return 'Hired';
      default: return 'Active';
    }
  }

  formatDate(date: any): string {
    if (!date) return '';
    const d = new Date(date);
    return d.toLocaleDateString('en-US', { year: 'numeric', month: 'short' });
  }

  downloadResume(): void {
    if (!this.candidate?.id) return;

    this.downloadingResume = true;
    const apiUrl = environment.apis.default.url;
    const url = `${apiUrl}/api/app/candidate/${this.candidate.id}/resume`;

    // Create a temporary anchor element to trigger download
    fetch(url, {
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('access_token') || ''}`
      }
    })
      .then(response => {
        if (!response.ok) {
          throw new Error('Failed to download resume');
        }
        return response.blob();
      })
      .then(blob => {
        // Extract filename from Content-Disposition header or use default
        const fileName = `${this.candidate?.firstName}_${this.candidate?.lastName}_Resume.pdf`;
        
        // Create download link
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = fileName;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        window.URL.revokeObjectURL(url);

        this.toaster.success('Resume downloaded successfully');
        this.downloadingResume = false;
      })
      .catch(error => {
        console.error('Error downloading resume:', error);
        this.toaster.error('Failed to download resume. The candidate may not have uploaded one.');
        this.downloadingResume = false;
      });
  }
}
