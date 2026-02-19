import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { PublicJobService } from '../../../proxy/jobs/public-job.service';
import { PublicJobDto } from '../../../proxy/jobs/public/models';
import { ApplicationFormComponent } from '../application-form/application-form.component';
import { EmploymentTypePipe } from '../../../shared/pipes/employment-type.pipe';
import { ExperienceLevelPipe } from '../../../shared/pipes/experience-level.pipe';

@Component({
  selector: 'app-public-job-detail',
  standalone: true,
  imports: [CommonModule, RouterModule, ApplicationFormComponent, EmploymentTypePipe, ExperienceLevelPipe],
  template: `
    <div class="container py-5" *ngIf="job">
      <div class="row">
        <div class="col-lg-8">
          <!-- Job Header -->
          <div class="job-header mb-4">
            <h1 class="display-4">{{ job.title }}</h1>
            <div class="job-meta mt-3">
              <span class="badge bg-primary me-2">{{ job.employmentType | employmentTypeText }}</span>
              <span class="badge bg-info me-2">{{ job.experienceLevel | experienceLevelText }}</span>
              <span class="badge bg-secondary me-2" *ngIf="job.isRemote">Remote</span>
              <span class="badge bg-secondary" *ngIf="job.location">{{ job.location }}</span>
            </div>
            <p class="text-muted mt-2">
              <i class="fas fa-building me-2"></i>{{ job.departmentName }}
              <i class="fas fa-calendar ms-3 me-2"></i>Posted {{ job.postedDate | date }}
            </p>
          </div>

          <!-- Job Description -->
          <div class="job-section mb-4">
            <h3>About the Role</h3>
            <p class="job-description">{{ job.description }}</p>
          </div>

          <!-- Requirements -->
          <div class="job-section mb-4" *ngIf="job.requirements">
            <h3>Requirements</h3>
            <p class="whitespace-pre-line">{{ job.requirements }}</p>
          </div>

          <!-- Responsibilities -->
          <div class="job-section mb-4" *ngIf="job.responsibilities">
            <h3>Responsibilities</h3>
            <p class="whitespace-pre-line">{{ job.responsibilities }}</p>
          </div>

          <!-- Skills -->
          <div class="job-section mb-4" *ngIf="job.requiredSkills?.length">
            <h3>Required Skills</h3>
            <div class="skills-container">
              <span class="badge bg-primary me-2 mb-2" *ngFor="let skill of job.requiredSkills">
                {{ skill }}
              </span>
            </div>
          </div>

          <div class="job-section mb-4" *ngIf="job.preferredSkills?.length">
            <h3>Preferred Skills</h3>
            <div class="skills-container">
              <span class="badge bg-secondary me-2 mb-2" *ngFor="let skill of job.preferredSkills">
                {{ skill }}
              </span>
            </div>
          </div>

          <!-- Salary Range -->
          <div class="job-section mb-4" *ngIf="job.minSalary || job.maxSalary">
            <h3>Compensation</h3>
            <p class="h5">
              <span *ngIf="job.minSalary">{{ job.minSalary | currency: job.currency }}</span>
              <span *ngIf="job.minSalary && job.maxSalary"> - </span>
              <span *ngIf="job.maxSalary">{{ job.maxSalary | currency: job.currency }}</span>
              <span *ngIf="job.minSalary || job.maxSalary"> per year</span>
            </p>
          </div>

          <!-- Application Form -->
          <div class="application-section mt-5" id="apply">
            <h2 class="mb-4">Apply for this Position</h2>
            <app-application-form
              [jobId]="job.id"
              (applicationSubmitted)="onApplicationSubmitted($event)">
            </app-application-form>
          </div>
        </div>

        <!-- Sidebar -->
        <div class="col-lg-4">
          <div class="sticky-top" style="top: 20px;">
            <div class="card shadow-sm">
              <div class="card-body">
                <h5 class="card-title">Job Details</h5>
                <hr>
                <div class="mb-3">
                  <small class="text-muted">Employment Type</small>
                  <p class="mb-0">{{ job.employmentType | employmentTypeText }}</p>
                </div>
                <div class="mb-3">
                  <small class="text-muted">Experience Level</small>
                  <p class="mb-0">{{ job.experienceLevel | experienceLevelText }}</p>
                </div>
                <div class="mb-3" *ngIf="job.location">
                  <small class="text-muted">Location</small>
                  <p class="mb-0">{{ job.location }}</p>
                </div>
                <div class="mb-3" *ngIf="job.isRemote">
                  <small class="text-muted">Remote Work</small>
                  <p class="mb-0">✓ Remote Available</p>
                </div>
                <div class="mb-3" *ngIf="job.closingDate">
                  <small class="text-muted">Application Deadline</small>
                  <p class="mb-0">{{ job.closingDate | date }}</p>
                </div>
                <a href="#apply" class="btn btn-primary w-100 mt-3">
                  <i class="fas fa-paper-plane me-2"></i>Apply Now
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Loading State -->
    <div class="container py-5 text-center" *ngIf="!job && loading">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Loading...</span>
      </div>
      <p class="mt-3">Loading job details...</p>
    </div>

    <!-- Error State -->
    <div class="container py-5 text-center" *ngIf="!job && !loading">
      <div class="alert alert-danger">
        <i class="fas fa-exclamation-triangle me-2"></i>
        Job not found or no longer available.
      </div>
      <a routerLink="/" class="btn btn-primary">
        <i class="fas fa-home me-2"></i>Back to Home
      </a>
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

    .job-header {
      border-bottom: 2px solid #f0f0f0;
      padding-bottom: 1.5rem;
    }

    .job-section {
      h3 {
        color: #333;
        font-size: 1.5rem;
        margin-bottom: 1rem;
        font-weight: 600;
      }
    }

    .job-description {
      line-height: 1.8;
      color: #555;
    }

    .whitespace-pre-line {
      white-space: pre-line;
      line-height: 1.8;
      color: #555;
    }

    .skills-container {
      .badge {
        font-size: 0.9rem;
        padding: 0.5rem 1rem;
      }
    }

    .application-section {
      border-top: 2px solid #f0f0f0;
      padding-top: 2rem;
    }
  `]
})
export class PublicJobDetailComponent implements OnInit {
  job: PublicJobDto | null = null;
  loading = true;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private publicJobService: PublicJobService
  ) {}

  ngOnInit(): void {
    const slug = this.route.snapshot.paramMap.get('slug');
    if (slug) {
      this.loadJob(slug);
    } else {
      this.loading = false;
    }
  }

  loadJob(slug: string): void {
    this.loading = true;
    this.publicJobService.getBySlug(slug).subscribe({
      next: (job) => {
        this.job = job;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading job:', error);
        this.loading = false;
      }
    });
  }

  onApplicationSubmitted(applicationId: string): void {
    this.router.navigate(['/apply/success'], {
      queryParams: { applicationId }
    });
  }
}

