import type { CreateUpdateDepartmentDto, DepartmentDto, GetDepartmentListInput } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  apiName = 'Default';
  

  create = (input: CreateUpdateDepartmentDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DepartmentDto>({
      method: 'POST',
      url: '/api/app/department',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/department/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DepartmentDto>({
      method: 'GET',
      url: `/api/app/department/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getAllActive = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DepartmentDto[]>({
      method: 'GET',
      url: '/api/app/department/active',
    },
    { apiName: this.apiName,...config });
  

  getHierarchy = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DepartmentDto[]>({
      method: 'GET',
      url: '/api/app/department/hierarchy',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetDepartmentListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<DepartmentDto>>({
      method: 'GET',
      url: '/api/app/department',
      params: { filter: input.filter, isActive: input.isActive, parentDepartmentId: input.parentDepartmentId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateDepartmentDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DepartmentDto>({
      method: 'PUT',
      url: `/api/app/department/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
