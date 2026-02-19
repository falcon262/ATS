import type { ApplicationDto, ApplicationListDto, CreateApplicationDto, GetApplicationListInput, MakeOfferInput, MoveApplicationStageInput, RejectApplicationInput, UpdateApplicationDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ApplicationService {
  apiName = 'Default';
  

  acceptOffer = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ApplicationDto>({
      method: 'POST',
      url: `/api/app/application/${id}/accept-offer`,
    },
    { apiName: this.apiName,...config });
  

  assignReviewer = (id: string, reviewerId: string, reviewerName: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ApplicationDto>({
      method: 'POST',
      url: `/api/app/application/${id}/assign-reviewer/${reviewerId}`,
      params: { reviewerName },
    },
    { apiName: this.apiName,...config });
  

  create = (input: CreateApplicationDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ApplicationDto>({
      method: 'POST',
      url: '/api/app/application',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  declineOffer = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ApplicationDto>({
      method: 'POST',
      url: `/api/app/application/${id}/decline-offer`,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/application/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ApplicationDto>({
      method: 'GET',
      url: `/api/app/application/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetApplicationListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ApplicationListDto>>({
      method: 'GET',
      url: '/api/app/application',
      params: { jobId: input.jobId, candidateId: input.candidateId, status: input.status, stage: input.stage, assignedToId: input.assignedToId, minAIScore: input.minAIScore, appliedAfter: input.appliedAfter, appliedBefore: input.appliedBefore, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getPipelineStatistics = (jobId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Record<string, number>>({
      method: 'GET',
      url: `/api/app/application/pipeline-statistics/${jobId}`,
    },
    { apiName: this.apiName,...config });
  

  makeOffer = (input: MakeOfferInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ApplicationDto>({
      method: 'POST',
      url: '/api/app/application/make-offer',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  moveToStage = (input: MoveApplicationStageInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ApplicationDto>({
      method: 'POST',
      url: '/api/app/application/move-to-stage',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  reject = (input: RejectApplicationInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ApplicationDto>({
      method: 'POST',
      url: '/api/app/application/reject',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateApplicationDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ApplicationDto>({
      method: 'PUT',
      url: `/api/app/application/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
