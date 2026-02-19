import type { CreateUpdateJobDto, GetJobListInput, JobDto, JobListDto, PublishJobInput } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class JobService {
  apiName = 'Default';
  

  close = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, JobDto>({
      method: 'POST',
      url: `/api/app/job/${id}/close`,
    },
    { apiName: this.apiName,...config });
  

  create = (input: CreateUpdateJobDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, JobDto>({
      method: 'POST',
      url: '/api/app/job',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/job/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, JobDto>({
      method: 'GET',
      url: `/api/app/job/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getActiveJobs = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, JobListDto[]>({
      method: 'GET',
      url: '/api/app/job/active-jobs',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetJobListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<JobListDto>>({
      method: 'GET',
      url: '/api/app/job',
      params: { filter: input.filter, departmentId: input.departmentId, status: input.status, employmentType: input.employmentType, experienceLevel: input.experienceLevel, isRemote: input.isRemote, postedAfter: input.postedAfter, postedBefore: input.postedBefore, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  incrementViewCount = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/job/${id}/increment-view-count`,
    },
    { apiName: this.apiName,...config });
  

  publish = (input: PublishJobInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, JobDto>({
      method: 'POST',
      url: '/api/app/job/publish',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateJobDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, JobDto>({
      method: 'PUT',
      url: `/api/app/job/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
