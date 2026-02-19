import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PublicJobService } from '../../../proxy/jobs/public-job.service';
import { ToasterService } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-application-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <form [formGroup]="applicationForm" (ngSubmit)="onSubmit()" class="application-form">
      <!-- Personal Information -->
      <div class="form-section">
        <h4 class="mb-3">Personal Information</h4>
        <div class="row">
          <div class="col-md-6 mb-3">
            <label for="firstName" class="form-label">First Name <span class="text-danger">*</span></label>
            <input type="text" class="form-control" id="firstName" formControlName="firstName"
              [class.is-invalid]="isFieldInvalid('firstName')">
            <div class="invalid-feedback">First name is required</div>
          </div>
          <div class="col-md-6 mb-3">
            <label for="lastName" class="form-label">Last Name <span class="text-danger">*</span></label>
            <input type="text" class="form-control" id="lastName" formControlName="lastName"
              [class.is-invalid]="isFieldInvalid('lastName')">
            <div class="invalid-feedback">Last name is required</div>
          </div>
        </div>
        <div class="row">
          <div class="col-md-6 mb-3">
            <label for="email" class="form-label">Email <span class="text-danger">*</span></label>
            <input type="email" class="form-control" id="email" formControlName="email"
              [class.is-invalid]="isFieldInvalid('email')">
            <div class="invalid-feedback">Valid email is required</div>
          </div>
          <div class="col-md-6 mb-3">
            <label for="phone" class="form-label">Phone</label>
            <input type="tel" class="form-control" id="phone" formControlName="phone">
          </div>
        </div>
      </div>

      <!-- Current Position -->
      <div class="form-section">
        <h4 class="mb-3">Current Position</h4>
        <div class="row">
          <div class="col-md-6 mb-3">
            <label for="currentJobTitle" class="form-label">Job Title</label>
            <input type="text" class="form-control" id="currentJobTitle" formControlName="currentJobTitle">
          </div>
          <div class="col-md-6 mb-3">
            <label for="currentCompany" class="form-label">Company</label>
            <input type="text" class="form-control" id="currentCompany" formControlName="currentCompany">
          </div>
        </div>
        <div class="mb-3">
          <label for="yearsOfExperience" class="form-label">Years of Experience <span class="text-danger">*</span></label>
          <input type="number" class="form-control" id="yearsOfExperience" formControlName="yearsOfExperience"
            min="0" max="50" [class.is-invalid]="isFieldInvalid('yearsOfExperience')">
          <div class="invalid-feedback">Years of experience is required</div>
        </div>
      </div>

      <!-- Location -->
      <div class="form-section">
        <h4 class="mb-3">Location</h4>
        <div class="row">
          <div class="col-md-4 mb-3">
            <label for="city" class="form-label">City</label>
            <input type="text" class="form-control" id="city" formControlName="city">
          </div>
          <div class="col-md-4 mb-3">
            <label for="state" class="form-label">State/Province</label>
            <input type="text" class="form-control" id="state" formControlName="state">
          </div>
          <div class="col-md-4 mb-3">
            <label for="country" class="form-label">Country</label>
            <input type="text" class="form-control" id="country" formControlName="country">
          </div>
        </div>
      </div>

      <!-- Professional Links -->
      <div class="form-section">
        <h4 class="mb-3">Professional Links</h4>
        <div class="row">
          <div class="col-md-4 mb-3">
            <label for="linkedInUrl" class="form-label">LinkedIn URL</label>
            <input type="url" class="form-control" id="linkedInUrl" formControlName="linkedInUrl"
              placeholder="https://linkedin.com/in/...">
          </div>
          <div class="col-md-4 mb-3">
            <label for="gitHubUrl" class="form-label">GitHub URL</label>
            <input type="url" class="form-control" id="gitHubUrl" formControlName="gitHubUrl"
              placeholder="https://github.com/...">
          </div>
          <div class="col-md-4 mb-3">
            <label for="portfolioUrl" class="form-label">Portfolio URL</label>
            <input type="url" class="form-control" id="portfolioUrl" formControlName="portfolioUrl"
              placeholder="https://...">
          </div>
        </div>
      </div>

      <!-- Skills, Education, Experience -->
      <div class="form-section">
        <h4 class="mb-3">Background</h4>
        <div class="mb-3">
          <label for="skills" class="form-label">Skills (comma-separated)</label>
          <input type="text" class="form-control" id="skills" formControlName="skillsInput"
            placeholder="e.g., JavaScript, React, Node.js">
          <small class="form-text text-muted">Enter your skills separated by commas</small>
        </div>
        <div class="mb-3">
          <label for="educationSummary" class="form-label">Education Summary</label>
          <textarea class="form-control" id="educationSummary" formControlName="educationSummary"
            rows="2" placeholder="e.g., BS in Computer Science, University of XYZ (2018-2022)"></textarea>
        </div>
        <div class="mb-3">
          <label for="experienceSummary" class="form-label">Experience Summary</label>
          <textarea class="form-control" id="experienceSummary" formControlName="experienceSummary"
            rows="3" placeholder="Brief summary of your relevant work experience"></textarea>
        </div>
      </div>

      <!-- Cover Letter -->
      <div class="form-section">
        <h4 class="mb-3">Cover Letter</h4>
        <div class="mb-3">
          <label for="coverLetter" class="form-label">Why are you interested in this position?</label>
          <textarea class="form-control" id="coverLetter" formControlName="coverLetter"
            rows="5" placeholder="Tell us why you're a great fit for this role..."></textarea>
        </div>
      </div>

      <!-- Resume Upload -->
      <div class="form-section">
        <h4 class="mb-3">Resume/CV</h4>
        <div class="mb-3">
          <label for="resume" class="form-label">Upload Resume (PDF, DOC, DOCX - Max 5MB)</label>
          <input type="file" class="form-control" id="resume"
            (change)="onFileSelected($event)"
            accept=".pdf,.doc,.docx"
            [class.is-invalid]="fileError">
          <div class="invalid-feedback" *ngIf="fileError">{{ fileError }}</div>
          <div class="mt-2" *ngIf="selectedFileName">
            <small class="text-success">
              <i class="fas fa-check-circle me-1"></i>Selected: {{ selectedFileName }}
            </small>
          </div>
        </div>
      </div>

      <!-- Consent -->
      <div class="form-section">
        <div class="form-check mb-3">
          <input class="form-check-input" type="checkbox" id="consentToProcess"
            formControlName="consentToProcess"
            [class.is-invalid]="isFieldInvalid('consentToProcess')">
          <label class="form-check-label" for="consentToProcess">
            I consent to the processing of my personal data for recruitment purposes <span class="text-danger">*</span>
          </label>
          <div class="invalid-feedback">You must consent to data processing</div>
        </div>
      </div>

      <!-- Submit Button -->
      <div class="d-grid gap-2">
        <button type="submit" class="btn btn-primary btn-lg" [disabled]="submitting || applicationForm.invalid">
          <span *ngIf="!submitting">
            <i class="fas fa-paper-plane me-2"></i>Submit Application
          </span>
          <span *ngIf="submitting">
            <span class="spinner-border spinner-border-sm me-2" role="status"></span>
            Submitting...
          </span>
        </button>
      </div>

      <p class="text-muted text-center mt-3 small">
        <i class="fas fa-lock me-1"></i>Your information is secure and will only be used for recruitment purposes
      </p>
    </form>
  `,
  styles: [`
    .form-section {
      background: #f8f9fa;
      padding: 1.5rem;
      border-radius: 8px;
      margin-bottom: 1.5rem;

      h4 {
        color: #333;
        font-weight: 600;
      }
    }

    .application-form {
      max-width: 900px;
    }

    .form-label {
      font-weight: 500;
      color: #555;
    }

    .btn-primary {
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      border: none;
      padding: 1rem;
      font-size: 1.1rem;
      font-weight: 600;

      &:hover {
        transform: translateY(-2px);
        box-shadow: 0 4px 12px rgba(102, 126, 234, 0.4);
      }
    }
  `]
})
export class ApplicationFormComponent implements OnInit {
  @Input() jobId!: string;
  @Output() applicationSubmitted = new EventEmitter<string>();

  applicationForm!: FormGroup;
  submitting = false;
  selectedFileName = '';
  fileError = '';

  private resumeFileContent: string | null = null;
  private resumeFileName: string | null = null;

  constructor(
    private fb: FormBuilder,
    private publicJobService: PublicJobService,
    private toaster: ToasterService
  ) {}

  ngOnInit(): void {
    this.applicationForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: [''],
      currentJobTitle: [''],
      currentCompany: [''],
      yearsOfExperience: [0, [Validators.required, Validators.min(0)]],
      city: [''],
      state: [''],
      country: [''],
      linkedInUrl: [''],
      gitHubUrl: [''],
      portfolioUrl: [''],
      skillsInput: [''],
      educationSummary: [''],
      experienceSummary: [''],
      coverLetter: [''],
      consentToProcess: [false, Validators.requiredTrue]
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.applicationForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      // Validate file size (5MB max)
      if (file.size > 5 * 1024 * 1024) {
        this.fileError = 'File size must not exceed 5MB';
        this.selectedFileName = '';
        this.resumeFileContent = null;
        event.target.value = '';
        return;
      }

      // Validate file type
      const validTypes = ['application/pdf', 'application/msword',
        'application/vnd.openxmlformats-officedocument.wordprocessingml.document'];
      if (!validTypes.includes(file.type)) {
        this.fileError = 'Only PDF, DOC, and DOCX files are allowed';
        this.selectedFileName = '';
        this.resumeFileContent = null;
        event.target.value = '';
        return;
      }

      this.fileError = '';
      this.selectedFileName = file.name;
      this.resumeFileName = file.name;

      // Convert to base64
      const reader = new FileReader();
      reader.onload = () => {
        const base64 = reader.result as string;
        // Remove data URL prefix (data:application/pdf;base64,)
        this.resumeFileContent = base64.split(',')[1];
      };
      reader.readAsDataURL(file);
    }
  }

  onSubmit(): void {
    if (this.applicationForm.invalid) {
      Object.keys(this.applicationForm.controls).forEach(key => {
        this.applicationForm.get(key)?.markAsTouched();
      });
      this.toaster.warn('Please fill in all required fields');
      return;
    }

    this.submitting = true;
    const formValue = this.applicationForm.value;

    // Parse skills from comma-separated string
    const skills = formValue.skillsInput
      ? formValue.skillsInput.split(',').map((s: string) => s.trim()).filter((s: string) => s)
      : [];

    const applicationData = {
      jobId: this.jobId,
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      email: formValue.email,
      phone: formValue.phone || undefined,
      currentJobTitle: formValue.currentJobTitle || undefined,
      currentCompany: formValue.currentCompany || undefined,
      yearsOfExperience: formValue.yearsOfExperience,
      city: formValue.city || undefined,
      state: formValue.state || undefined,
      country: formValue.country || undefined,
      linkedInUrl: formValue.linkedInUrl || undefined,
      gitHubUrl: formValue.gitHubUrl || undefined,
      portfolioUrl: formValue.portfolioUrl || undefined,
      coverLetter: formValue.coverLetter || undefined,
      educationSummary: formValue.educationSummary || undefined,
      experienceSummary: formValue.experienceSummary || undefined,
      skills: skills,
      resumeFileName: this.resumeFileName || undefined,
      resumeContentBase64: this.resumeFileContent || undefined,
      consentToProcess: formValue.consentToProcess
    };

    this.publicJobService.submitApplication(applicationData).subscribe({
      next: (applicationId) => {
        this.submitting = false;
        this.toaster.success('Application submitted successfully!');
        // Remove quotes from the GUID string returned by the API
        const cleanId = applicationId.replace(/"/g, '');
        this.applicationSubmitted.emit(cleanId);
      },
      error: (error) => {
        console.error('Error submitting application:', error);
        this.toaster.error(
          error.error?.error?.message || 'Failed to submit application. Please try again.'
        );
        this.submitting = false;
      }
    });
  }
}

