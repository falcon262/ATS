import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { StatsOverview, DashboardStats } from '../stats-overview/stats-overview';
import { RecentApplications } from '../recent-applications/recent-applications';
import { QuickActions } from '../quick-actions/quick-actions';
import { JobService } from '@proxy/jobs';
import { ApplicationService } from '@proxy/applications';

@Component({
  selector: 'app-dashboard-page',
  imports: [
    CommonModule,
    RouterModule,
    StatsOverview,
    RecentApplications,
    QuickActions
  ],
  templateUrl: './dashboard-page.html',
  styleUrl: './dashboard-page.scss'
})
export class DashboardPage implements OnInit {
  stats: DashboardStats = {
    openPositions: 0,
    totalApplications: 0,
    interviewsScheduled: 0,
    offersExtended: 0
  };

  recentApplications: any[] = [];
  loading = false;

  constructor(
    private jobService: JobService,
    private applicationService: ApplicationService
  ) {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.loading = true;

    // Load active jobs count
    this.jobService.getActiveJobs().subscribe({
      next: (jobs) => {
        this.stats.openPositions = jobs.length;
      },
      error: (error) => console.error('Error loading jobs:', error)
    });

    // Load recent applications
    this.applicationService.getList({
      maxResultCount: 10,
      skipCount: 0,
      sorting: 'creationTime DESC'
    }).subscribe({
      next: (result) => {
        this.recentApplications = result.items || [];
        this.stats.totalApplications = result.totalCount || 0;

        // Calculate other stats from applications
        this.calculateStats(result.items || []);
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading applications:', error);
        this.loading = false;
      }
    });
  }

  private calculateStats(applications: any[]): void {
    // Count interviews (applications in interview stages)
    this.stats.interviewsScheduled = applications.filter(app =>
      app.stage === 3 || app.stage === 4 || app.stage === 5 // FirstInterview, FinalInterview, TechnicalInterview
    ).length;

    // Count offers
    this.stats.offersExtended = applications.filter(app =>
      app.stage === 6 // Offer stage
    ).length;
  }
}
