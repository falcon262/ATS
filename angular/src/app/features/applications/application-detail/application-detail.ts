import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ApplicationService } from '@proxy/applications';
import { ApplicationDto, MoveApplicationStageInput, RejectApplicationInput, MakeOfferInput, UpdateApplicationDto } from '@proxy/applications/dtos/models';
import { ApplicationStatus } from '@proxy/applications/application-status.enum';
import { PipelineStage } from '@proxy/applications/pipeline-stage.enum';
import { ToasterService } from '@abp/ng.theme.shared';
import { ConfirmationService } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-application-detail',
  imports: [CommonModule, RouterModule, FormsModule, ReactiveFormsModule],
  templateUrl: './application-detail.html',
  styleUrl: './application-detail.scss'
})
export class ApplicationDetail implements OnInit {
  application: ApplicationDto | null = null;
  loading = false;
  saving = false;

  // Modal states
  showStageModal = false;
  showRejectModal = false;
  showOfferModal = false;
  showAssignModal = false;
  showNotesModal = false;

  // Forms
  stageForm!: FormGroup;
  rejectForm!: FormGroup;
  offerForm!: FormGroup;
  assignForm!: FormGroup;
  notesForm!: FormGroup;

  // Options
  stageOptions = this.getStageOptions();
  statusOptions = this.getStatusOptions();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private applicationService: ApplicationService,
    private fb: FormBuilder,
    private toaster: ToasterService,
    private confirmation: ConfirmationService
  ) {
    this.initForms();
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];
      if (id) {
        this.loadApplication(id);
      }
    });
  }

  initForms(): void {
    this.stageForm = this.fb.group({
      newStage: [null, Validators.required],
      notes: ['']
    });

    this.rejectForm = this.fb.group({
      rejectionReason: ['', Validators.required],
      sendNotification: [true]
    });

    this.offerForm = this.fb.group({
      offeredSalary: [null, [Validators.required, Validators.min(0)]],
      offerExpiryDate: [null, Validators.required],
      offerDetails: ['']
    });

    this.assignForm = this.fb.group({
      reviewerId: ['', Validators.required],
      reviewerName: ['', Validators.required]
    });

    this.notesForm = this.fb.group({
      reviewNotes: [''],
      rating: [null, [Validators.min(1), Validators.max(5)]]
    });
  }

  loadApplication(id: string): void {
    this.loading = true;
    this.applicationService.get(id).subscribe({
      next: (app) => {
        this.application = app;
        this.loading = false;
        this.updateForms();
      },
      error: (error) => {
        console.error('Failed to load application:', error);
        this.toaster.error('Failed to load application');
        this.loading = false;
      }
    });
  }

  updateForms(): void {
    if (!this.application) return;

    this.notesForm.patchValue({
      reviewNotes: this.application.reviewNotes || '',
      rating: this.application.rating || null
    });
  }

  // Status and Stage helpers
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

  // Actions
  openStageModal(): void {
    if (this.application) {
      this.stageForm.patchValue({ newStage: this.application.stage });
    }
    this.showStageModal = true;
  }

  openRejectModal(): void {
    this.rejectForm.reset({ sendNotification: true });
    this.showRejectModal = true;
  }

  openOfferModal(): void {
    this.offerForm.reset();
    this.showOfferModal = true;
  }

  openAssignModal(): void {
    this.assignForm.reset();
    this.showAssignModal = true;
  }

  openNotesModal(): void {
    this.updateForms();
    this.showNotesModal = true;
  }

  closeModals(): void {
    this.showStageModal = false;
    this.showRejectModal = false;
    this.showOfferModal = false;
    this.showAssignModal = false;
    this.showNotesModal = false;
  }

  moveToStage(): void {
    if (!this.application || !this.stageForm.valid) return;

    this.saving = true;
    const input: MoveApplicationStageInput = {
      applicationId: this.application.id,
      newStage: this.stageForm.value.newStage,
      notes: this.stageForm.value.notes || undefined
    };

    this.applicationService.moveToStage(input).subscribe({
      next: (app) => {
        this.application = app;
        this.saving = false;
        this.closeModals();
        this.toaster.success('Application stage updated successfully');
      },
      error: (error) => {
        console.error('Failed to move stage:', error);
        this.toaster.error('Failed to update stage');
        this.saving = false;
      }
    });
  }

  rejectApplication(): void {
    if (!this.application || !this.rejectForm.valid) return;

    this.saving = true;
    const input: RejectApplicationInput = {
      applicationId: this.application.id,
      rejectionReason: this.rejectForm.value.rejectionReason,
      sendNotification: this.rejectForm.value.sendNotification
    };

    this.applicationService.reject(input).subscribe({
      next: (app) => {
        this.application = app;
        this.saving = false;
        this.closeModals();
        this.toaster.success('Application rejected');
      },
      error: (error) => {
        console.error('Failed to reject application:', error);
        this.toaster.error('Failed to reject application');
        this.saving = false;
      }
    });
  }

  makeOffer(): void {
    if (!this.application || !this.offerForm.valid) return;

    this.saving = true;
    const input: MakeOfferInput = {
      applicationId: this.application.id,
      offeredSalary: this.offerForm.value.offeredSalary,
      offerExpiryDate: this.offerForm.value.offerExpiryDate,
      offerDetails: this.offerForm.value.offerDetails || undefined
    };

    this.applicationService.makeOffer(input).subscribe({
      next: (app) => {
        this.application = app;
        this.saving = false;
        this.closeModals();
        this.toaster.success('Offer created successfully');
      },
      error: (error) => {
        console.error('Failed to make offer:', error);
        this.toaster.error('Failed to create offer');
        this.saving = false;
      }
    });
  }

  assignReviewer(): void {
    if (!this.application || !this.assignForm.valid) return;

    this.saving = true;
    this.applicationService.assignReviewer(
      this.application.id,
      this.assignForm.value.reviewerId,
      this.assignForm.value.reviewerName
    ).subscribe({
      next: (app) => {
        this.application = app;
        this.saving = false;
        this.closeModals();
        this.toaster.success('Reviewer assigned successfully');
      },
      error: (error) => {
        console.error('Failed to assign reviewer:', error);
        this.toaster.error('Failed to assign reviewer');
        this.saving = false;
      }
    });
  }

  saveNotes(): void {
    if (!this.application || !this.notesForm.valid) return;

    this.saving = true;
    const input: UpdateApplicationDto = {
      reviewNotes: this.notesForm.value.reviewNotes || undefined,
      rating: this.notesForm.value.rating || undefined
    };

    this.applicationService.update(this.application.id, input).subscribe({
      next: (app) => {
        this.application = app;
        this.saving = false;
        this.closeModals();
        this.toaster.success('Notes saved successfully');
      },
      error: (error) => {
        console.error('Failed to save notes:', error);
        this.toaster.error('Failed to save notes');
        this.saving = false;
      }
    });
  }

  updateStatus(status: ApplicationStatus): void {
    if (!this.application) return;

    this.confirmation.warn(
      `Are you sure you want to change the status to "${this.getStatusText(status)}"?`,
      'Change Application Status'
    ).subscribe((result) => {
        if (result) {
          this.saving = true;
          const input: UpdateApplicationDto = { status };
          this.applicationService.update(this.application!.id, input).subscribe({
            next: (app) => {
              this.application = app;
              this.saving = false;
              this.toaster.success('Status updated successfully');
            },
            error: (error) => {
              console.error('Failed to update status:', error);
              this.toaster.error('Failed to update status');
              this.saving = false;
            }
          });
        }
      });
  }
}

