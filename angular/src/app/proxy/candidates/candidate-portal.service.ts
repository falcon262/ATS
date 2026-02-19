import type { CandidateApplicationListDto, CandidateRegistrationDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { ApplicationDto } from '../applications/dtos/models';

@Injectable({
  providedIn: 'root',
})
export class CandidatePortalService {
  apiName = 'Default';
  

  getMyApplicationDetail = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ApplicationDto>({
      method: 'GET',
      url: `/api/app/candidate-portal/${id}/my-application-detail`,
    },
    { apiName: this.apiName,...config });
  

  getMyApplications = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, CandidateApplicationListDto[]>({
      method: 'GET',
      url: '/api/app/candidate-portal/my-applications',
    },
    { apiName: this.apiName,...config });
  

  registerFromApplication = (input: CandidateRegistrationDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/candidate-portal/register-from-application',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
