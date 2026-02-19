import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { JobService } from '@proxy/jobs';
import { CreateUpdateJobDto } from '@proxy/jobs/dtos/models';
import { DepartmentService } from '../../../proxy/departments/department.service';
import { DepartmentDto } from '../../../proxy/departments/dtos/models';

@Component({
  selector: 'app-job-form',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './job-form.html',
  styleUrl: './job-form.scss'
})
export class JobForm implements OnInit {
  jobForm: FormGroup;
  loading = false;
  isEditMode = false;
  jobId: string | null = null;
  departments: DepartmentDto[] = [];
  loadingDepartments = false;

  employmentTypes = [
    { value: 0, label: 'Full-Time' },
    { value: 1, label: 'Part-Time' },
    { value: 2, label: 'Contract' },
    { value: 3, label: 'Internship' }
  ];

  experienceLevels = [
    { value: 0, label: 'Entry Level' },
    { value: 1, label: 'Junior' },
    { value: 2, label: 'Mid Level' },
    { value: 3, label: 'Senior' },
    { value: 4, label: 'Lead' },
    { value: 5, label: 'Executive' }
  ];

  constructor(
    private fb: FormBuilder,
    private jobService: JobService,
    private departmentService: DepartmentService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.jobForm = this.fb.group({
      title: ['', Validators.required],
      departmentId: ['', Validators.required],
      location: ['', Validators.required],
      isRemote: [false],
      employmentType: [0, Validators.required],
      experienceLevel: [0, Validators.required],
      description: ['', Validators.required],
      requirements: ['', Validators.required],
      responsibilities: [''],
      benefits: [''],
      salaryRangeMin: [null],
      salaryRangeMax: [null],
      applicationDeadline: [''],
      requiredSkills: [''],
      hiringManagerId: [''],
      hiringManagerName: [''],
      hiringManagerEmail: ['']
    });
  }

  ngOnInit(): void {
    this.loadDepartments();

    this.route.params.subscribe(params => {
      this.jobId = params['id'];
      this.isEditMode = !!this.jobId;

      if (this.isEditMode) {
        this.loadJob();
      }
    });
  }

  loadDepartments(): void {
    this.loadingDepartments = true;
    this.departmentService.getAllActive().subscribe({
      next: (departments) => {
        this.departments = departments;
        this.loadingDepartments = false;
      },
      error: (error) => {
        console.error('Failed to load departments:', error);
        this.loadingDepartments = false;
      }
    });
  }

  loadJob(): void {
    if (!this.jobId) return;

    this.loading = true;
    this.jobService.get(this.jobId).subscribe({
      next: (job) => {
        this.jobForm.patchValue({
          title: job.title,
          departmentId: job.departmentId,
          location: job.location,
          isRemote: job.isRemote,
          employmentType: job.employmentType,
          experienceLevel: job.experienceLevel,
          description: job.description,
          requirements: job.requirements,
          responsibilities: job.responsibilities,
          benefits: job.benefits || '',
          salaryRangeMin: job.minSalary,
          salaryRangeMax: job.maxSalary,
          applicationDeadline: job.closingDate ? new Date(job.closingDate).toISOString().split('T')[0] : '',
          requiredSkills: job.requiredSkills?.join(', ') || '',
          hiringManagerId: job.hiringManagerId,
          hiringManagerName: job.hiringManagerName,
          hiringManagerEmail: job.hiringManagerEmail
        });
        this.loading = false;
      },
      error: (error) => {
        console.error('Failed to load job:', error);
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.jobForm.invalid) {
      this.jobForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    const formValue = this.jobForm.value;

    const jobDto: CreateUpdateJobDto = {
      title: formValue.title,
      description: formValue.description,
      requirements: formValue.requirements,
      responsibilities: formValue.responsibilities,
      benefits: formValue.benefits,
      departmentId: formValue.departmentId,
      location: formValue.location,
      isRemote: formValue.isRemote,
      employmentType: formValue.employmentType,
      experienceLevel: formValue.experienceLevel,
      minSalary: formValue.salaryRangeMin,
      maxSalary: formValue.salaryRangeMax,
      currency: 'USD',
      requiredSkills: formValue.requiredSkills ? formValue.requiredSkills.split(',').map((s: string) => s.trim()) : [],
      preferredSkills: [],
      closingDate: formValue.applicationDeadline ? new Date(formValue.applicationDeadline).toISOString() : undefined,
      hiringManagerId: formValue.hiringManagerId,
      hiringManagerName: formValue.hiringManagerName,
      hiringManagerEmail: formValue.hiringManagerEmail
    };

    const request = this.isEditMode && this.jobId
      ? this.jobService.update(this.jobId, jobDto)
      : this.jobService.create(jobDto);

    request.subscribe({
      next: (job) => {
        this.loading = false;
        this.router.navigate(['/jobs', job.id]);
      },
      error: (error) => {
        console.error('Failed to save job:', error);
        this.loading = false;
      }
    });
  }

  cancel(): void {
    if (this.isEditMode && this.jobId) {
      this.router.navigate(['/jobs', this.jobId]);
    } else {
      this.router.navigate(['/jobs']);
    }
  }
}
