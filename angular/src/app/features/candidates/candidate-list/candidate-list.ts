import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CandidateService } from '@proxy/candidates';
import { CandidateListDto } from '@proxy/candidates/dtos/models';

@Component({
  selector: 'app-candidate-list',
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './candidate-list.html',
  styleUrl: './candidate-list.scss'
})
export class CandidateList implements OnInit {
  candidates: CandidateListDto[] = [];
  totalCount = 0;
  loading = false;
  searchFilter = '';
  currentPage = 1;
  pageSize = 12;

  constructor(private candidateService: CandidateService) {}

  ngOnInit(): void {
    this.loadCandidates();
  }

  loadCandidates(): void {
    this.loading = true;
    this.candidateService.getList({
      filter: this.searchFilter || undefined,
      skills: undefined,
      maxResultCount: this.pageSize,
      skipCount: (this.currentPage - 1) * this.pageSize,
      sorting: 'creationTime DESC'
    }).subscribe({
      next: (result) => {
        this.candidates = result.items || [];
        this.totalCount = result.totalCount || 0;
        this.loading = false;
      },
      error: (error) => {
        console.error('Failed to load candidates:', error);
        this.loading = false;
      }
    });
  }

  onSearch(): void {
    this.currentPage = 1;
    this.loadCandidates();
  }

  getStatusClass(status: number): string {
    const statusMap: Record<number, string> = {
      0: 'badge bg-success',      // Active
      1: 'badge bg-secondary',    // Inactive
      2: 'badge bg-danger',       // Blacklisted
      3: 'badge bg-primary'       // Hired
    };
    return statusMap[status] || 'badge bg-secondary';
  }

  getStatusText(status: number): string {
    const statusMap: Record<number, string> = {
      0: 'Active',
      1: 'Inactive',
      2: 'Blacklisted',
      3: 'Hired'
    };
    return statusMap[status] || 'Unknown';
  }
}
