import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface DashboardStats {
  openPositions: number;
  totalApplications: number;
  interviewsScheduled: number;
  offersExtended: number;
}

@Component({
  selector: 'app-stats-overview',
  imports: [CommonModule],
  templateUrl: './stats-overview.html',
  styleUrl: './stats-overview.scss'
})
export class StatsOverview {
  @Input() stats: DashboardStats = {
    openPositions: 0,
    totalApplications: 0,
    interviewsScheduled: 0,
    offersExtended: 0
  };
}
