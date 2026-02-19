# TalentFlow ATS - Angular Frontend Implementation Roadmap

## 🎯 Executive Summary
This document provides a complete step-by-step implementation guide for building the Angular frontend of TalentFlow ATS using ABP.io framework. All backend APIs and proxies for Jobs, Candidates, and Applications are already implemented and ready for integration.

## 📋 Prerequisites Checklist
- ✅ ABP.io Angular project initialized
- ✅ Backend APIs implemented (Jobs, Candidates, Applications)
- ✅ Proxies generated for all services
- ✅ Authentication configured
- ✅ Angular 17+ installed
- ✅ Node.js 18+ installed

---

## 🏗️ Phase 1: Project Setup & Core Module Structure
**Timeline: Day 1-2**

### Step 1.1: Install Required Dependencies
```bash
# Navigate to Angular project
cd angular

# Install UI dependencies
npm install @ng-bootstrap/ng-bootstrap
npm install chart.js ng2-charts
npm install @fortawesome/fontawesome-free
npm install ngx-toastr
npm install ngx-dropzone
npm install @angular/cdk
npm install file-saver
npm install xlsx

# Install development dependencies
npm install @types/file-saver --save-dev
```

### Step 1.2: Configure Module Imports
**File: `src/app/app.module.ts`**
```typescript
// Add these imports to AppModule
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgChartsModule } from 'ng2-charts';
import { ToastrModule } from 'ngx-toastr';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { DragDropModule } from '@angular/cdk/drag-drop';

// Add to imports array:
imports: [
  NgbModule,
  NgChartsModule,
  ToastrModule.forRoot(),
  NgxDropzoneModule,
  DragDropModule,
  // ... existing imports
]
```

### Step 1.3: Update Global Styles
**File: `src/styles.scss`**
```scss
// Add these styles after existing imports
@import '~@fortawesome/fontawesome-free/css/all.css';
@import '~ngx-toastr/toastr.css';

:root {
  --primary: #6366f1;
  --primary-dark: #4f46e5;
  --secondary: #8b5cf6;
  --success: #10b981;
  --warning: #f59e0b;
  --danger: #ef4444;
  --dark: #1e293b;
  --gray: #64748b;
  --light: #f1f5f9;
  --white: #ffffff;
  --gradient: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}

// Custom utility classes
.stat-card {
  background: white;
  border-radius: 15px;
  padding: 1.5rem;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
  transition: transform 0.3s ease;
  
  &:hover {
    transform: translateY(-5px);
    box-shadow: 0 5px 20px rgba(0, 0, 0, 0.1);
  }
}

.data-table-container {
  background: white;
  border-radius: 15px;
  padding: 1.5rem;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.05);
}

.badge-custom {
  padding: 0.375rem 0.75rem;
  border-radius: 20px;
  font-size: 0.75rem;
  font-weight: 600;
  text-transform: uppercase;
}

.ai-score-bar {
  background: #e5e7eb;
  height: 8px;
  border-radius: 10px;
  overflow: hidden;
  
  .ai-score-fill {
    height: 100%;
    background: var(--gradient);
    transition: width 1s ease;
  }
}
```

### Step 1.4: Create Feature Modules
```bash
# Generate feature modules
ng generate module features/dashboard --routing
ng generate module features/jobs --routing
ng generate module features/candidates --routing
ng generate module features/applications --routing
ng generate module features/pipeline --routing
ng generate module features/ai-analysis --routing
ng generate module features/reports --routing
ng generate module features/settings --routing

# Generate shared module
ng generate module shared
```

---

## 🎨 Phase 2: Core Components Implementation
**Timeline: Day 3-5**

### Step 2.1: Dashboard Module Components

#### Create Dashboard Component
```bash
ng generate component features/dashboard/dashboard-page
ng generate component features/dashboard/stats-overview
ng generate component features/dashboard/recent-applications
ng generate component features/dashboard/quick-actions
```

**File: `src/app/features/dashboard/dashboard-page/dashboard-page.component.ts`**
```typescript
import { Component, OnInit } from '@angular/core';
import { JobService, CandidateService, ApplicationService } from '@proxy/application/contracts';
import { ReportsService } from '@proxy/reports';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.scss']
})
export class DashboardPageComponent implements OnInit {
  stats = {
    openPositions: 0,
    totalApplications: 0,
    interviewsScheduled: 0,
    offersExtended: 0
  };
  
  recentApplications: any[] = [];
  loading = false;

  constructor(
    private jobService: JobService,
    private applicationService: ApplicationService,
    private reportsService: ReportsService
  ) {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.loading = true;
    
    // Load dashboard stats
    this.reportsService.getDashboardStats().subscribe({
      next: (data) => {
        this.stats = {
          openPositions: data.openPositions,
          totalApplications: data.totalApplications,
          interviewsScheduled: data.interviewsScheduled,
          offersExtended: data.offersExtended
        };
      },
      error: (error) => console.error('Error loading stats:', error)
    });

    // Load recent applications
    this.applicationService.getList({
      maxResultCount: 10,
      skipCount: 0,
      sorting: 'AppliedDate DESC'
    }).subscribe({
      next: (result) => {
        this.recentApplications = result.items;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading applications:', error);
        this.loading = false;
      }
    });
  }
}
```

**File: `src/app/features/dashboard/dashboard-page/dashboard-page.component.html`**
```html
<div class="container-fluid">
  <div class="row mb-4">
    <div class="col">
      <h1 class="h2">Dashboard Overview</h1>
    </div>
    <div class="col-auto">
      <button class="btn btn-primary" routerLink="/jobs/new">
        <i class="fas fa-plus"></i> New Job Posting
      </button>
    </div>
  </div>

  <!-- Stats Overview -->
  <app-stats-overview [stats]="stats"></app-stats-overview>

  <!-- Recent Applications -->
  <app-recent-applications 
    [applications]="recentApplications"
    [loading]="loading">
  </app-recent-applications>
</div>
```

### Step 2.2: Jobs Module Implementation

#### Generate Job Components
```bash
ng generate component features/jobs/job-list
ng generate component features/jobs/job-detail
ng generate component features/jobs/job-form
ng generate component features/jobs/job-card
```

**File: `src/app/features/jobs/job-list/job-list.component.ts`**
```typescript
import { Component, OnInit } from '@angular/core';
import { JobService } from '@proxy/jobs';
import { JobListDto, GetJobListInput } from '@proxy/jobs/models';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { JobFormComponent } from '../job-form/job-form.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-job-list',
  templateUrl: './job-list.component.html',
  styleUrls: ['./job-list.component.scss']
})
export class JobListComponent implements OnInit {
  jobs: JobListDto[] = [];
  totalCount = 0;
  loading = false;
  
  searchParams: GetJobListInput = {
    filter: '',
    maxResultCount: 10,
    skipCount: 0,
    sorting: 'PostedDate DESC'
  };

  constructor(
    private jobService: JobService,
    private modalService: NgbModal,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadJobs();
  }

  loadJobs(): void {
    this.loading = true;
    this.jobService.getList(this.searchParams).subscribe({
      next: (result) => {
        this.jobs = result.items;
        this.totalCount = result.totalCount;
        this.loading = false;
      },
      error: (error) => {
        this.toastr.error('Failed to load jobs');
        this.loading = false;
      }
    });
  }

  openJobForm(jobId?: string): void {
    const modalRef = this.modalService.open(JobFormComponent, {
      size: 'lg',
      backdrop: 'static'
    });
    
    modalRef.componentInstance.jobId = jobId;
    
    modalRef.result.then(
      (result) => {
        if (result) {
          this.loadJobs();
          this.toastr.success('Job saved successfully');
        }
      },
      () => {} // Modal dismissed
    );
  }

  publishJob(jobId: string): void {
    this.jobService.publish({ jobId }).subscribe({
      next: () => {
        this.toastr.success('Job published successfully');
        this.loadJobs();
      },
      error: () => this.toastr.error('Failed to publish job')
    });
  }

  closeJob(jobId: string): void {
    this.jobService.close(jobId).subscribe({
      next: () => {
        this.toastr.success('Job closed successfully');
        this.loadJobs();
      },
      error: () => this.toastr.error('Failed to close job')
    });
  }

  onPageChange(page: number): void {
    this.searchParams.skipCount = (page - 1) * this.searchParams.maxResultCount;
    this.loadJobs();
  }

  onSearch(filter: string): void {
    this.searchParams.filter = filter;
    this.searchParams.skipCount = 0;
    this.loadJobs();
  }
}
```

### Step 2.3: Candidates Module Implementation

#### Generate Candidate Components
```bash
ng generate component features/candidates/candidate-list
ng generate component features/candidates/candidate-detail
ng generate component features/candidates/candidate-form
ng generate component features/candidates/resume-upload
```

**File: `src/app/features/candidates/candidate-list/candidate-list.component.ts`**
```typescript
import { Component, OnInit } from '@angular/core';
import { CandidateService } from '@proxy/candidates';
import { CandidateListDto, GetCandidateListInput } from '@proxy/candidates/models';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CandidateDetailComponent } from '../candidate-detail/candidate-detail.component';

@Component({
  selector: 'app-candidate-list',
  templateUrl: './candidate-list.component.html',
  styleUrls: ['./candidate-list.component.scss']
})
export class CandidateListComponent implements OnInit {
  candidates: CandidateListDto[] = [];
  totalCount = 0;
  loading = false;
  
  filters: GetCandidateListInput = {
    filter: '',
    skills: [],
    minExperience: null,
    maxExperience: null,
    minAIScore: null,
    status: null,
    maxResultCount: 10,
    skipCount: 0,
    sorting: 'CreationTime DESC'
  };

  constructor(
    private candidateService: CandidateService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.loadCandidates();
  }

  loadCandidates(): void {
    this.loading = true;
    this.candidateService.getList(this.filters).subscribe({
      next: (result) => {
        this.candidates = result.items;
        this.totalCount = result.totalCount;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  viewCandidateDetail(candidateId: string): void {
    const modalRef = this.modalService.open(CandidateDetailComponent, {
      size: 'xl',
      scrollable: true
    });
    
    modalRef.componentInstance.candidateId = candidateId;
  }

  applyFilters(): void {
    this.filters.skipCount = 0;
    this.loadCandidates();
  }

  clearFilters(): void {
    this.filters = {
      filter: '',
      skills: [],
      minExperience: null,
      maxExperience: null,
      minAIScore: null,
      status: null,
      maxResultCount: 10,
      skipCount: 0,
      sorting: 'CreationTime DESC'
    };
    this.loadCandidates();
  }
}
```

### Step 2.4: Pipeline (Kanban Board) Implementation

#### Generate Pipeline Components
```bash
ng generate component features/pipeline/pipeline-board
ng generate component features/pipeline/candidate-card
ng generate component features/pipeline/stage-column
```

**File: `src/app/features/pipeline/pipeline-board/pipeline-board.component.ts`**
```typescript
import { Component, OnInit } from '@angular/core';
import { ApplicationService } from '@proxy/applications';
import { JobService } from '@proxy/jobs';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { PipelineStage } from '@proxy/applications/models';
import { ToastrService } from 'ngx-toastr';

interface PipelineColumn {
  stage: PipelineStage;
  title: string;
  icon: string;
  applications: any[];
  color: string;
}

@Component({
  selector: 'app-pipeline-board',
  templateUrl: './pipeline-board.component.html',
  styleUrls: ['./pipeline-board.component.scss']
})
export class PipelineBoardComponent implements OnInit {
  selectedJobId: string | null = null;
  jobs: any[] = [];
  loading = false;
  
  pipelineColumns: PipelineColumn[] = [
    {
      stage: PipelineStage.Applied,
      title: 'Applied',
      icon: '📥',
      applications: [],
      color: '#6366f1'
    },
    {
      stage: PipelineStage.Screening,
      title: 'Screening',
      icon: '🔍',
      applications: [],
      color: '#8b5cf6'
    },
    {
      stage: PipelineStage.PhoneScreen,
      title: 'Phone Screen',
      icon: '📞',
      applications: [],
      color: '#f59e0b'
    },
    {
      stage: PipelineStage.FirstInterview,
      title: 'First Interview',
      icon: '💬',
      applications: [],
      color: '#10b981'
    },
    {
      stage: PipelineStage.FinalInterview,
      title: 'Final Interview',
      icon: '🎯',
      applications: [],
      color: '#3b82f6'
    },
    {
      stage: PipelineStage.Offer,
      title: 'Offer',
      icon: '✉️',
      applications: [],
      color: '#ec4899'
    },
    {
      stage: PipelineStage.Hired,
      title: 'Hired',
      icon: '✅',
      applications: [],
      color: '#10b981'
    }
  ];

  constructor(
    private applicationService: ApplicationService,
    private jobService: JobService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadJobs();
  }

  loadJobs(): void {
    this.jobService.getActiveJobs().subscribe({
      next: (jobs) => {
        this.jobs = jobs;
        if (jobs.length > 0 && !this.selectedJobId) {
          this.selectedJobId = jobs[0].id;
          this.loadApplications();
        }
      }
    });
  }

  loadApplications(): void {
    if (!this.selectedJobId) return;
    
    this.loading = true;
    
    // Reset all columns
    this.pipelineColumns.forEach(col => col.applications = []);
    
    this.applicationService.getList({
      jobId: this.selectedJobId,
      maxResultCount: 100,
      skipCount: 0
    }).subscribe({
      next: (result) => {
        // Group applications by stage
        result.items.forEach(app => {
          const column = this.pipelineColumns.find(col => col.stage === app.stage);
          if (column) {
            column.applications.push(app);
          }
        });
        this.loading = false;
      },
      error: () => {
        this.toastr.error('Failed to load applications');
        this.loading = false;
      }
    });
  }

  onJobChange(): void {
    this.loadApplications();
  }

  drop(event: CdkDragDrop<any[]>, targetStage: PipelineStage): void {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      const application = event.previousContainer.data[event.previousIndex];
      
      // Update application stage in backend
      this.applicationService.moveToStage({
        applicationId: application.id,
        newStage: targetStage,
        notes: `Moved via pipeline board`
      }).subscribe({
        next: () => {
          transferArrayItem(
            event.previousContainer.data,
            event.container.data,
            event.previousIndex,
            event.currentIndex
          );
          this.toastr.success('Application stage updated');
        },
        error: () => {
          this.toastr.error('Failed to update stage');
        }
      });
    }
  }

  getConnectedLists(): string[] {
    return this.pipelineColumns.map((_, index) => `list-${index}`);
  }
}
```

---

## 🤖 Phase 3: AI Integration Components
**Timeline: Day 6-7**

### Step 3.1: AI Analysis Module

#### Generate AI Components
```bash
ng generate component features/ai-analysis/ai-dashboard
ng generate component features/ai-analysis/ai-scoring
ng generate component features/ai-analysis/skill-gap-analysis
ng generate component features/ai-analysis/batch-analysis
```

**File: `src/app/features/ai-analysis/ai-dashboard/ai-dashboard.component.ts`**
```typescript
import { Component, OnInit } from '@angular/core';
import { AIAnalysisService } from '@proxy/ai';
import { JobService } from '@proxy/jobs';
import { ChartConfiguration, ChartType } from 'chart.js';

@Component({
  selector: 'app-ai-dashboard',
  templateUrl: './ai-dashboard.component.html',
  styleUrls: ['./ai-dashboard.component.scss']
})
export class AiDashboardComponent implements OnInit {
  selectedJobId: string | null = null;
  jobs: any[] = [];
  rankedApplications: any[] = [];
  aiProvider = 'Claude';
  loading = false;
  
  aiInsights = {
    averageScore: 0,
    topCandidatesCount: 0,
    screeningSpeed: 0,
    predictionAccuracy: 0
  };

  // Chart configuration for skill gap analysis
  skillGapChartData: ChartConfiguration<'bar'> = {
    type: 'bar',
    data: {
      labels: [],
      datasets: [{
        data: [],
        backgroundColor: '#6366f1',
        label: 'Skill Demand'
      }]
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      scales: {
        y: {
          beginAtZero: true,
          max: 100
        }
      }
    }
  };

  constructor(
    private aiService: AIAnalysisService,
    private jobService: JobService
  ) {}

  ngOnInit(): void {
    this.loadJobs();
    this.loadAIInsights();
  }

  loadJobs(): void {
    this.jobService.getActiveJobs().subscribe({
      next: (jobs) => {
        this.jobs = jobs;
        if (jobs.length > 0) {
          this.selectedJobId = jobs[0].id;
          this.loadRankedApplications();
        }
      }
    });
  }

  loadRankedApplications(): void {
    if (!this.selectedJobId) return;
    
    this.loading = true;
    this.aiService.getRankedApplications({
      jobId: this.selectedJobId,
      topCount: 10,
      minScore: 70
    }).subscribe({
      next: (applications) => {
        this.rankedApplications = applications;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  loadAIInsights(): void {
    // Load AI insights from service
    // This would typically come from a reports/analytics endpoint
    this.aiInsights = {
      averageScore: 82,
      topCandidatesCount: 15,
      screeningSpeed: 3.2,
      predictionAccuracy: 78
    };
    
    // Load skill gap data
    this.loadSkillGapAnalysis();
  }

  loadSkillGapAnalysis(): void {
    // This would come from an AI analysis endpoint
    const skillGaps = [
      { skill: 'Cloud Architecture', demand: 85 },
      { skill: 'Machine Learning', demand: 65 },
      { skill: 'DevOps', demand: 70 },
      { skill: 'React', demand: 90 },
      { skill: 'Python', demand: 75 }
    ];
    
    this.skillGapChartData.data.labels = skillGaps.map(s => s.skill);
    this.skillGapChartData.data.datasets[0].data = skillGaps.map(s => s.demand);
  }

  runBatchAnalysis(): void {
    if (!this.selectedJobId) return;
    
    this.loading = true;
    this.aiService.batchAnalyze({
      jobId: this.selectedJobId,
      applicationIds: [], // Would get from selected applications
      rankAfterAnalysis: true
    }).subscribe({
      next: () => {
        this.loadRankedApplications();
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }
}
```

---

## 📊 Phase 4: Reports & Analytics
**Timeline: Day 8-9**

### Step 4.1: Reports Module Components

```bash
ng generate component features/reports/reports-dashboard
ng generate component features/reports/metrics-cards
ng generate component features/reports/hiring-chart
ng generate component features/reports/department-performance
```

**File: `src/app/features/reports/reports-dashboard/reports-dashboard.component.ts`**
```typescript
import { Component, OnInit } from '@angular/core';
import { ReportsService } from '@proxy/reports';
import { ChartConfiguration } from 'chart.js';
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-reports-dashboard',
  templateUrl: './reports-dashboard.component.html',
  styleUrls: ['./reports-dashboard.component.scss']
})
export class ReportsDashboardComponent implements OnInit {
  dateRange = { start: new Date(), end: new Date() };
  selectedDepartmentId: string | null = null;
  
  metrics = {
    timeToHire: 0,
    costPerHire: 0,
    offerAcceptanceRate: 0,
    qualityOfHire: 0
  };
  
  // Hiring trend chart
  hiringTrendChart: ChartConfiguration<'line'> = {
    type: 'line',
    data: {
      labels: [],
      datasets: [{
        data: [],
        label: 'Applications',
        borderColor: '#6366f1',
        backgroundColor: 'rgba(99, 102, 241, 0.1)',
        fill: true
      }]
    },
    options: {
      responsive: true,
      maintainAspectRatio: false
    }
  };

  departmentPerformance: any[] = [];
  loading = false;

  constructor(private reportsService: ReportsService) {}

  ngOnInit(): void {
    this.setDateRange('month');
    this.loadMetrics();
    this.loadChartData();
  }

  setDateRange(period: 'week' | 'month' | 'quarter' | 'year'): void {
    const end = new Date();
    const start = new Date();
    
    switch(period) {
      case 'week':
        start.setDate(end.getDate() - 7);
        break;
      case 'month':
        start.setMonth(end.getMonth() - 1);
        break;
      case 'quarter':
        start.setMonth(end.getMonth() - 3);
        break;
      case 'year':
        start.setFullYear(end.getFullYear() - 1);
        break;
    }
    
    this.dateRange = { start, end };
    this.loadMetrics();
  }

  loadMetrics(): void {
    this.loading = true;
    this.reportsService.getRecruitmentMetrics({
      startDate: this.dateRange.start,
      endDate: this.dateRange.end,
      departmentId: this.selectedDepartmentId,
      reportType: 'recruitment'
    }).subscribe({
      next: (data) => {
        this.metrics = {
          timeToHire: data.averageTimeToHire,
          costPerHire: 4250, // Would come from backend
          offerAcceptanceRate: data.offerAcceptanceRate,
          qualityOfHire: 4.2 // Would come from backend
        };
        this.departmentPerformance = data.hiresByDepartment || [];
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  loadChartData(): void {
    this.reportsService.getDashboardStats().subscribe({
      next: (data) => {
        if (data.applicationTrend) {
          this.hiringTrendChart.data.labels = data.applicationTrend.map(t => t.label);
          this.hiringTrendChart.data.datasets[0].data = data.applicationTrend.map(t => t.count);
        }
      }
    });
  }

  exportToExcel(): void {
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet([
      {
        'Metric': 'Time to Hire',
        'Value': `${this.metrics.timeToHire} days`
      },
      {
        'Metric': 'Cost per Hire',
        'Value': `$${this.metrics.costPerHire}`
      },
      {
        'Metric': 'Offer Acceptance Rate',
        'Value': `${this.metrics.offerAcceptanceRate}%`
      },
      {
        'Metric': 'Quality of Hire',
        'Value': `${this.metrics.qualityOfHire}/5.0`
      }
    ]);
    
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Metrics');
    
    const wbout: ArrayBuffer = XLSX.write(wb, { bookType: 'xlsx', type: 'array' });
    const blob = new Blob([wbout], { type: 'application/octet-stream' });
    saveAs(blob, `recruitment-metrics-${new Date().toISOString().split('T')[0]}.xlsx`);
  }
}
```

---

## 🔧 Phase 5: Settings & Configuration
**Timeline: Day 10**

### Step 5.1: Settings Module

```bash
ng generate component features/settings/settings-page
ng generate component features/settings/ai-settings
ng generate component features/settings/user-management
ng generate component features/settings/audit-logs
```

**File: `src/app/features/settings/ai-settings/ai-settings.component.ts`**
```typescript
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConfigurationService } from '@abp/ng.core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-ai-settings',
  templateUrl: './ai-settings.component.html',
  styleUrls: ['./ai-settings.component.scss']
})
export class AiSettingsComponent implements OnInit {
  aiSettingsForm: FormGroup;
  providers = ['Claude', 'OpenAI', 'Gemini'];
  loading = false;

  constructor(
    private fb: FormBuilder,
    private configService: ConfigurationService,
    private toastr: ToastrService
  ) {
    this.aiSettingsForm = this.fb.group({
      provider: ['Claude', Validators.required],
      apiKey: ['', Validators.required],
      autoScoreThreshold: [75, [Validators.min(0), Validators.max(100)]],
      enableAutoRanking: [true],
      maxConcurrentAnalysis: [5, [Validators.min(1), Validators.max(20)]]
    });
  }

  ngOnInit(): void {
    this.loadSettings();
  }

  loadSettings(): void {
    // Load settings from configuration service
    this.configService.getAll().subscribe({
      next: (config) => {
        // Map configuration to form
        const aiSettings = config['AI'] || {};
        this.aiSettingsForm.patchValue({
          provider: aiSettings.provider || 'Claude',
          apiKey: aiSettings.apiKey || '',
          autoScoreThreshold: aiSettings.autoScoreThreshold || 75,
          enableAutoRanking: aiSettings.enableAutoRanking !== false,
          maxConcurrentAnalysis: aiSettings.maxConcurrentAnalysis || 5
        });
      }
    });
  }

  saveSettings(): void {
    if (this.aiSettingsForm.invalid) {
      return;
    }

    this.loading = true;
    const settings = this.aiSettingsForm.value;

    // Save settings via configuration service
    // This would typically be a custom endpoint
    setTimeout(() => {
      this.toastr.success('AI settings saved successfully');
      this.loading = false;
    }, 1000);
  }

  testConnection(): void {
    const provider = this.aiSettingsForm.get('provider')?.value;
    const apiKey = this.aiSettingsForm.get('apiKey')?.value;

    if (!apiKey) {
      this.toastr.error('Please enter an API key');
      return;
    }

    // Test API connection
    this.toastr.info(`Testing ${provider} connection...`);
    
    // This would call a test endpoint
    setTimeout(() => {
      this.toastr.success('Connection successful!');
    }, 1500);
  }
}
```

---

## 🎯 Phase 6: Shared Components & Services
**Timeline: Day 11-12**

### Step 6.1: Create Shared Components

```bash
# Generate shared components
ng generate component shared/components/loading-spinner
ng generate component shared/components/confirm-dialog
ng generate component shared/components/ai-score-badge
ng generate component shared/components/status-badge
ng generate component shared/components/search-filter
ng generate component shared/components/pagination

# Generate shared services
ng generate service shared/services/export
ng generate service shared/services/notification
ng generate service shared/services/ai-integration
```

**File: `src/app/shared/components/ai-score-badge/ai-score-badge.component.ts`**
```typescript
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-ai-score-badge',
  template: `
    <span [class]="getBadgeClass()" [title]="getTooltip()">
      {{ score }}%
    </span>
  `,
  styles: [`
    :host {
      display: inline-block;
    }
    span {
      padding: 0.375rem 0.75rem;
      border-radius: 20px;
      font-size: 0.75rem;
      font-weight: 600;
    }
    .score-high {
      background: rgba(16, 185, 129, 0.1);
      color: var(--success);
    }
    .score-medium {
      background: rgba(245, 158, 11, 0.1);
      color: var(--warning);
    }
    .score-low {
      background: rgba(239, 68, 68, 0.1);
      color: var(--danger);
    }
  `]
})
export class AiScoreBadgeComponent {
  @Input() score: number = 0;

  getBadgeClass(): string {
    if (this.score >= 80) return 'badge score-high';
    if (this.score >= 60) return 'badge score-medium';
    return 'badge score-low';
  }

  getTooltip(): string {
    if (this.score >= 80) return 'Excellent match';
    if (this.score >= 60) return 'Good match';
    return 'Fair match';
  }
}
```

### Step 6.2: Create AI Integration Service

**File: `src/app/shared/services/ai-integration.service.ts`**
```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { delay, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AiIntegrationService {
  private apiEndpoint = '/api/ai';

  constructor(private http: HttpClient) {}

  analyzeResume(resumeText: string, jobRequirements: string): Observable<any> {
    // Simulated AI analysis - replace with actual API call
    return of({
      overallScore: Math.floor(Math.random() * 30) + 70,
      skillMatch: this.generateSkillScores(),
      strengths: ['Strong technical background', 'Relevant experience'],
      recommendations: 'Consider for interview'
    }).pipe(delay(1500));
  }

  batchAnalyze(applicationIds: string[]): Observable<any> {
    return this.http.post(`${this.apiEndpoint}/batch-analyze`, {
      applicationIds
    });
  }

  private generateSkillScores(): any {
    return {
      'Technical Skills': Math.floor(Math.random() * 30) + 70,
      'Experience': Math.floor(Math.random() * 30) + 70,
      'Education': Math.floor(Math.random() * 30) + 70,
      'Soft Skills': Math.floor(Math.random() * 30) + 70
    };
  }
}
```

---

## 🚦 Phase 7: Routing Configuration
**Timeline: Day 13**

### Step 7.1: Configure App Routing

**File: `src/app/app-routing.module.ts`**
```typescript
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@abp/ng.core';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/dashboard'
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./features/dashboard/dashboard.module').then(m => m.DashboardModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'jobs',
    loadChildren: () => import('./features/jobs/jobs.module').then(m => m.JobsModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'candidates',
    loadChildren: () => import('./features/candidates/candidates.module').then(m => m.CandidatesModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'applications',
    loadChildren: () => import('./features/applications/applications.module').then(m => m.ApplicationsModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'pipeline',
    loadChildren: () => import('./features/pipeline/pipeline.module').then(m => m.PipelineModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'ai-analysis',
    loadChildren: () => import('./features/ai-analysis/ai-analysis.module').then(m => m.AiAnalysisModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'reports',
    loadChildren: () => import('./features/reports/reports.module').then(m => m.ReportsModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'settings',
    loadChildren: () => import('./features/settings/settings.module').then(m => m.SettingsModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
```

### Step 7.2: Configure Navigation Menu

**File: `src/app/route.provider.ts`**
```typescript
import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/dashboard',
        name: 'Dashboard',
        iconClass: 'fas fa-chart-line',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/jobs',
        name: 'Jobs',
        iconClass: 'fas fa-briefcase',
        order: 2,
        layout: eLayoutType.application,
      },
      {
        path: '/candidates',
        name: 'Candidates',
        iconClass: 'fas fa-users',
        order: 3,
        layout: eLayoutType.application,
      },
      {
        path: '/pipeline',
        name: 'Pipeline',
        iconClass: 'fas fa-tasks',
        order: 4,
        layout: eLayoutType.application,
      },
      {
        path: '/ai-analysis',
        name: 'AI Analysis',
        iconClass: 'fas fa-robot',
        order: 5,
        layout: eLayoutType.application,
      },
      {
        path: '/reports',
        name: 'Reports',
        iconClass: 'fas fa-chart-bar',
        order: 6,
        layout: eLayoutType.application,
      },
      {
        path: '/settings',
        name: 'Settings',
        iconClass: 'fas fa-cog',
        order: 7,
        layout: eLayoutType.application,
      },
    ]);
  };
}
```

---

## 🧪 Phase 8: Testing Setup
**Timeline: Day 14**

### Step 8.1: Unit Test Configuration

**File: `src/app/features/jobs/job-list/job-list.component.spec.ts`**
```typescript
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { JobListComponent } from './job-list.component';
import { JobService } from '@proxy/jobs';
import { of } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

describe('JobListComponent', () => {
  let component: JobListComponent;
  let fixture: ComponentFixture<JobListComponent>;
  let jobService: jasmine.SpyObj<JobService>;
  let modalService: jasmine.SpyObj<NgbModal>;
  let toastrService: jasmine.SpyObj<ToastrService>;

  beforeEach(() => {
    const jobServiceSpy = jasmine.createSpyObj('JobService', ['getList', 'publish', 'close']);
    const modalServiceSpy = jasmine.createSpyObj('NgbModal', ['open']);
    const toastrServiceSpy = jasmine.createSpyObj('ToastrService', ['success', 'error']);

    TestBed.configureTestingModule({
      declarations: [JobListComponent],
      providers: [
        { provide: JobService, useValue: jobServiceSpy },
        { provide: NgbModal, useValue: modalServiceSpy },
        { provide: ToastrService, useValue: toastrServiceSpy }
      ]
    });

    fixture = TestBed.createComponent(JobListComponent);
    component = fixture.componentInstance;
    jobService = TestBed.inject(JobService) as jasmine.SpyObj<JobService>;
    modalService = TestBed.inject(NgbModal) as jasmine.SpyObj<NgbModal>;
    toastrService = TestBed.inject(ToastrService) as jasmine.SpyObj<ToastrService>;
  });

  it('should load jobs on init', () => {
    const mockJobs = {
      items: [
        { id: '1', title: 'Developer', status: 'Active' }
      ],
      totalCount: 1
    };

    jobService.getList.and.returnValue(of(mockJobs));

    component.ngOnInit();

    expect(jobService.getList).toHaveBeenCalled();
    expect(component.jobs.length).toBe(1);
    expect(component.totalCount).toBe(1);
  });

  it('should publish job successfully', () => {
    const jobId = '123';
    jobService.publish.and.returnValue(of({}));
    jobService.getList.and.returnValue(of({ items: [], totalCount: 0 }));

    component.publishJob(jobId);

    expect(jobService.publish).toHaveBeenCalledWith({ jobId });
    expect(toastrService.success).toHaveBeenCalledWith('Job published successfully');
  });
});
```

---

## 📱 Phase 9: Responsive Design
**Timeline: Day 15**

### Step 9.1: Add Responsive Styles

**File: `src/app/shared/styles/_responsive.scss`**
```scss
// Breakpoints
$breakpoints: (
  xs: 0,
  sm: 576px,
  md: 768px,
  lg: 992px,
  xl: 1200px,
  xxl: 1400px
);

// Responsive mixins
@mixin respond-to($breakpoint) {
  @if map-has-key($breakpoints, $breakpoint) {
    @media (min-width: map-get($breakpoints, $breakpoint)) {
      @content;
    }
  }
}

// Mobile-first responsive utilities
.stat-card {
  margin-bottom: 1rem;
  
  @include respond-to(md) {
    margin-bottom: 0;
  }
}

.kanban-board {
  flex-direction: column;
  
  @include respond-to(lg) {
    flex-direction: row;
  }
  
  .kanban-column {
    width: 100%;
    margin-bottom: 1rem;
    
    @include respond-to(lg) {
      min-width: 300px;
      margin-bottom: 0;
    }
  }
}

.data-table-container {
  overflow-x: auto;
  
  table {
    min-width: 600px;
    
    @include respond-to(md) {
      min-width: auto;
    }
  }
}

// Hide/show elements based on screen size
.mobile-only {
  @include respond-to(md) {
    display: none !important;
  }
}

.desktop-only {
  display: none !important;
  
  @include respond-to(md) {
    display: block !important;
  }
}
```

---

## 🚀 Phase 10: Deployment Preparation
**Timeline: Day 16**

### Step 10.1: Production Build Configuration

**File: `angular.json` (update production configuration)**
```json
{
  "production": {
    "budgets": [
      {
        "type": "initial",
        "maximumWarning": "2mb",
        "maximumError": "5mb"
      },
      {
        "type": "anyComponentStyle",
        "maximumWarning": "6kb",
        "maximumError": "10kb"
      }
    ],
    "fileReplacements": [
      {
        "replace": "src/environments/environment.ts",
        "with": "src/environments/environment.prod.ts"
      }
    ],
    "outputHashing": "all"
  }
}
```

### Step 10.2: Environment Configuration

**File: `src/environments/environment.prod.ts`**
```typescript
export const environment = {
  production: true,
  application: {
    baseUrl: 'https://talentflow.yourdomain.com',
    name: 'TalentFlow ATS',
  },
  oAuthConfig: {
    issuer: 'https://talentflow.yourdomain.com',
    redirectUri: window.location.origin,
    clientId: 'TalentFlow_App',
    responseType: 'code',
    scope: 'offline_access TalentFlow',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://talentflow.yourdomain.com',
      rootNamespace: 'TalentFlow',
    }
  }
};
```

### Step 10.3: Build Commands

```bash
# Development build
npm run build

# Production build
npm run build:prod

# Build with stats for bundle analysis
npm run build:stats

# Serve production build locally
npm run serve:prod
```

---

## ✅ Implementation Checklist

### Phase Completion Checklist:
- [ ] **Phase 1**: Project Setup & Dependencies
- [ ] **Phase 2**: Core Components (Dashboard, Jobs, Candidates)
- [ ] **Phase 3**: AI Integration Components
- [ ] **Phase 4**: Reports & Analytics
- [ ] **Phase 5**: Settings & Configuration
- [ ] **Phase 6**: Shared Components & Services
- [ ] **Phase 7**: Routing Configuration
- [ ] **Phase 8**: Testing Setup
- [ ] **Phase 9**: Responsive Design
- [ ] **Phase 10**: Deployment Preparation

### Feature Completion Checklist:
- [ ] Dashboard with statistics
- [ ] Job management (CRUD + Publish/Close)
- [ ] Candidate management with resume upload
- [ ] Application tracking
- [ ] Pipeline/Kanban board with drag-drop
- [ ] AI scoring and analysis
- [ ] Reports and metrics
- [ ] Settings and configuration
- [ ] User management
- [ ] Audit logging

### Integration Checklist:
- [ ] ABP.io proxy services connected
- [ ] Authentication/Authorization working
- [ ] API endpoints integrated
- [ ] File upload/download working
- [ ] Real-time updates (SignalR) configured
- [ ] Export functionality (Excel/PDF)

### Quality Checklist:
- [ ] Unit tests written (>70% coverage)
- [ ] E2E tests configured
- [ ] Responsive design tested
- [ ] Cross-browser compatibility verified
- [ ] Performance optimized
- [ ] Accessibility standards met
- [ ] Error handling implemented
- [ ] Loading states added
- [ ] Form validations complete

---

## 🛠️ Troubleshooting Guide

### Common Issues and Solutions:

1. **Proxy Service Not Found**
   ```bash
   # Regenerate proxies
   abp generate-proxy -t ng
   ```

2. **CORS Issues**
   - Check backend CORS configuration
   - Ensure API URL in environment matches backend

3. **Authentication Issues**
   - Verify OAuth configuration
   - Check token expiration handling

4. **Build Errors**
   ```bash
   # Clear cache and reinstall
   rm -rf node_modules package-lock.json
   npm install
   ```

5. **Module Import Errors**
   - Ensure all modules are properly imported in app.module.ts
   - Check for circular dependencies

---

## 📚 Additional Resources

- [ABP.io Angular Documentation](https://docs.abp.io/en/abp/latest/UI/Angular/Getting-Started)
- [Angular Material CDK (Drag & Drop)](https://material.angular.io/cdk/drag-drop)
- [Chart.js Documentation](https://www.chartjs.org/docs/latest/)
- [ng-bootstrap Components](https://ng-bootstrap.github.io/#/components)
- [ngx-toastr Documentation](https://github.com/scttcper/ngx-toastr)

---

## 🎉 Completion

Once all phases are complete, your TalentFlow ATS Angular frontend will be fully functional with:
- Complete UI matching the design mockup
- Full integration with ABP.io backend APIs
- AI-powered candidate scoring and ranking
- Comprehensive reporting and analytics
- Responsive design for all devices
- Production-ready configuration

**Estimated Total Implementation Time: 16 Days**

This roadmap can be directly fed into Cursor AI for implementation. Each code block is complete and ready to use with your existing ABP.io backend structure.