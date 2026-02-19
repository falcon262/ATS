import type { PublicJobApplicationDto, PublicJobDto } from './public/models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PublicJobService {
  apiName = 'Default';
  

  getActivePublicJobs = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, PublicJobDto[]>({
      method: 'GET',
      url: '/api/app/public-job/active-public-jobs',
    },
    { apiName: this.apiName,...config });
  

  getBySlug = (slug: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PublicJobDto>({
      method: 'GET',
      url: '/api/app/public-job/by-slug',
      params: { slug },
    },
    { apiName: this.apiName,...config });
  

  submitApplication = (input: PublicJobApplicationDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, string>({
      method: 'POST',
      responseType: 'text',
      url: '/api/app/public-job/submit-application',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
