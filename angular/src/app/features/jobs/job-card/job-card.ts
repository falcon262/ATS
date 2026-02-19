import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { JobListDto } from '@proxy/jobs/dtos/models';
import { JobService } from '@proxy/jobs';

@Component({
  selector: 'app-job-card',
  imports: [CommonModule, RouterModule],
  templateUrl: './job-card.html',
  styleUrl: './job-card.scss'
})
export class JobCard {
  @Input() job!: JobListDto;
  @Output() refresh = new EventEmitter<void>();

  constructor(private jobService: JobService) {}

  getStatusClass(status: number): string {
    const statusMap: Record<number, string> = {
      0: 'badge-custom badge-draft',
      1: 'badge-custom badge-active',
      2: 'badge-custom badge-closed'
    };
    return statusMap[status] || 'badge-custom';
  }

  getStatusText(status: number): string {
    const statusMap: Record<number, string> = {
      0: 'Draft',
      1: 'Active',
      2: 'Closed'
    };
    return statusMap[status] || 'Unknown';
  }

  publishJob(): void {
    if (!this.job.id) return;

    this.jobService.publish({ jobId: this.job.id }).subscribe({
      next: () => {
        this.refresh.emit();
      },
      error: (error) => {
        console.error('Failed to publish job:', error);
      }
    });
  }

  closeJob(): void {
    if (!this.job.id) return;

    if (confirm('Are you sure you want to close this job posting?')) {
      this.jobService.close(this.job.id).subscribe({
        next: () => {
          this.refresh.emit();
        },
        error: (error) => {
          console.error('Failed to close job:', error);
        }
      });
    }
  }
}
