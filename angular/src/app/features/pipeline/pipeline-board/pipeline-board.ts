import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DragDropModule, CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { ApplicationService } from '@proxy/applications';
import { ApplicationListDto, MoveApplicationStageInput, GetApplicationListInput } from '@proxy/applications/dtos/models';
import { PipelineStage } from '@proxy/applications/pipeline-stage.enum';
import { JobService } from '@proxy/jobs';
import { JobListDto } from '@proxy/jobs/dtos/models';
import { ToasterService } from '@abp/ng.theme.shared';

interface StageColumn {
  stage: PipelineStage;
  label: string;
  applications: ApplicationListDto[];
}

@Component({
  selector: 'app-pipeline-board',
  imports: [CommonModule, RouterModule, FormsModule, DragDropModule],
  templateUrl: './pipeline-board.html',
  styleUrl: './pipeline-board.scss'
})
export class PipelineBoard implements OnInit {
  columns: StageColumn[] = [];
  jobs: JobListDto[] = [];
  selectedJobId: string | undefined;
  loading = false;
  moving = false;

  constructor(
    private applicationService: ApplicationService,
    private jobService: JobService,
    private route: ActivatedRoute,
    private toaster: ToasterService
  ) {
    this.initializeColumns();
  }

  ngOnInit(): void {
    // Check for jobId in query params
    this.route.queryParams.subscribe(params => {
      if (params['jobId']) {
        this.selectedJobId = params['jobId'];
      }
    });

    this.loadJobs();
    this.loadApplications();
  }

  initializeColumns(): void {
    this.columns = [
      { stage: PipelineStage.Applied, label: 'Applied', applications: [] },
      { stage: PipelineStage.Screening, label: 'Screening', applications: [] },
      { stage: PipelineStage.PhoneScreen, label: 'Phone Screen', applications: [] },
      { stage: PipelineStage.FirstInterview, label: 'First Interview', applications: [] },
      { stage: PipelineStage.TechnicalAssessment, label: 'Technical Assessment', applications: [] },
      { stage: PipelineStage.FinalInterview, label: 'Final Interview', applications: [] },
      { stage: PipelineStage.ReferenceCheck, label: 'Reference Check', applications: [] },
      { stage: PipelineStage.Offer, label: 'Offer', applications: [] },
      { stage: PipelineStage.Hired, label: 'Hired', applications: [] },
      { stage: PipelineStage.Rejected, label: 'Rejected', applications: [] }
    ];
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
    
    const input: GetApplicationListInput = {
      skipCount: 0,
      maxResultCount: 1000,
      sorting: 'appliedDate DESC'
    };

    if (this.selectedJobId) {
      input.jobId = this.selectedJobId;
    }

    this.applicationService.getList(input).subscribe({
      next: (result) => {
        const applications = result.items || [];
        this.groupApplicationsByStage(applications);
        this.loading = false;
      },
      error: (error) => {
        console.error('Failed to load applications:', error);
        this.toaster.error('Failed to load applications');
        this.loading = false;
      }
    });
  }

  groupApplicationsByStage(applications: ApplicationListDto[]): void {
    // Reset all columns
    this.columns.forEach(col => col.applications = []);

    // Group applications by stage
    applications.forEach(app => {
      const stage = app.stage !== undefined ? app.stage : PipelineStage.Applied;
      const column = this.columns.find(col => col.stage === stage);
      if (column) {
        column.applications.push(app);
      }
    });
  }

  onJobFilterChange(): void {
    this.loadApplications();
  }

  onDrop(event: CdkDragDrop<ApplicationListDto[]>): void {
    if (event.previousContainer === event.container) {
      // Reorder within same column
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      // Move to different column
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );

      // Get the application and new stage
      const application = event.container.data[event.currentIndex];
      const newStage = this.columns.find(col => col.applications === event.container.data)?.stage;

      if (application && newStage !== undefined) {
        this.moveApplicationToStage(application, newStage);
      }
    }
  }

  moveApplicationToStage(application: ApplicationListDto, newStage: PipelineStage): void {
    if (!application.id) return;

    this.moving = true;
    const input: MoveApplicationStageInput = {
      applicationId: application.id,
      newStage: newStage
    };

    this.applicationService.moveToStage(input).subscribe({
      next: (updatedApp) => {
        // Update the application in the column
        const appIndex = this.columns.find(col => col.stage === newStage)?.applications.findIndex(a => a.id === application.id);
        if (appIndex !== undefined && appIndex >= 0) {
          const column = this.columns.find(col => col.stage === newStage);
          if (column) {
            // Update the application data
            const index = column.applications.findIndex(a => a.id === application.id);
            if (index >= 0) {
              column.applications[index] = {
                ...column.applications[index],
                stage: updatedApp.stage,
                status: updatedApp.status
              };
            }
          }
        }
        this.moving = false;
        this.toaster.success('Application moved successfully');
      },
      error: (error) => {
        console.error('Failed to move application:', error);
        this.toaster.error('Failed to move application');
        this.moving = false;
        // Reload to restore state
        this.loadApplications();
      }
    });
  }

  getStatusBadgeClass(status: number | undefined): string {
    if (status === undefined) return 'badge bg-secondary';
    switch (status) {
      case 0: return 'badge bg-info'; // New
      case 1: return 'badge bg-warning'; // InReview
      case 2: return 'badge bg-primary'; // Shortlisted
      case 3: return 'badge bg-warning'; // Interview
      case 4: return 'badge bg-success'; // Offered
      case 5: return 'badge bg-success'; // Hired
      case 6: return 'badge bg-danger'; // Rejected
      case 7: return 'badge bg-secondary'; // Withdrawn
      case 8: return 'badge bg-secondary'; // OnHold
      default: return 'badge bg-secondary';
    }
  }

  getStatusText(status: number | undefined): string {
    if (status === undefined) return 'Unknown';
    switch (status) {
      case 0: return 'New';
      case 1: return 'In Review';
      case 2: return 'Shortlisted';
      case 3: return 'Interview';
      case 4: return 'Offered';
      case 5: return 'Hired';
      case 6: return 'Rejected';
      case 7: return 'Withdrawn';
      case 8: return 'On Hold';
      default: return 'Unknown';
    }
  }

  getScoreClass(score: number | undefined): string {
    if (!score) return 'text-muted';
    if (score >= 80) return 'text-success';
    if (score >= 60) return 'text-warning';
    return 'text-danger';
  }

  getTotalApplications(): number {
    return this.columns.reduce((sum, col) => sum + col.applications.length, 0);
  }
}
