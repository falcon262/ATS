import type { EntityDto, FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { ApplicationStatus } from '../application-status.enum';
import type { PipelineStage } from '../pipeline-stage.enum';

export interface AIAnalysisDetails {
  skillMatchScore: number;
  experienceScore: number;
  educationScore: number;
  locationScore: number;
  strengths: string[];
  weaknesses: string[];
  recommendation?: string;
  redFlags: string[];
}

export interface ActivityLogEntry {
  timestamp?: string;
  action?: string;
  description?: string;
  userName?: string;
}

export interface ApplicationDto extends FullAuditedEntityDto<string> {
  jobId?: string;
  jobTitle?: string;
  departmentName?: string;
  candidateId?: string;
  candidateName?: string;
  candidateEmail?: string;
  appliedDate?: string;
  status?: ApplicationStatus;
  stage?: PipelineStage;
  aiScore?: number;
  aiMatchSummary?: string;
  aiAnalysisDetails: AIAnalysisDetails;
  skillMatchScores: SkillMatchDto[];
  rank?: number;
  coverLetter?: string;
  screeningAnswers: Record<string, string>;
  assignedToId?: string;
  assignedToName?: string;
  assignedDate?: string;
  reviewNotes?: string;
  rating?: number;
  interviewDate?: string;
  interviewLocation?: string;
  interviewNotes?: string;
  rejectionReason?: string;
  rejectedDate?: string;
  offeredSalary?: number;
  offerDate?: string;
  offerExpiryDate?: string;
  offerAccepted?: boolean;
  activityLog: ActivityLogEntry[];
}

export interface ApplicationListDto extends EntityDto<string> {
  jobId?: string;
  jobTitle?: string;
  candidateId?: string;
  candidateName?: string;
  candidateEmail?: string;
  appliedDate?: string;
  status?: ApplicationStatus;
  stage?: PipelineStage;
  aiScore?: number;
  rank?: number;
  assignedToName?: string;
  rating?: number;
}

export interface CreateApplicationDto {
  jobId: string;
  candidateId: string;
  coverLetter?: string;
  screeningAnswers: Record<string, string>;
}

export interface GetApplicationListInput extends PagedAndSortedResultRequestDto {
  jobId?: string;
  candidateId?: string;
  status?: ApplicationStatus;
  stage?: PipelineStage;
  assignedToId?: string;
  minAIScore?: number;
  appliedAfter?: string;
  appliedBefore?: string;
}

export interface MakeOfferInput {
  applicationId?: string;
  offeredSalary: number;
  offerExpiryDate?: string;
  offerDetails?: string;
}

export interface MoveApplicationStageInput {
  applicationId?: string;
  newStage?: PipelineStage;
  notes?: string;
}

export interface RejectApplicationInput {
  applicationId?: string;
  rejectionReason: string;
  sendNotification: boolean;
}

export interface SkillMatchDto {
  skill?: string;
  hasIt: boolean;
  yearsExperience: number;
  weight?: string;
}

export interface UpdateApplicationDto {
  status?: ApplicationStatus;
  stage?: PipelineStage;
  reviewNotes?: string;
  rating?: number;
  assignedToId?: string;
  assignedToName?: string;
}
