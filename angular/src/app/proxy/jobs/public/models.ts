import type { EmploymentType } from '../employment-type.enum';
import type { ExperienceLevel } from '../experience-level.enum';

export interface PublicJobApplicationDto {
  jobId: string;
  firstName: string;
  lastName: string;
  email: string;
  phone?: string;
  currentJobTitle?: string;
  currentCompany?: string;
  yearsOfExperience: number;
  city?: string;
  state?: string;
  country?: string;
  linkedInUrl?: string;
  gitHubUrl?: string;
  portfolioUrl?: string;
  coverLetter?: string;
  educationSummary?: string;
  experienceSummary?: string;
  skills: string[];
  resumeFileName?: string;
  resumeContentBase64?: string;
  consentToProcess: boolean;
}

export interface PublicJobDto {
  id?: string;
  title?: string;
  description?: string;
  requirements?: string;
  responsibilities?: string;
  location?: string;
  isRemote: boolean;
  employmentType?: EmploymentType;
  experienceLevel?: ExperienceLevel;
  requiredSkills: string[];
  preferredSkills: string[];
  postedDate?: string;
  closingDate?: string;
  departmentName?: string;
  publicSlug?: string;
  minSalary?: number;
  maxSalary?: number;
  currency?: string;
}
