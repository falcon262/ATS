import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ApplicationService } from '@proxy/applications';
import { ApplicationListDto, GetApplicationListInput } from '@proxy/applications/dtos/models';
import { ApplicationStatus } from '@proxy/applications/application-status.enum';
import { PipelineStage } from '@proxy/applications/pipeline-stage.enum';
import { JobService } from '@proxy/jobs';
import { JobListDto } from '@proxy/jobs/dtos/models';
import { ToasterService } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-application-list',
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './application-list.html',
  styleUrl: './application-list.scss'
})
export class ApplicationList implements OnInit {
  applications: ApplicationListDto[] = [];
  totalCount = 0;
  loading = false;

  // Filters
  filters: GetApplicationListInput = {
    skipCount: 0,
    maxResultCount: 20,
    sorting: 'appliedDate DESC'
  };

  // Filter options
  jobs: JobListDto[] = [];
  selectedJobId: string | undefined;
  selectedStatus: ApplicationStatus | undefined;
  selectedStage: PipelineStage | undefined;
  minAIScore: number | undefined;
  searchText = '';
  filtersExpanded = false;

  // Pagination
  currentPage = 1;
  pageSize = 20;

  // Status and Stage options
  statusOptions = Object.values(ApplicationStatus).filter(v => typeof v === 'number') as ApplicationStatus[];
  stageOptions = Object.values(PipelineStage).filter(v => typeof v === 'number') as PipelineStage[];

  constructor(
    private applicationService: ApplicationService,
    private jobService: JobService,
    private toaster: ToasterService
  ) {}

  ngOnInit(): void {
    this.loadJobs();
    this.loadApplications();
  }

  loadJobs(): void {
    this.jobService.getList({
      maxResultCount: 1000,
      skipCount: 0,
      sorting: 'title ASC'
    }).subscribe({
      next: (result) => {
        this.jobs = result.items || [];
      },
      error: (error) => {
        console.error('Failed to load jobs:', error);
      }
    });
  }

  loadApplications(): void {
    this.loading = true;
    
    // Build filter input
    const input: GetApplicationListInput = {
      skipCount: (this.currentPage - 1) * this.pageSize,
      maxResultCount: this.pageSize,
      sorting: this.filters.sorting || 'appliedDate DESC'
    };

    if (this.selectedJobId) {
      input.jobId = this.selectedJobId;
    }
    if (this.selectedStatus !== undefined) {
      input.status = this.selectedStatus;
    }
    if (this.selectedStage !== undefined) {
      input.stage = this.selectedStage;
    }
    if (this.minAIScore !== undefined && this.minAIScore > 0) {
      input.minAIScore = this.minAIScore;
    }

    this.applicationService.getList(input).subscribe({
      next: (result) => {
        this.applications = result.items || [];
        this.totalCount = result.totalCount || 0;
        this.loading = false;
      },
      error: (error) => {
        console.error('Failed to load applications:', error);
        this.toaster.error('Failed to load applications');
        this.loading = false;
      }
    });
  }

  onFilterChange(): void {
    this.currentPage = 1;
    this.loadApplications();
  }

  onSearch(): void {
    // Note: Search would need backend support for candidate name/job title search
    // For now, we'll filter by job if search matches a job title
    const matchingJob = this.jobs.find(j => 
      j.title?.toLowerCase().includes(this.searchText.toLowerCase())
    );
    if (matchingJob) {
      this.selectedJobId = matchingJob.id;
    }
    this.onFilterChange();
  }

  clearFilters(): void {
    this.selectedJobId = undefined;
    this.selectedStatus = undefined;
    this.selectedStage = undefined;
    this.minAIScore = undefined;
    this.searchText = '';
    this.onFilterChange();
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadApplications();
  }

  get totalPages(): number {
    return Math.ceil(this.totalCount / this.pageSize);
  }

  get pages(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  getStatusBadgeClass(status: ApplicationStatus | undefined): string {
    if (status === undefined) return 'badge bg-secondary';
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

  getStatusText(status: ApplicationStatus | undefined): string {
    if (status === undefined) return 'Unknown';
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

  getStageText(stage: PipelineStage | undefined): string {
    if (stage === undefined) return 'Unknown';
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

  getScoreClass(score: number | undefined): string {
    if (!score) return 'text-muted';
    if (score >= 80) return 'text-success';
    if (score >= 60) return 'text-warning';
    return 'text-danger';
  }

  getStatusOptions(): { value: number; label: string }[] {
    return [
      { value: ApplicationStatus.New, label: 'New' },
      { value: ApplicationStatus.InReview, label: 'In Review' },
      { value: ApplicationStatus.Shortlisted, label: 'Shortlisted' },
      { value: ApplicationStatus.Interview, label: 'Interview' },
      { value: ApplicationStatus.Offered, label: 'Offered' },
      { value: ApplicationStatus.Hired, label: 'Hired' },
      { value: ApplicationStatus.Rejected, label: 'Rejected' },
      { value: ApplicationStatus.Withdrawn, label: 'Withdrawn' },
      { value: ApplicationStatus.OnHold, label: 'On Hold' }
    ];
  }

  getStageOptions(): { value: number; label: string }[] {
    return [
      { value: PipelineStage.Applied, label: 'Applied' },
      { value: PipelineStage.Screening, label: 'Screening' },
      { value: PipelineStage.PhoneScreen, label: 'Phone Screen' },
      { value: PipelineStage.FirstInterview, label: 'First Interview' },
      { value: PipelineStage.TechnicalAssessment, label: 'Technical Assessment' },
      { value: PipelineStage.FinalInterview, label: 'Final Interview' },
      { value: PipelineStage.ReferenceCheck, label: 'Reference Check' },
      { value: PipelineStage.Offer, label: 'Offer' },
      { value: PipelineStage.Hired, label: 'Hired' },
      { value: PipelineStage.Rejected, label: 'Rejected' }
    ];
  }

  // Expose Math to template
  Math = Math;
}
