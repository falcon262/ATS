import type { EntityDto, FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { EducationInfo, ExperienceInfo } from './info/models';
import type { CandidateStatus } from '../candidate-status.enum';

export interface CandidateDto extends FullAuditedEntityDto<string> {
  firstName?: string;
  lastName?: string;
  fullName?: string;
  email?: string;
  phoneNumber?: string;
  alternatePhone?: string;
  currentJobTitle?: string;
  currentCompany?: string;
  yearsOfExperience: number;
  city?: string;
  state?: string;
  country?: string;
  postalCode?: string;
  resumeUrl?: string;
  coverLetterUrl?: string;
  portfolioUrl?: string;
  linkedInUrl?: string;
  gitHubUrl?: string;
  personalWebsite?: string;
  skills: string[];
  education: EducationInfo[];
  experience: ExperienceInfo[];
  certifications: string[];
  expectedSalary?: number;
  preferredCurrency?: string;
  isWillingToRelocate: boolean;
  isOpenToRemote: boolean;
  availableFrom?: string;
  noticePeriod?: string;
  source?: string;
  referredBy?: string;
  status?: CandidateStatus;
  overallAIScore?: number;
  tags: string[];
  notes?: string;
  consentToProcess: boolean;
  consentDate?: string;
}

export interface CandidateListDto extends EntityDto<string> {
  fullName?: string;
  email?: string;
  currentJobTitle?: string;
  yearsOfExperience: number;
  location?: string;
  overallAIScore?: number;
  status?: CandidateStatus;
  creationTime?: string;
  topSkills: string[];
}

export interface CreateUpdateCandidateDto {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber?: string;
  alternatePhone?: string;
  currentJobTitle?: string;
  currentCompany?: string;
  yearsOfExperience: number;
  city?: string;
  state?: string;
  country?: string;
  postalCode?: string;
  linkedInUrl?: string;
  gitHubUrl?: string;
  personalWebsite?: string;
  skills: string[];
  education: EducationInfo[];
  experience: ExperienceInfo[];
  certifications: string[];
  expectedSalary?: number;
  preferredCurrency?: string;
  isWillingToRelocate: boolean;
  isOpenToRemote: boolean;
  availableFrom?: string;
  noticePeriod?: string;
  source?: string;
  referredBy?: string;
  tags: string[];
  notes?: string;
}

export interface GetCandidateListInput extends PagedAndSortedResultRequestDto {
  filter?: string;
  skills: string[];
  minExperience?: number;
  maxExperience?: number;
  minAIScore?: number;
  status?: CandidateStatus;
  source?: string;
  isOpenToRemote?: boolean;
}

export interface ResumeDownloadDto {
  content: number[];
  fileName?: string;
  contentType?: string;
}

export interface UploadResumeInput {
  candidateId?: string;
  fileContent: number[];
  fileName?: string;
  contentType?: string;
}
