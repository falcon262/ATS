import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateUpdateDepartmentDto {
  name: string;
  description?: string;
  code?: string;
  headId?: string;
  headName?: string;
  headEmail?: string;
  parentDepartmentId?: string;
  isActive: boolean;
}

export interface DepartmentDto extends FullAuditedEntityDto<string> {
  name?: string;
  description?: string;
  code?: string;
  headId?: string;
  headName?: string;
  headEmail?: string;
  parentDepartmentId?: string;
  parentDepartmentName?: string;
  isActive: boolean;
  jobCount: number;
  subDepartments: DepartmentDto[];
}

export interface GetDepartmentListInput extends PagedAndSortedResultRequestDto {
  filter?: string;
  isActive?: boolean;
  parentDepartmentId?: string;
}
