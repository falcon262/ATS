
export interface AIAnalysisRequestDto {
  applicationId?: string;
  forceReanalysis: boolean;
}

export interface AIAnalysisResultDto {
  applicationId?: string;
  overallScore: number;
  skillMatchScore: number;
  experienceScore: number;
  educationScore: number;
  locationScore: number;
  salaryExpectationScore: number;
  aiProvider?: string;
  modelVersion?: string;
  strengths: string[];
  weaknesses: string[];
  recommendation?: string;
  recommendationType?: string;
  extractedKeywords: string[];
  analysisDate?: string;
  processingTimeMs: number;
  confidenceLevel: number;
  redFlags: string[];
}

export interface AIRankingRequestDto {
  jobId?: string;
  topCount: number;
  minScore: number;
}

export interface BatchAIAnalysisDto {
  jobId?: string;
  applicationIds: string[];
  rankAfterAnalysis: boolean;
}
