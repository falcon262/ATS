import { mapEnumToOptions } from '@abp/ng.core';

export enum PipelineStage {
  Applied = 0,
  Screening = 1,
  PhoneScreen = 2,
  FirstInterview = 3,
  TechnicalAssessment = 4,
  FinalInterview = 5,
  ReferenceCheck = 6,
  Offer = 7,
  Hired = 8,
  Rejected = 9,
}

export const pipelineStageOptions = mapEnumToOptions(PipelineStage);
