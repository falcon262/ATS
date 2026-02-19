import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-recent-applications',
  imports: [CommonModule, RouterModule],
  templateUrl: './recent-applications.html',
  styleUrl: './recent-applications.scss'
})
export class RecentApplications {
  @Input() applications: any[] = [];
  @Input() loading = false;

  getStatusClass(status: string): string {
    const statusMap: Record<string, string> = {
      'Applied': 'badge-custom badge-pending',
      'Screening': 'badge-custom badge-active',
      'Interview': 'badge-custom badge-warning',
      'Offer': 'badge-custom badge-success',
      'Hired': 'badge-custom badge-hired',
      'Rejected': 'badge-custom badge-closed'
    };
    return statusMap[status] || 'badge-custom';
  }

  getAIScoreClass(score: number): string {
    if (score >= 80) return 'ai-score-badge score-high';
    if (score >= 60) return 'ai-score-badge score-medium';
    return 'ai-score-badge score-low';
  }
}
