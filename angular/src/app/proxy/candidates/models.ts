import type { ApplicationStatus } from '../applications/application-status.enum';
import type { PipelineStage } from '../applications/pipeline-stage.enum';

export interface CandidateApplicationListDto {
  id?: string;
  jobTitle?: string;
  company?: string;
  appliedDate?: string;
  status?: ApplicationStatus;
  stage?: PipelineStage;
  aiScore?: number;
  jobId?: string;
}

export interface CandidateRegistrationDto {
  email: string;
  password: string;
  confirmPassword: string;
  applicationId: string;
  acceptTerms: boolean;
}
