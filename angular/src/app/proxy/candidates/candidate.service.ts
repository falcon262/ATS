import type { CandidateDto, CandidateListDto, CreateUpdateCandidateDto, GetCandidateListInput, ResumeDownloadDto, UploadResumeInput } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CandidateService {
  apiName = 'Default';
  

  create = (input: CreateUpdateCandidateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CandidateDto>({
      method: 'POST',
      url: '/api/app/candidate',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/candidate/${id}`,
    },
    { apiName: this.apiName,...config });
  

  downloadResume = (candidateId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ResumeDownloadDto>({
      method: 'POST',
      url: `/api/app/candidate/download-resume/${candidateId}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CandidateDto>({
      method: 'GET',
      url: `/api/app/candidate/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetCandidateListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CandidateListDto>>({
      method: 'GET',
      url: '/api/app/candidate',
      params: { filter: input.filter, skills: input.skills, minExperience: input.minExperience, maxExperience: input.maxExperience, minAIScore: input.minAIScore, status: input.status, source: input.source, isOpenToRemote: input.isOpenToRemote, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getTopCandidates = (count: number = 10, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CandidateListDto[]>({
      method: 'GET',
      url: '/api/app/candidate/top-candidates',
      params: { count },
    },
    { apiName: this.apiName,...config });
  

  grantConsent = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/candidate/${id}/grant-consent`,
    },
    { apiName: this.apiName,...config });
  

  isEmailUnique = (email: string, excludeId?: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/candidate/is-email-unique',
      params: { email, excludeId },
    },
    { apiName: this.apiName,...config });
  

  revokeConsent = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/candidate/${id}/revoke-consent`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateCandidateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CandidateDto>({
      method: 'PUT',
      url: `/api/app/candidate/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  uploadResume = (input: UploadResumeInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CandidateDto>({
      method: 'POST',
      url: '/api/app/candidate/upload-resume',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
