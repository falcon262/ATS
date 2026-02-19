import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { JobService } from '@proxy/jobs';
import { JobListDto } from '@proxy/jobs/dtos/models';
import { JobCard } from '../job-card/job-card';

@Component({
  selector: 'app-job-list',
  imports: [CommonModule, RouterModule, FormsModule, JobCard],
  templateUrl: './job-list.html',
  styleUrl: './job-list.scss'
})
export class JobList implements OnInit {
  jobs: JobListDto[] = [];
  totalCount = 0;
  loading = false;
  searchFilter = '';

  currentPage = 1;
  pageSize = 12;

  constructor(private jobService: JobService) {}

  ngOnInit(): void {
    this.loadJobs();
  }

  loadJobs(): void {
    this.loading = true;
    this.jobService.getList({
      filter: this.searchFilter || undefined,
      maxResultCount: this.pageSize,
      skipCount: (this.currentPage - 1) * this.pageSize,
      sorting: 'creationTime DESC'
    }).subscribe({
      next: (result) => {
        this.jobs = result.items || [];
        this.totalCount = result.totalCount || 0;
        this.loading = false;
      },
      error: (error) => {
        console.error('Failed to load jobs:', error);
        this.loading = false;
      }
    });
  }

  onSearch(): void {
    this.currentPage = 1;
    this.loadJobs();
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadJobs();
  }

  get totalPages(): number {
    return Math.ceil(this.totalCount / this.pageSize);
  }

  get pages(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }
}
