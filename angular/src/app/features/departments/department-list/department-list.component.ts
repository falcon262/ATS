import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DepartmentService } from '../../../proxy/departments/department.service';
import { DepartmentDto, CreateUpdateDepartmentDto } from '../../../proxy/departments/dtos/models';
import { ToasterService } from '@abp/ng.theme.shared';
import { ConfirmationService } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-department-list',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  template: `
    <div class="container-fluid py-4">
      <!-- Header -->
      <div class="row mb-4">
        <div class="col">
          <h1 class="h2 mb-3">Departments</h1>
          <p class="text-muted">Manage organizational departments</p>
        </div>
        <div class="col-auto">
          <button class="btn btn-primary" (click)="openCreateModal()">
            <i class="fas fa-plus me-2"></i> Add Department
          </button>
        </div>
      </div>

      <!-- Loading State -->
      <div *ngIf="loading" class="text-center py-5">
        <div class="spinner-border text-primary" role="status">
          <span class="visually-hidden">Loading...</span>
        </div>
      </div>

      <!-- Departments List -->
      <div *ngIf="!loading && departments.length > 0" class="row">
        <div class="col-12">
          <div class="card shadow-sm">
            <div class="card-body">
              <div class="table-responsive">
                <table class="table table-hover">
                  <thead>
                    <tr>
                      <th>Name</th>
                      <th>Code</th>
                      <th>Description</th>
                      <th>Head</th>
                      <th>Jobs</th>
                      <th>Status</th>
                      <th>Actions</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr *ngFor="let dept of departments">
                      <td>
                        <strong>{{ dept.name }}</strong>
                      </td>
                      <td>
                        <span class="badge bg-secondary">{{ dept.code || 'N/A' }}</span>
                      </td>
                      <td>{{ dept.description || '-' }}</td>
                      <td>
                        <span *ngIf="dept.headName">{{ dept.headName }}</span>
                        <span *ngIf="!dept.headName" class="text-muted">Not assigned</span>
                      </td>
                      <td>
                        <span class="badge bg-info">{{ dept.jobCount || 0 }} jobs</span>
                      </td>
                      <td>
                        <span class="badge" [ngClass]="dept.isActive ? 'bg-success' : 'bg-danger'">
                          {{ dept.isActive ? 'Active' : 'Inactive' }}
                        </span>
                      </td>
                      <td>
                        <button class="btn btn-sm btn-outline-primary me-1" (click)="openEditModal(dept)">
                          <i class="fas fa-edit"></i>
                        </button>
                        <button class="btn btn-sm btn-outline-danger" (click)="deleteDepartment(dept)">
                          <i class="fas fa-trash"></i>
                        </button>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Empty State -->
      <div *ngIf="!loading && departments.length === 0" class="empty-state text-center py-5">
        <i class="fas fa-sitemap fa-4x text-muted mb-3"></i>
        <h3>No Departments Found</h3>
        <p class="text-muted">Get started by adding your first department.</p>
        <button class="btn btn-primary mt-3" (click)="openCreateModal()">
          <i class="fas fa-plus me-2"></i> Add Department
        </button>
      </div>
    </div>

    <!-- Create/Edit Modal -->
    <div class="modal fade" [class.show]="isModalOpen" [style.display]="isModalOpen ? 'block' : 'none'" tabindex="-1">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">{{ isEditMode ? 'Edit Department' : 'New Department' }}</h5>
            <button type="button" class="btn-close" (click)="closeModal()"></button>
          </div>
          <form [formGroup]="departmentForm" (ngSubmit)="saveDepartment()">
            <div class="modal-body">
              <div class="mb-3">
                <label class="form-label required">Name</label>
                <input type="text" class="form-control" formControlName="name"
                  [class.is-invalid]="isFieldInvalid('name')">
                <div class="invalid-feedback">Department name is required</div>
              </div>

              <div class="mb-3">
                <label class="form-label">Code</label>
                <input type="text" class="form-control" formControlName="code"
                  placeholder="e.g., ENG" maxlength="50">
              </div>

              <div class="mb-3">
                <label class="form-label">Description</label>
                <textarea class="form-control" formControlName="description"
                  rows="3" maxlength="500"></textarea>
              </div>

              <div class="mb-3">
                <div class="form-check">
                  <input class="form-check-input" type="checkbox" formControlName="isActive" id="isActive">
                  <label class="form-check-label" for="isActive">
                    Active
                  </label>
                </div>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" (click)="closeModal()">Cancel</button>
              <button type="submit" class="btn btn-primary" [disabled]="departmentForm.invalid || submitting">
                <span *ngIf="!submitting">{{ isEditMode ? 'Update' : 'Create' }}</span>
                <span *ngIf="submitting">
                  <span class="spinner-border spinner-border-sm me-1"></span>Saving...
                </span>
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>

    <!-- Modal Backdrop -->
    <div class="modal-backdrop fade" [class.show]="isModalOpen" *ngIf="isModalOpen"></div>
  `,
  styles: [`
    .card {
      border-radius: 12px;
    }

    .table th {
      font-weight: 600;
      color: #495057;
      border-bottom: 2px solid #dee2e6;
    }

    .table tbody tr {
      transition: background-color 0.2s;

      &:hover {
        background-color: #f8f9fa;
      }
    }

    .empty-state {
      padding: 3rem;
    }

    .modal.show {
      background-color: rgba(0, 0, 0, 0.5);
    }

    .modal-backdrop.show {
      opacity: 0.5;
    }

    .form-label.required::after {
      content: ' *';
      color: #dc3545;
    }
  `]
})
export class DepartmentListComponent implements OnInit {
  departments: DepartmentDto[] = [];
  loading = false;
  isModalOpen = false;
  isEditMode = false;
  submitting = false;
  selectedDepartmentId: string | null = null;
  departmentForm!: FormGroup;

  constructor(
    private departmentService: DepartmentService,
    private fb: FormBuilder,
    private toaster: ToasterService,
    private confirmation: ConfirmationService
  ) {
    this.initForm();
  }

  ngOnInit(): void {
    this.loadDepartments();
  }

  initForm(): void {
    this.departmentForm = this.fb.group({
      name: ['', Validators.required],
      code: [''],
      description: [''],
      isActive: [true]
    });
  }

  loadDepartments(): void {
    this.loading = true;
    this.departmentService.getAllActive().subscribe({
      next: (departments) => {
        this.departments = departments;
        this.loading = false;
      },
      error: (error) => {
        console.error('Failed to load departments:', error);
        this.toaster.error('Failed to load departments');
        this.loading = false;
      }
    });
  }

  openCreateModal(): void {
    this.isEditMode = false;
    this.selectedDepartmentId = null;
    this.departmentForm.reset({ isActive: true });
    this.isModalOpen = true;
  }

  openEditModal(department: DepartmentDto): void {
    this.isEditMode = true;
    this.selectedDepartmentId = department.id;
    this.departmentForm.patchValue({
      name: department.name,
      code: department.code,
      description: department.description,
      isActive: department.isActive
    });
    this.isModalOpen = true;
  }

  closeModal(): void {
    this.isModalOpen = false;
    this.departmentForm.reset();
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.departmentForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  saveDepartment(): void {
    if (this.departmentForm.invalid) {
      Object.keys(this.departmentForm.controls).forEach(key => {
        this.departmentForm.get(key)?.markAsTouched();
      });
      return;
    }

    this.submitting = true;
    const formValue = this.departmentForm.value;

    const departmentData: CreateUpdateDepartmentDto = {
      name: formValue.name,
      code: formValue.code || undefined,
      description: formValue.description || undefined,
      isActive: formValue.isActive,
      headId: undefined,
      headName: undefined,
      headEmail: undefined,
      parentDepartmentId: undefined
    };

    const operation = this.isEditMode
      ? this.departmentService.update(this.selectedDepartmentId!, departmentData)
      : this.departmentService.create(departmentData);

    operation.subscribe({
      next: () => {
        this.toaster.success(
          this.isEditMode ? 'Department updated successfully' : 'Department created successfully'
        );
        this.closeModal();
        this.loadDepartments();
        this.submitting = false;
      },
      error: (error) => {
        console.error('Failed to save department:', error);
        this.toaster.error(
          error.error?.error?.message || 'Failed to save department'
        );
        this.submitting = false;
      }
    });
  }

  deleteDepartment(department: DepartmentDto): void {
    this.confirmation.warn(
      `Are you sure you want to delete the department "${department.name}"?`,
      'Delete Department'
    ).subscribe((confirmed) => {
      if (confirmed) {
        this.departmentService.delete(department.id).subscribe({
          next: () => {
            this.toaster.success('Department deleted successfully');
            this.loadDepartments();
          },
          error: (error) => {
            console.error('Failed to delete department:', error);
            this.toaster.error(
              error.error?.error?.message || 'Failed to delete department. It may have associated jobs.'
            );
          }
        });
      }
    });
  }
}
