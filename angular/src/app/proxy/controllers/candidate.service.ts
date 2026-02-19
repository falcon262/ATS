import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { IActionResult } from '../microsoft/asp-net-core/mvc/models';

@Injectable({
  providedIn: 'root',
})
export class CandidateService {
  apiName = 'Default';
  

  downloadResumeByCandidateId = (candidateId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, IActionResult>({
      method: 'GET',
      url: `/api/app/candidate/${candidateId}/resume`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
