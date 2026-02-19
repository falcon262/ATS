import type { AIAnalysisRequestDto, AIAnalysisResultDto, AIRankingRequestDto, BatchAIAnalysisDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { ApplicationListDto } from '../applications/dtos/models';

@Injectable({
  providedIn: 'root',
})
export class AIAnalysisService {
  apiName = 'Default';
  

  analyzeApplication = (input: AIAnalysisRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AIAnalysisResultDto>({
      method: 'POST',
      url: '/api/app/a-iAnalysis/analyze-application',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  batchAnalyze = (input: BatchAIAnalysisDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AIAnalysisResultDto[]>({
      method: 'POST',
      url: '/api/app/a-iAnalysis/batch-analyze',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  getRankedApplications = (input: AIRankingRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ApplicationListDto[]>({
      method: 'GET',
      url: '/api/app/a-iAnalysis/ranked-applications',
      params: { jobId: input.jobId, topCount: input.topCount, minScore: input.minScore },
    },
    { apiName: this.apiName,...config });
  

  updateRankings = (jobId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/a-iAnalysis/rankings/${jobId}`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
