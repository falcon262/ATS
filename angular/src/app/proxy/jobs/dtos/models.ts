import type { EmploymentType } from '../employment-type.enum';
import type { ExperienceLevel } from '../experience-level.enum';
import type { EntityDto, FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { JobStatus } from '../job-status.enum';

export interface CreateUpdateJobDto {
  title: string;
  description: string;
  requirements?: string;
  responsibilities?: string;
  benefits?: string;
  departmentId: string;
  location?: string;
  isRemote: boolean;
  employmentType: EmploymentType;
  experienceLevel: ExperienceLevel;
  minSalary?: number;
  maxSalary?: number;
  currency?: string;
  requiredSkills: string[];
  preferredSkills: string[];
  closingDate?: string;
  hiringManagerId?: string;
  hiringManagerName?: string;
  hiringManagerEmail?: string;
}

export interface GetJobListInput extends PagedAndSortedResultRequestDto {
  filter?: string;
  departmentId?: string;
  status?: JobStatus;
  employmentType?: EmploymentType;
  experienceLevel?: ExperienceLevel;
  isRemote?: boolean;
  postedAfter?: string;
  postedBefore?: string;
}

export interface JobDto extends FullAuditedEntityDto<string> {
  title?: string;
  description?: string;
  requirements?: string;
  responsibilities?: string;
  benefits?: string;
  departmentId?: string;
  departmentName?: string;
  location?: string;
  isRemote: boolean;
  employmentType?: EmploymentType;
  experienceLevel?: ExperienceLevel;
  minSalary?: number;
  maxSalary?: number;
  currency?: string;
  requiredSkills: string[];
  preferredSkills: string[];
  status?: JobStatus;
  postedDate?: string;
  closingDate?: string;
  hiringManagerId?: string;
  hiringManagerName?: string;
  hiringManagerEmail?: string;
  viewCount: number;
  applicationCount: number;
  publicSlug?: string;
  isPubliclyVisible: boolean;
  publicApplicationUrl?: string;
}

export interface JobListDto extends EntityDto<string> {
  title?: string;
  departmentName?: string;
  location?: string;
  employmentType?: EmploymentType;
  experienceLevel?: ExperienceLevel;
  status?: JobStatus;
  postedDate?: string;
  applicationCount: number;
  isRemote: boolean;
}

export interface PublishJobInput {
  jobId?: string;
  scheduledDate?: string;
}
