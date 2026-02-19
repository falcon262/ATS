import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { JobService } from '@proxy/jobs';
import { JobDto } from '@proxy/jobs/dtos/models';
import { ToasterService } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-job-detail',
  imports: [CommonModule, RouterModule],
  templateUrl: './job-detail.html',
  styleUrl: './job-detail.scss'
})
export class JobDetail implements OnInit {
  job: JobDto | null = null;
  loading = false;
  jobId: string = '';
  publicUrl: string = '';

  constructor(
    private jobService: JobService,
    private route: ActivatedRoute,
    private toaster: ToasterService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.jobId = params['id'];
      if (this.jobId) {
        this.loadJob();
      }
    });
  }

  loadJob(): void {
    this.loading = true;
    this.jobService.get(this.jobId).subscribe({
      next: (job) => {
        this.job = job;
        this.loading = false;
        // Generate public URL if slug exists
        if (job.publicSlug) {
          this.publicUrl = `${window.location.origin}/apply/${job.publicSlug}`;
        }
        // Increment view count
        this.jobService.incrementViewCount(this.jobId).subscribe();
      },
      error: (error) => {
        console.error('Failed to load job:', error);
        this.loading = false;
      }
    });
  }

  copyPublicLink(): void {
    if (!this.publicUrl) {
      this.toaster.warn('Public link is not available for this job');
      return;
    }

    navigator.clipboard.writeText(this.publicUrl).then(() => {
      this.toaster.success('Public application link copied to clipboard!');
    }).catch((error) => {
      console.error('Failed to copy:', error);
      this.toaster.error('Failed to copy link to clipboard');
    });
  }

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

  getEmploymentTypeText(type: number): string {
    const typeMap: Record<number, string> = {
      0: 'Full-Time',
      1: 'Part-Time',
      2: 'Contract',
      3: 'Internship'
    };
    return typeMap[type] || 'Unknown';
  }

  getExperienceLevelText(level: number): string {
    const levelMap: Record<number, string> = {
      0: 'Entry Level',
      1: 'Junior',
      2: 'Mid Level',
      3: 'Senior',
      4: 'Lead',
      5: 'Executive'
    };
    return levelMap[level] || 'Unknown';
  }

  publishJob(): void {
    if (!confirm('Are you sure you want to publish this job? It will become publicly visible.')) {
      return;
    }

    this.loading = true;
    this.jobService.publish({ jobId: this.jobId } as any).subscribe({
      next: (job) => {
        this.toaster.success('Job published successfully!');
        this.loadJob(); // Reload to get updated status and slug
      },
      error: (error) => {
        console.error('Failed to publish job:', error);
        this.toaster.error('Failed to publish job');
        this.loading = false;
      }
    });
  }

  closeJob(): void {
    if (!confirm('Are you sure you want to close this job? It will stop accepting applications.')) {
      return;
    }

    this.loading = true;
    this.jobService.close(this.jobId).subscribe({
      next: (job) => {
        this.toaster.success('Job closed successfully!');
        this.loadJob(); // Reload to get updated status
      },
      error: (error) => {
        console.error('Failed to close job:', error);
        this.toaster.error('Failed to close job');
        this.loading = false;
      }
    });
  }

  canPublish(): boolean {
    return this.job?.status === 0; // Draft status
  }

  canClose(): boolean {
    return this.job?.status === 1; // Active status
  }
}
